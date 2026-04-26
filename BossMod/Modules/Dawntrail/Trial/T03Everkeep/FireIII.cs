namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Spread on each party member: icon 376 appears on all 8 party members simultaneously (~5s
// warning), then 8 helpers each instant-cast Fire III (37752) on their assigned target. Players
// must spread out of each other's circles.
// Forbidden radius bumped 5→7m so the AI keeps a 2m buffer from every other party member during the
// 5s spread window — bumping to 6 wasn't enough to stop the AI from clipping a neighbor's circle
// while routing through them to its own destination.
class FireIII(BossModule module) : Components.SpreadFromIcon(module, 376, AID.FireIII, 7f, 5.1f);
