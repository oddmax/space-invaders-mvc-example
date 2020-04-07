using UnityEngine;

namespace DefaultNamespace.StaticData
{
	public class GameConfig : ScriptableObject
	{
		public LevelConfig[] levels;
		public PlayerConfig playerConfig;
	}
}