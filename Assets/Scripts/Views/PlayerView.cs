using DefaultNamespace.StaticData;
using UnityEngine;

namespace DefaultNamespace
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField]
		private Transform weaponCoordinates; // the turret (bullet spawn location)

		[SerializeField] 
		public Rigidbody Rigidbody;

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