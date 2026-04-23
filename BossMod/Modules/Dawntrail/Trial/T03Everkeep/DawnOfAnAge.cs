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
}
