// Note: Due to the NPC Skill Blind, there's some randomness in because Allied NPCs may or may not survive.
// This route plays it extremely safe and assumes only Mylla is alive to take out ignored mobs in this module.

namespace BossMod.QuestBattle.ARealmReborn.ClassJobQuests.PLD;

[ZoneModuleInfo(BossModuleInfo.Maturity.Contributed, 318,257)]
internal class TheRematch(WorldState ws) : QuestBattle(ws)
{
    public override List<QuestObjective> DefineObjectives(WorldState ws) => [
        new QuestObjective(ws) // Take out Starting Conjurer (0x292) and Starting Lancer (0x293)
            .PauseForCombat(false)
            .With(obj => {
                var killed = new HashSet<uint>();
                obj.OnActorKilled += act => {
                    if (act.OID == 0x292 || act.OID == 0x293)
                        killed.Add(act.OID);
                    obj.CompleteIf(killed.Count >= 2);
                };
            }),
        new QuestObjective(ws) // Take out waiting Conjurer (0x292) and waiting Pugilist (0x294)
            .WithConnection(new Vector3(5.2286663f, -31f, -8.183876f))
            .PauseForCombat(false)
            .With(obj => {
                var killed = new HashSet<uint>();
                obj.OnActorKilled += act => {
                    if (act.OID == 0x294 || act.OID == 0x293)
                        killed.Add(act.OID);
                    obj.CompleteIf(killed.Count >= 2);
                };
            }),
        new QuestObjective(ws) // Take out Ambushing Archers
            //.WithConnection(new Vector3(5.12009f, -17.544205f, 23.051228f))
            .WithConnection(new Vector3(5.4512296f, -17.544205f, 22.28043f))
            .PauseForCombat(false)
            .Hints((player, hints) =>
            {
                foreach (var e in hints.PotentialTargets)
                    if (e.Actor.OID == 0x2AF || e.Actor.OID == 0x297 || e.Actor.OID == 0x298 || e.Actor.OID == 0x52C) // Ignore bridge mobs for this objective
                        e.Priority = AIHints.Enemy.PriorityForbidden;
                hints.PrioritizeTargetsByOID(0x295,5);
            })
            .CompleteOnKilled(0x295, 2),
        new QuestObjective(ws) // Take out Ambushing Thaumaturges (0x296)
            //.WithConnection(new Vector3(18.953478f, -6.0142717f, -3.4347162f))
            .PauseForCombat(false)
            .Hints((player, hints) =>
                {
                    foreach (var e in hints.PotentialTargets)
                        if (e.Actor.OID == 0x2AF || e.Actor.OID == 0x297 || e.Actor.OID == 0x298 || e.Actor.OID == 0x52C) // Ignore bridge mobs for this objective
                            e.Priority = AIHints.Enemy.PriorityForbidden;
                    hints.PrioritizeTargetsByOID(0x296,5);
                })
            .CompleteOnKilled(0x296, 2),
        new QuestObjective(ws) // Target Gotwin
            .WithConnection(new Vector3(14.811271f, -6.482954f, -15.588835f))
            .PauseForCombat(false)
            .Hints((player, hints) =>
            {
                foreach (var e in hints.PotentialTargets)
                    if (e.Actor.OID == 0x52C) // Ignore Leavold
                        e.Priority = AIHints.Enemy.PriorityForbidden;
                hints.PrioritizeTargetsByOID(0x2AF,5);
            })
            .CompleteOnKilled(0x2AF),
        new QuestObjective(ws) // Clear out the last lancer
            .WithConnection(new Vector3(-0.95f, -10.02f, -16.34f))
            .PauseForCombat(false)
            .Hints((player, hints) =>
            {
                foreach (var e in hints.PotentialTargets)
                    if (e.Actor.OID == 0x52C  || e.Actor.OID == 0x298) // Ignore Leavold & Bridge Pugilists otherwise to prevent AoE from Pulling from Mylla
                        e.Priority = AIHints.Enemy.PriorityForbidden;
            })
            .CompleteOnKilled(0x297, 1),
        new QuestObjective(ws) // Wait for Gigrya to separate from his Lancers.
            .WithConnection(new Vector3(5.2827086f, -7.8521442f, 1.292858f))
            .ThenWait(25.0f)
            .PauseForCombat(false)
            .Hints((player, hints) =>
            {
                foreach (var e in hints.PotentialTargets)
                    if (e.Actor.OID == 0x52C || e.Actor.OID == 0x297 || e.Actor.OID == 0x290 ||  e.Actor.OID == 0x298) // Ignore all mobs while NPCs position
                        e.Priority = AIHints.Enemy.PriorityForbidden;
            }),
        new QuestObjective(ws) // Target Gigrya; Note: may complete Duty here once remaining NPCs die
            .WithConnection(new Vector3(15.304749f, -6.0142717f, -4.5930176f))
            .PauseForCombat(false)
            .Hints((player, hints) =>
            {
                foreach (var e in hints.PotentialTargets)
                    if (e.Actor.OID == 0x52C || e.Actor.OID == 0x297  || e.Actor.OID == 0x298 ) // Ignore all other mobs
                        e.Priority = AIHints.Enemy.PriorityForbidden;
                hints.PrioritizeTargetsByOID(0x290,5);
            })
            .CompleteOnKilled(0x290),
        new QuestObjective(ws) // Target Void Slave, Remaining Mobs will be Handled by AI
            .PauseForCombat(false)
            .Hints((player, hints) =>
            {
                foreach (var e in hints.PotentialTargets)
                    if (e.Actor.OID == 0x52C || e.Actor.OID == 0x297 || e.Actor.OID == 0x298) // Ignore all other mobs
                        e.Priority = AIHints.Enemy.PriorityForbidden;
                hints.PrioritizeTargetsByOID(0x29C,5);
            })
            .CompleteOnKilled(0x29C)
    ];
}


