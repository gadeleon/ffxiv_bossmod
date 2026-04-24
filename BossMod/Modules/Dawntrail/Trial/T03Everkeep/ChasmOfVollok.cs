namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Cast target positions sit at the "near" corner of each 5x5 grid cell rather than the center,
// so we draw the rect as 5 forward + 0 back (origin at the back corner) instead of centered.
// That way the extreme rect (SE-most in local) has its outer corner at the arena's S diamond tip.
class ChasmOfVollok(BossModule module) : Components.StandardAOEs(module, AID.ChasmOfVollokAOE, new AOEShapeRect(5f, 2.5f));
