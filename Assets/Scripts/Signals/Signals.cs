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
}