/*
[ ] Resolve GameData
1AF6581D090:4000B68E[PreviousTarget] - BattleNpc - Leavold, Sword of Thal - X-2.2125854 Y-7.1800265 Z-33.24945 D16 R-0.063421726 - Target: 4000B68F
       Level: 50 ClassJob: 1 CHP: 256464 MHP: 258900 CMP: 27000 MMP: 28000
       Customize: 01 00 01 64 01 05 07 01 01 01 04 B0 40 05 05 01 80 01 03 00 03 64 00 00 00 00 StatusFlags: Hostile, InCombat, WeaponOut
[ C ]
[ Clear CT ]
[ Clear FT ]
[ Set CT ]
[ Set FT ] |
*/

// new(14.195155f, -6.0142727f, 0.2007984f)
// new(9.84198f, -7.6376314f, 2.670288f)
/*
 * "DataId": 662,
          "Position": {
            "X": 9.84198,
            "Y": -7.6376314,
            "Z": 2.670288
          },
          "TerritoryId": 257,
          "InteractionType": "Interact"
 */

/*
 "DataId": 656, // Gigirya
          "Position": {
            "X": 25.009521,
            "Y": -6.014861,
            "Z": -11.551086
          },
          "TerritoryId": 257,
          "InteractionType": "Interact"
*/

/*"DataId": 687, // Gotwin
          "Position": {
            "X": -4.3183594,
            "Y": -10.015328,
            "Z": -15.091187
          },
          "TerritoryId": 257,
          "InteractionType": "Interact"
          */

// 658 Starting Archer
// 659 Starting Lancer
// 661 Archer
// 662 Tahuma
// 687 Gotwin
// 656 Gigirya
// Void Thingy


/*
"DataId": 1324,
          "Position": {
            "X": -2.2125854,
            "Y": -7.1800265,
            "Z": -33.24945
          },
          "TerritoryId": 257,
          "InteractionType": "Interact"
*/
