using Data;
using DefaultNamespace.Signals;
using DefaultNamespace.StaticData;
using JetBrains.Annotations;
using Zenject;

namespace Models
{
	[UsedImplicitly]
	public class GameStateModel
	{
		[Inject]
		private GameConfig gameConfig;
		
		[Inject]
		private SignalBus signalBus;

		private GameState state = GameState.MENU;
		private int currentLevelIndex;
		
		public GameState State
		{
			get => state;
			set
			{
				state = value;
				signalBus.Fire(new GameStateChangedSignal(value));
			}
		}

		public int CurrentLevelIndex
		{
			get => currentLevelIndex;
			set
			{
				if(value < 0 || value >= gameConfig.levels.Length)
					return;
				
				currentLevelIndex = value;
				signalBus.Fire(new LevelChangedSignal(value));
			}
		}

		public int LevelsAmount => gameConfig.levels.Length;

		public LevelConfig GetCurrentLevelConfig()
		{
			return gameConfig.levels[CurrentLevelIndex];
		}
		
	}
}