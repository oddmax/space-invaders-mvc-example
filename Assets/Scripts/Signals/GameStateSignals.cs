using Data;

namespace DefaultNamespace.Signals
{
	
	public class ChangeLevelSignal
	{
		public int Level { get; }

		public ChangeLevelSignal(int level)
		{
			this.Level = level;
		}
	}
	public class LevelChangedSignal
	{
		public int Level { get; }

		public LevelChangedSignal(int level)
		{
			this.Level = level;
		}
	}
	
	public class StartLevelSignal
	{
	}
	
	public class GameStateChangedSignal
	{
		public GameState GameState { get; }

		public GameStateChangedSignal(GameState gameState)
		{
			this.GameState = gameState;
		}
	}
}