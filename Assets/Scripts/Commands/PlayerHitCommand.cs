using Data;
using DefaultNamespace.Commands;
using DefaultNamespace.Signals;
using JetBrains.Annotations;
using Models;
using Zenject;

namespace Commands
{
	[UsedImplicitly]
	public class PlayerHitCommand : Command
	{
		[Inject] 
		private PlayerScoreModel playerScoreModel;
		
		[Inject] 
		private GameStateModel gameStateModel;
		
		[Inject] 
		private PlayerShipModel playerShipModel;

		public void Execute(PlayerHitSignal playerHitSignal)
		{
			playerShipModel.Hp.Value--;

			if (playerShipModel.Hp.Value <= 0)
			{
				gameStateModel.State = GameState.GAME_OVER;
			}
		}
	}
}