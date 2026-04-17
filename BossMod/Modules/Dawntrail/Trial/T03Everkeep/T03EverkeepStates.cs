namespace BossMod.Dawntrail.Trial.T03Everkeep;

class T03EverkeepStates : StateMachineBuilder
{
    private readonly T03Everkeep _module;

    public T03EverkeepStates(T03Everkeep module) : base(module)
    {
        _module = module;
        SimplePhase(0, Phase1, "P1")
            .Raw.Update = () => Module.PrimaryActor.IsDeadOrDestroyed || (Module.PrimaryActor.CastInfo?.IsSpell(AID.SoulOverflowEnrage) ?? false);
        SimplePhase(1, Phase2, "P2")
            .Raw.Update = () => Module.PrimaryActor.IsDeadOrDestroyed && (_module.BossP2()?.IsDeadOrDestroyed ?? true);
    }

    private void Phase1(uint id)
    {
        SimpleState(id, 10000, "P1")
            .ActivateOnEnter<SoulOverflow>()
            .ActivateOnEnter<SoulOverflowEnrage>()
            .ActivateOnEnter<PatricidalPique>()
            .ActivateOnEnter<CalamitysEdge>()
            .ActivateOnEnter<Burst>()
            .ActivateOnEnter<VorpalTrail>()
            .ActivateOnEnter<DoubleEdgedSwords>();
    }

    private void Phase2(uint id)
    {
        SimpleState(id, 10000, "P2")
            .ActivateOnEnter<DawnOfAnAge>()
            .ActivateOnEnter<Actualize>();
        // phase 2 mechanics activated as they are implemented
    }
}
