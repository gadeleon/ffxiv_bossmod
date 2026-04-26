namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Dawn of an Age is the raidwide that also shrinks the arena: when the cast resolves the platform
// collapses to a 20x20 center square for the Chasm of Vollok + Forged Track sequence. Actualize
// restores the full 40x40 arena at the end of that sequence.
class DawnOfAnAge(BossModule module) : Components.RaidwideCast(module, AID.DawnOfAnAge)
{
    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        base.OnEventCast(caster, spell);
        if ((AID)spell.Action.ID == AID.DawnOfAnAge)
            Module.Arena.Bounds = T03Everkeep.SmallBounds;
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        base.AddAIHints(slot, actor, assignment, hints);
        // While the cast is up, forbid everything outside the post-shrink diamond so ranged jobs
        // pull in toward center before the platform collapses around them.
        var center = Module.Center;
        var bounds = T03Everkeep.SmallBounds;
        foreach (var c in Casters)
            hints.AddForbiddenZone(p => !bounds.Contains(p - center), Module.CastFinishAt(c.CastInfo));
    }
}
