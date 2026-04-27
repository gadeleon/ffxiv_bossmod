namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Spread on icon 376; 8 helpers instant-cast Fire III (37752) ~5s later. Forbidden radius is 7m
// (2m wider than the 5m hit) so the AI doesn't clip a neighbor while routing.
class FireIII(BossModule module) : Components.SpreadFromIcon(module, 376, AID.FireIII, 7f, 5.1f)
{
    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        base.AddAIHints(slot, actor, assignment, hints);
        // Duty Support NPCs always fan to the perimeter, so running onto the boss leaves the player
        // alone in the center — only their own Fire III circle hits, no overlap from a neighbor's.
        if (Spreads.Any(s => s.Target == actor))
            hints.GoalZones.Add(hints.GoalSingleTarget(Module.PrimaryActor, 1f, 10f));
    }
}
