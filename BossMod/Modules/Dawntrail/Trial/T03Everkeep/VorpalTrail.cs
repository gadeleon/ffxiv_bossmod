namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Each 42AA Fang does a sequence of sprints (5 inward + 5 outward = 10 segments per fang); the
// rectangle AOE is the line segment each sprint covers, from the fang's current position to the
// sprint endpoint (carried in the cast's TargetPos / LocXZ).
//
// - Initial sprint (VorpalTrailInitial / 38183): instant cast fired when the sprint begins from
//   the arena-edge spawn. Extend rect back by 3 so it reaches the diamond corner.
// - Subsequent sprints (VorpalTrailSprint / 37711): 0.7s cast fired while the fang is stationary
//   at the current waypoint. Rect spans interior waypoints with no back extension.
//
// One rect per fang is tracked; a new sprint cast replaces the previous rect, and rects auto-expire
// shortly after the sprint would complete.
class VorpalTrail(BossModule module) : Components.GenericAOEs(module)
{
    private readonly Dictionary<ulong, AOEInstance> _fangAOE = [];
    private static readonly AOEShapeCircle _centerKeepout = new(6f);

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        var now = WorldState.CurrentTime;
        foreach (var id in _fangAOE.Where(kv => kv.Value.Activation < now).Select(kv => kv.Key).ToList())
            _fangAOE.Remove(id);
        return _fangAOE.Values;
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        base.AddAIHints(slot, actor, assignment, hints);
        // Pinwheel safe-spots all sit near the perimeter; a central keepout stops the AI from
        // settling on the apparent gap between converging rects at the convergence point.
        if (_fangAOE.Count > 0)
            hints.AddForbiddenZone(_centerKeepout, Module.Center);
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if (caster.OID != (uint)OID.Fang)
            return;
        if ((AID)spell.Action.ID != AID.VorpalTrailInitial)
            return;
        // Initial-sprint rects tile the outer diamond perimeter flush — wide halfwidth + back extension.
        SetRect(caster.InstanceID, caster.Position, spell.TargetXZ, 4f, 0f, 3f, WorldState.FutureTime(1.5f));
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (caster.OID != (uint)OID.Fang)
            return;
        if ((AID)spell.Action.ID != AID.VorpalTrailSprint)
            return;
        // Subsequent sprints form a pinwheel — narrower arms + extend back by 2 so the rect reaches
        // the 233C helper sitting 2 units behind the fang's sprint-start position. HalfWidth bumped
        // from 2.5 to 2.75 so adjacent converging lanes overlap slightly — a raw 2.5 left a ~1m
        // sliver at the pinwheel center that the AI read as safe but the player hitbox clipped.
        SetRect(caster.InstanceID, caster.Position, spell.LocXZ, 2.5f, 2.0f, 2.5f, Module.CastFinishAt(spell, 1.0f));
    }

    private void SetRect(ulong fangId, WPos from, WPos to, float halfWidth, float frontExt, float backExt, DateTime expiration)
    {
        var diff = to - from;
        var length = diff.Length();
        if (length < 0.1f)
        {
            _fangAOE.Remove(fangId);
            return;
        }
        var mid = from + diff * 0.5f;
        var angle = Angle.FromDirection(diff);
        _fangAOE[fangId] = new AOEInstance(new AOEShapeRect(length * 0.5f + frontExt, halfWidth, length * 0.5f + backExt), mid, angle, expiration);
    }
}
