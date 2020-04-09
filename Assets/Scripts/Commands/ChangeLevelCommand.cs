using DefaultNamespace.Signals;
using JetBrains.Annotations;
using Models;
using Zenject;

namespace DefaultNamespace.Presenters
{
	[UsedImplicitly]
	public class ChangeLevelCommand
	{
		[Inject] 
		private SignalBus signalBus;
		
		[Inject] 
		private GameStateModel gameStateModel;
		
		[Inject] 
		private EnemiesSwarmModel swarmModel;
		
		[Inject] 
		private PlayerShipModel playerShipModel;
		
		public void Execute(ChangeLevelSignal changeLevelSignal)
		{
			gameStateModel.CurrentLevelIndex = changeLevelSignal.Level;
			swarmModel.Create(gameStateModel.GetCurrentLevelConfig());
			playerShipModel.Reset();
			
			signalBus.Fire<StartLevelSignal>();
		}
	}
}