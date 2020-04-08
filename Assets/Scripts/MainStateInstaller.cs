using Commands;
using DefaultNamespace.Commands;
using DefaultNamespace.Presenters;
using DefaultNamespace.Signals;
using DefaultNamespace.StaticData;
using Models;
using Zenject;

namespace DefaultNamespace
{
	public class MainStateInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			SignalBusInstaller.Install(Container);
			
			//Models
			Container.BindInterfacesAndSelfTo<PlayerScoreModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<PlayerShipModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<SwarmModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<GameStateModel>().AsSingle();

			//Signals
			Container.DeclareSignal<FirePressedSignal>();
			Container.DeclareSignal<SpawnBulletSignal>();
			Container.DeclareSignal<SpawnEnemiesSignal>();
			Container.DeclareSignal<LevelChangedSignal>();
			Container.DeclareSignal<GameStateChangedSignal>();
			Container.DeclareSignal<ChangeLevelSignal>();
			Container.DeclareSignal<StartLevelSignal>();
			Container.DeclareSignal<PlayerHitSignal>();
			Container.DeclareSignal<EnemyHitSignal>();
			
			//Commands
			//High-level controllers
			Container.BindInterfacesAndSelfTo<ChangeLevelCommand>().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemyHitCommand>().AsSingle();
			Container.BindInterfacesAndSelfTo<PlayerHitCommand>().AsSingle();
			
			Container.BindSignal<ChangeLevelSignal>()
				.ToMethod<ChangeLevelCommand>(x => x.Execute).FromResolve();
			
			Container.BindSignal<EnemyHitSignal>()
				.ToMethod<EnemyHitCommand>(x => x.Execute).FromResolve();
			
			Container.BindSignal<PlayerHitSignal>()
				.ToMethod<PlayerHitCommand>(x => x.Execute).FromResolve();
			
			//Static data
			Container.Bind<GameConfig>().FromScriptableObjectResource("StaticData/GameConfig").AsSingle();
			Container.Bind<PlayerConfig>().FromScriptableObjectResource("StaticData/PlayerConfig").AsSingle();
		}
	}
}