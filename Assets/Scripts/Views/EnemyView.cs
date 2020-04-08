using Data;
using UnityEngine;

namespace DefaultNamespace
{
	public class EnemyView : MonoBehaviour
	{
		[SerializeField]
		private Transform weaponCoordinates; // the turret (bullet spawn location)

		public EnemyData EnemyData { get; private set; }
		public Rigidbody Rigidbody; 

		public void Init(EnemyData enemyData)
		{
			EnemyData = enemyData;
		}
		
		public Vector3 GetWeaponCoordinates()
		{
			return weaponCoordinates.position;
		}
		
		public Quaternion GetRotation()
		{
			return weaponCoordinates.rotation;
		}
	}
}