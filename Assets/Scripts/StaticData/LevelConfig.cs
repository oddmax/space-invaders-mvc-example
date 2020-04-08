using UnityEngine;

namespace DefaultNamespace.StaticData
{
	[System.Serializable]
	public class EnemyLocation
	{
		public EnemyConfig enemyConfig;
		public Vector2 startPosition;
	}
	
	public class LevelConfig : ScriptableObject
	{
		public EnemyLocation[] enemyInfos;
	}
}