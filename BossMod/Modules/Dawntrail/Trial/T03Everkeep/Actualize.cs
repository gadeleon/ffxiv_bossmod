namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Actualize is the raidwide that restores the arena after the Chasm of Vollok + Forged Track
// sequence: at cast resolution the platform expands back to the full 40x40 arena.
class Actualize(BossModule module) : Components.RaidwideCast(module, AID.Actualize)
{
    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        base.OnEventCast(caster, spell);
        if ((AID)spell.Action.ID == AID.Actualize)
            Module.Arena.Bounds = T03Everkeep.NormalBounds;
    }
}
