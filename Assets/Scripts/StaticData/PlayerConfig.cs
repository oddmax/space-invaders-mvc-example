using Data;
using UnityEngine;

namespace DefaultNamespace.StaticData
{
	public class PlayerConfig : ScriptableObject
	{
		public int lifeAmount;
		public float fireRate = 0.5f;
		
		public float movementSpeed;
		public float movementTiltFactor;  
		public Boundary movementBoundary; 
		
		public GameObject viewPrefab;
	}
}