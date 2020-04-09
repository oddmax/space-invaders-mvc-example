using Data;
using DefaultNamespace.StaticData;
using UnityEngine;

namespace DefaultNamespace.Signals
{
	public class FirePressedSignal
	{
		public FirePressedSignal()
		{
		}
	}
	
	public class SpawnBulletSignal
	{
		public Vector3 Coordinates { get; }
		public BulletConfig BulletConfig { get; }
		public Quaternion Rotation { get; }

		public SpawnBulletSignal(BulletConfig bulletConfig, Vector3 coordinates, Quaternion rotation)
		{
			Coordinates = coordinates;
			BulletConfig = bulletConfig;
			Rotation = rotation;
		}
	}
	
	public class SpawnEnemiesSignal
	{
		public Vector3 Coordinates { get; }
		public EnemyData EnemyData { get; }
		public SpawnEnemiesSignal(EnemyData enemyData, Vector3 coordinates)
		{
			Coordinates = coordinates;
			EnemyData = enemyData;
		}
	}

	public class PlayerHitSignal
	{
		public PlayerHitSignal()
		{
			
		}
	}
	
	public class EnemyHitSignal
	{
		public EnemyData EnemyData { get; }

		public EnemyHitSignal(EnemyData enemyData)
		{
			this.EnemyData = enemyData;
		}
	}
}