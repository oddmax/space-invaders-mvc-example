using System;
using System.Collections.Generic;
using DefaultNamespace.Signals;
using DefaultNamespace.StaticData;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class BulletsPresenter : MonoBehaviour
	{
		[Inject] 
		private SignalBus signalBus;

		private List<BulletView> bullets = new List<BulletView>();

		private void Start()
		{
			signalBus.Subscribe<SpawnBulletSignal>(OnSpawnBullet);
		}

		public void SpawnBullet(BulletConfig bulletConfig, Vector3 position, Quaternion rotation)
		{
			BulletView bulletView = Instantiate(bulletConfig.viewPrefab,position, rotation).GetComponent<BulletView>();
			bulletView.Init(bulletConfig);
			bulletView.OnCollide += BulletViewOnOnCollide;
			bulletView.OnCollideExit += BulletViewOnOnCollideExit;
			bullets.Add(bulletView);
		}

		private void BulletViewOnOnCollide(BulletView bulletView, Collider other)
		{
			if (bulletView.CompareTag("Bullet") && other.CompareTag("Bullet"))
				return;
			
			//Destroy(other.gameObject);
			//Destroy(gameObject);
		}
		
		private void BulletViewOnOnCollideExit(BulletView bulletView, Collider other)
		{
			Debug.Log("tag = " + other.tag);
			if (other.CompareTag("Boundary"))
			{
				RemoveBullet(bulletView);
			}
		}

		private void Update()
		{
			for (int i = 0; i < bullets.Count; i++)
			{
				var bulletView = bullets[i];
				bulletView.Rigidbody.velocity = transform.forward * bulletView.Config.speed;
			}
		}
		
		private void RemoveBullet(BulletView bulletView)
		{
			bullets.Remove(bulletView);
			Destroy(bulletView);
		}
		
		private void OnSpawnBullet(SpawnBulletSignal spawnBulletSignal)
		{
			SpawnBullet(spawnBulletSignal.BulletConfig, spawnBulletSignal.Coordinates, spawnBulletSignal.Rotation);
		}
	}
}