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
}