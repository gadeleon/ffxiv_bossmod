namespace BossMod.Dawntrail.Trial.T03Everkeep;

public enum OID : uint
{
    Boss = 0x42A9, // R2.500, phase 1 (human form)
    BossP2 = 0x42B4, // phase 2 (Vollok form) - spawns mid-fight
    Helper = 0x233C,
    ShadowOfTural = 0x43A8, // R0.500, initial add waves (phase 1)
    Fang = 0x42AA, // spawn during fight
    ShadowOfTuralSword = 0x42AC, // spawn during fight, later wave
    ShadowOfTuralSpear = 0x42AD, // spawn during fight, later wave
    // 0x42B0..0x42B3 observed in replay; purpose TBD (phase 2 adds or fang variants)
}

public enum AID : uint
{
    AutoAttack = 6497, // Boss->player, no cast, single-target

    // === Phase 1 ===
    SoulOverflow = 37707, // Boss->self, 4.7s cast, raidwide
    SoulOverflowEnrage = 37744, // Boss->self, 6.7s cast, raidwide + inflicts bleed DoT (phase transition / enrage)
    PatricidalPique = 37715, // Boss->MT, 4.7s cast, single-target tankbuster
    CalamitysEdge = 37708, // Boss->self, 4.7s cast, raidwide
    Burst = 37709, // ShadowOfTural->self, 7.7s cast, range 8 circle (8 adds spawn in a pattern, each casts)

    // Vorpal Trail: Fang adds charge across arena leaving a trail of circles
    VorpalTrailVisual = 37710, // Boss->self, 3.4s cast, single-target visual (mechanic start)
    VorpalTrailSprint = 37711, // Fang->self, 0.7s cast, internal sprint tick (no player damage)
    VorpalTrailAOE = 37712, // Helper->location, 2.0s cast, range 6 circle (trail puddle)
    VorpalTrailTelegraph = 38184, // Helper->location, 4.0s cast, 0-hit path telegraph
}