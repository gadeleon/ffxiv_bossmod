namespace BossMod.Dawntrail.Trial.T03Everkeep;

// Track both the 6.7s preview (37720) and the 0.7s final damage cast (37722). The preview gives
// the long warning window the AI needs to reposition; the damage cast still renders as a
// final-moment reminder. Target positions for both are the same grid cells, so the rect appears
// in the correct spot throughout.
//
// Origin-at-back-corner shape (5f forward + 0 back) ensures the SE-most rect's outer corner
// reaches the arena's S diamond tip — cast targets sit at the back corner of each 5x5 cell, not
// the center.
class ChasmOfVollok(BossModule module) : Components.GroupedAOEs(module, [AID.ChasmOfVollokPreview, AID.ChasmOfVollokAOE], new AOEShapeRect(5f, 2.5f));
