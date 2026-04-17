namespace BossMod.Dawntrail.Trial.T03Everkeep;

class SoulOverflow(BossModule module) : Components.RaidwideCast(module, AID.SoulOverflow);
class SoulOverflowEnrage(BossModule module) : Components.RaidwideCast(module, AID.SoulOverflowEnrage);
class PatricidalPique(BossModule module) : Components.SingleTargetCast(module, AID.PatricidalPique);
class CalamitysEdge(BossModule module) : Components.RaidwideCast(module, AID.CalamitysEdge);
class Burst(BossModule module) : Components.StandardAOEs(module, AID.Burst, new AOEShapeCircle(8));
class VorpalTrail(BossModule module) : Components.StandardAOEs(module, AID.VorpalTrailAOE, new AOEShapeCircle(6));

class T03EverkeepStates : StateMachineBuilder
{
    public T03EverkeepStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<SoulOverflow>()
            .ActivateOnEnter<SoulOverflowEnrage>()
            .ActivateOnEnter<PatricidalPique>()
            .ActivateOnEnter<CalamitysEdge>()
            .ActivateOnEnter<Burst>()
            .ActivateOnEnter<VorpalTrail>();
    }
}

[ModuleInfo(BossModuleInfo.Maturity.WIP, Contributors = "Gabriel Deleon", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 995, NameID = 12881)]
public class T03Everkeep(WorldState ws, Actor primary) : BossModule(ws, primary, new(100, 100), new ArenaBoundsCircle(20));