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
	
	public class SpawnEnemySignal
	{
		public Vector3 Coordinates { get; }
		public EnemyData EnemyData { get; }
		public SpawnEnemySignal(EnemyData enemyData, Vector3 coordinates)
		{
			Coordinates = coordinates;
			EnemyData = enemyData;
		}
	}
	
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
	
	public class GameStateChangedSignal
	{
		public GameState GameState { get; }

		public GameStateChangedSignal(GameState gameState)
		{
			this.GameState = gameState;
		}
	}
}