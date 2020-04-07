using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace DefaultNamespace.StaticData
{
	public struct EnemyLocation
	{
		public EnemyConfig enemyConfig;
		public Vector2 startPosition;
	}
	
	public class LevelConfig : ScriptableObject
	{
		public EnemyLocation[] enemyInfos;
	}
}