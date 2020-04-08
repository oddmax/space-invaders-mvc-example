using DefaultNamespace.Signals;
using JetBrains.Annotations;
using Models;
using Zenject;

namespace DefaultNamespace.Presenters
{
	[UsedImplicitly]
	public class LevelController
	{
		[Inject] 
		private SignalBus signalBus;
		
		[Inject] 
		private GameStateModel gameStateModel;
		
		[Inject] 
		private SwarmModel swarmModel;
		
		[Inject] 
		private PlayerShipModel playerShipModel;
		
		public void ChangeLevel(ChangeLevelSignal changeLevelSignal)
		{
			gameStateModel.CurrentLevelIndex += changeLevelSignal.Level;
			swarmModel.Create(gameStateModel.GetCurrentLevelConfig());
			playerShipModel.Reset();
		}
	}
}