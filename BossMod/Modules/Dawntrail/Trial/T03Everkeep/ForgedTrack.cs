namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Forged Track: 4 intercardinal platforms spawn sword Fangs that telegraph narrow lane charges
// across the small arena. Each preview (37729, 11.6s, 0-hit) has an outer-platform caster; after
// the preview resolves, a separate inner Fang instant-casts 37730 for the actual damage along a
// parallel lane.
//
// The lane relationship between preview and damage has two patterns, keyed off the cast rotation:
// - NE-SW diagonal (rot -45° or +135°): damage lane is offset ±5 perpendicular from preview lane,
//   with the sign determined by Gateway's rotation (captured from its cast).
// - NW-SE diagonal (rot +45° or -135°): damage lane coincides with preview lane (no offset).
//
// MapEffect indices 11-14 (state 0x00020001 vs 0x00200010) also encode which platforms are in
// which pattern, but rotation is sufficient for the two cases we observe.
class ForgedTrack(BossModule module) : Components.GenericAOEs(module)
{
    private readonly Dictionary<ulong, AOEInstance> _aoes = [];
    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => _aoes.Values;

    private static readonly AOEShapeRect _shape = new(20f, 2.5f);
    private const float ForwardOffset = 30f;
    private const float PerpOffsetMagnitude = 5f;

    private float _gatewaySign; // +1 / -1, captured from Gateway cast; 0 if not yet seen.

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID == AID.Gateway)
            _gatewaySign = spell.Rotation.Rad >= 0 ? 1f : -1f;
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID != AID.ForgedTrackPreview)
            return;

        var dir = spell.Rotation.ToDirection();
        // NE-SW diagonal: dir.X and dir.Z have opposite signs; NW-SE: same signs.
        var isNESWDiagonal = dir.X * dir.Z < 0;
        var perpOffset = isNESWDiagonal && _gatewaySign != 0
            ? dir.OrthoL() * (PerpOffsetMagnitude * _gatewaySign)
            : default;
        var origin = caster.Position + dir * ForwardOffset + perpOffset;
        _aoes[caster.InstanceID] = new AOEInstance(_shape, origin, spell.Rotation, Module.CastFinishAt(spell));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.ForgedTrackPreview)
            _aoes.Remove(caster.InstanceID);
    }
}
