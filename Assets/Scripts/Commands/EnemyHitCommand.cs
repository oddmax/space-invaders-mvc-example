using Data;
using DefaultNamespace.Signals;
using JetBrains.Annotations;
using Models;
using Zenject;

namespace DefaultNamespace.Commands
{
	[UsedImplicitly]
	public class EnemyHitCommand : Command
	{
		[Inject] 
		private PlayerScoreModel playerScoreModel;
		
		[Inject] 
		private GameStateModel gameStateModel;
		
		[Inject] 
		private EnemiesSwarmModel swarmModel;
		
		[Inject] 
		private SignalBus signalBus;
		
		public void Execute(EnemyHitSignal enemyHitSignal)
		{
			var enemyData = enemyHitSignal.EnemyData;
			enemyData.Hp.Value--;

			if (enemyData.Hp.Value == 0)
			{
				playerScoreModel.Score.Value++;
			}

			if (swarmModel.HasNoEnemies())
			{
				if (gameStateModel.CurrentLevelIndex == gameStateModel.LevelsAmount)
				{
					gameStateModel.State = GameState.MENU;
				}
				else
				{
					signalBus.Fire(new ChangeLevelSignal(gameStateModel.CurrentLevelIndex+1));
				}
			}
		}
	}
}