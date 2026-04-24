namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Spread on each party member: icon 376 appears on all 8 party members simultaneously (~5s
// warning), then 8 helpers each instant-cast Fire III (37752) on their assigned target. Players
// must spread out of each other's circles.
class FireIII(BossModule module) : Components.SpreadFromIcon(module, 376, AID.FireIII, 5f, 5.1f);
