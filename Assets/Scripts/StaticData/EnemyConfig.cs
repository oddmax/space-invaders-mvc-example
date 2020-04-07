using UnityEngine;

namespace DefaultNamespace.StaticData
{
	public class EnemyConfig : ScriptableObject
	{
		public GameObject viewPrefab;
		public int attackDamage;
		public int lifeAmount;
	}
}