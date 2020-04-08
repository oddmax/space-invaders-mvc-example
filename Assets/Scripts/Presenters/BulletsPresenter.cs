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
			if (bulletView.CompareTag("Bullet") && other.CompareTag("EnemyBullet"))
				return;
			
			if (bulletView.CompareTag("EnemyBullet") && other.CompareTag("Bullet"))
				return;

			if (bulletView.CompareTag("EnemyBullet") && other.CompareTag("Player"))
			{
				signalBus.Fire(new PlayerHitSignal());
				RemoveBullet(bulletView);
			}

			if (bulletView.CompareTag("Bullet") && other.CompareTag("Enemy"))
			{
				signalBus.Fire(new EnemyHitSignal(other.GetComponent<EnemyView>().EnemyData));
				RemoveBullet(bulletView);
			}
		}
		
		private void BulletViewOnOnCollideExit(BulletView bulletView, Collider other)
		{
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
				bulletView.Rigidbody.velocity = bulletView.transform.forward * bulletView.Config.speed;
			}
		}
		
		private void RemoveBullet(BulletView bulletView)
		{
			bullets.Remove(bulletView);
			Destroy(bulletView.gameObject);
		}
		
		private void OnSpawnBullet(SpawnBulletSignal spawnBulletSignal)
		{
			SpawnBullet(spawnBulletSignal.BulletConfig, spawnBulletSignal.Coordinates, spawnBulletSignal.Rotation);
		}
	}
}