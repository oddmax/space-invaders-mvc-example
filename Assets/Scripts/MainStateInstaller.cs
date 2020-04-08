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
			
			//Static data
			Container.Bind<GameConfig>().FromScriptableObjectResource("StaticData/GameConfig").AsSingle();
			Container.Bind<PlayerConfig>().FromScriptableObjectResource("StaticData/PlayerConfig").AsSingle();
		}
	}
}