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
			
			//High-level controllers
			Container.BindInterfacesAndSelfTo<LevelController>().AsSingle();
			
			//Signals
			Container.DeclareSignal<FirePressedSignal>();
			Container.DeclareSignal<SpawnBulletSignal>();
			Container.DeclareSignal<SpawnEnemySignal>();
			Container.DeclareSignal<LevelChangedSignal>();
			Container.DeclareSignal<GameStateChangedSignal>();
			Container.DeclareSignal<ChangeLevelSignal>();
			
			Container.BindSignal<ChangeLevelSignal>()
				.ToMethod<LevelController>(x => x.ChangeLevel).FromResolve();
			
			//Static data
			Container.Bind<GameConfig>().FromScriptableObjectResource("StaticData/GameConfig").AsSingle();
			Container.Bind<PlayerConfig>().FromScriptableObjectResource("StaticData/PlayerConfig").AsSingle();
		}
	}
}