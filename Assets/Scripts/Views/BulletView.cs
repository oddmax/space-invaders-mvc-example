using System;
using DefaultNamespace.StaticData;
using UnityEngine;

namespace DefaultNamespace
{
	public class BulletView : MonoBehaviour
	{
		[SerializeField] 
		public Rigidbody Rigidbody;

		public BulletConfig Config { get; private set; }

		public event Action<BulletConfig, Collider> OnCollide;

		public void Init(BulletConfig bulletConfig)
		{
			Config = bulletConfig;
		}
		
		void OnTriggerEnter(Collider other)
		{
			OnCollide(Config, other);
		}
	}
}