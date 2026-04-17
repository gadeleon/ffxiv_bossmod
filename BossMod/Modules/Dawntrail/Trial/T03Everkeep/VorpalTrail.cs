namespace BossMod.Dawntrail.Trial.T03Everkeep;

class VorpalTrail(BossModule module) : Components.StandardAOEs(module, AID.VorpalTrailAOE, new AOEShapeCircle(6));
