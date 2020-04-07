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
		
		public int currentLevelIndex;

		public LevelConfig GetCurrentLevelConfig()
		{
			return gameConfig.levels[currentLevelIndex];
		}
	}
}