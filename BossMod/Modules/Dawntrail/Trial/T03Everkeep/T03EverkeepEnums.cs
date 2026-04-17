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
    FangSmall = 0x42B6, // R1.000, spawn during fight, Phase 2 Fang that telegraphs Chasm of Vollok preview
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

    // Double-edged Swords: two half-arena cleaves in sequence (forward-then-backward)
    DoubleEdgedSwordsVisual = 37713, // Boss->self, 4.1s cast, single-target visual (mechanic start)
    DoubleEdgedSwordsAOE = 37714, // Helper->self, 4.7s cast, range 60 width 120 rect (half-arena cleave; 2 casters with opposite rotations fire ~2.3s apart)

    // === Phase 2 (BossP2 / Zoraal Ja Vollok form, OID 0x42B4) ===
    DawnOfAnAge = 37716, // BossP2->self, 6.7s cast, raidwide + arena transition (distinct from Ex2 AID 37783)
    Actualize = 37718, // BossP2->self, 4.7s cast, raidwide (distinct from Ex2 AID 37784 at 5.0s)

    // Chasm of Vollok chain: Vollok spawns Fangs -> preview telegraphs at fang positions -> Sync -> helper AOEs resolve at same positions
    Vollok = 37719, // BossP2->self, 3.7s cast, visual (spawns FangSmall actors in a grid, no player damage)
    ChasmOfVollokPreview = 37720, // FangSmall->self, 6.7s cast, 5x5 rect telegraph, no player damage
    Sync = 37721, // BossP2->self, 4.7s cast, visual (activates AOEs), no player damage
    ChasmOfVollokAOE = 37722, // Helper->self, 0.7s cast, range 5 width 5 rect (final damage; positions match preview in Normal - no mirroring)

    // Gateway / Blade Warp / Forged Track chain: boss creates portals -> places swords -> charges along them
    Gateway = 37723, // BossP2->self, 3.7s cast, visual (spawns portals/gateways, no player damage)
    BladeWarp = 37726, // BossP2->self, 3.7s cast, visual (places swords, no player damage)
    ForgedTrackVisual = 37727, // BossP2->self, 3.7s cast, visual (no player damage)
    ForgedTrackPreview = 37729, // Helper->self, 11.6s cast, outer-arena sword-path telegraph (0 hits in CST!) - TODO: component
    ForgedTrackAOE = 37730, // Fang (OID 0x42AA)->self, instant cast, sword-charge damage along path - TODO: component paired with preview

    // Duty's Edge: target selection (35567) -> boss visual (37748) -> 4 repeated line-stack hits (37749 visual + 37750 damage)
    DutysEdgeTarget = 35567, // Helper->player, instant, target marker (shared AID with Ex2)
    DutysEdgeVisual = 37748, // BossP2->self, 4.6s cast, visual (mechanic start)
    DutysEdgeRepeat = 37749, // BossP2->self, 2.1s cast, visual (fires 4x repeated with 37750)
    DutysEdgeAOE = 37750, // Helper->self, instant, range 100 width 8 rect line-stack (distinct from Ex2 AID 38055)

    // Half Full: half-arena cleave (single variant observed in replay, west-facing)
    HalfFullVisual = 37737, // BossP2->self, 5.7s cast, visual only (self-status, no player damage)
    HalfFullAOE = 37738, // Helper->self, 6.0s cast, range 60 width 120 rect, actual cleave

    // Bitter Reaping: two simultaneous single-target tankbusters on MT + OT (distinct from Ex2 Bitter Whirlwind which is 3-hit tank-swap)
    BitterReapingVisual = 37753, // BossP2->self, 4.1s cast, visual only
    BitterReapingAOE = 37754, // Helper->player, 4.7s cast, single-target tankbuster (2 simultaneous casts targeting MT + Wuk/OT)

    // Half Circuit: 3 simultaneous AoEs — always a side-cleave rect + (circle OR donut) center shape
    HalfCircuitVisualCircle = 37739, // BossP2->self, 6.7s cast, visual paired with circle variant (shared AID with Ex2)
    HalfCircuitVisualDonut = 37740, // BossP2->self, 6.7s cast, visual paired with donut variant (shared AID with Ex2)
    HalfCircuitRect = 37741, // Helper->self, 7.0s cast, range 60 width 120 rect (always fires, rotation varies)
    HalfCircuitDonut = 37742, // Helper->self, 6.7s cast, range 10-30 donut (center safe)
    HalfCircuitCircle = 37743, // Helper->self, 6.7s cast, range 10 circle (outer safe)
}