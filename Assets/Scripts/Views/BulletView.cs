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

		public event Action<BulletView, Collider> OnCollide;
		public event Action<BulletView, Collider> OnCollideExit;

		public void Init(BulletConfig bulletConfig)
		{
			Config = bulletConfig;
		}
		
		void OnTriggerEnter(Collider other)
		{
			OnCollide(this, other);
		}
		
		void OnTriggerExit(Collider other)
		{
			OnCollideExit(this, other);
		}
	}
}