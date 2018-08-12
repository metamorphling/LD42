using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        InstallObjects();
        InstallSignals();
    }

    void InstallObjects()
    {
        Container.Bind<GameManager>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.Bind<PlaneController>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.Bind<TrailStorageController>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.Bind<ScoreController>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.Bind<TimerController>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.Bind<DebugController>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.Bind<SkyboxController>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }

    void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<SignalChangeColors>();
        Container.DeclareSignal<SignalGameState>();
        Container.DeclareSignal<SignalIncreaseScore>();
    }
}