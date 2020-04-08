using System;
using System.Collections.Generic;
using DefaultNamespace.StaticData;
using UnityEngine;

namespace DefaultNamespace.Presenters
{
	public class BulletsPresenter : MonoBehaviour
	{
		private List<BulletView> bullets;
		
		public void SpawnBullet(BulletConfig bulletConfig, Vector3 position, Quaternion rotation)
		{
			BulletView bulletView = Instantiate(bulletConfig.viewPrefab,position, rotation).GetComponent<BulletView>();
			bulletView.Init(bulletConfig);
			bulletView.OnCollide += BulletViewOnOnCollide;
		}

		private void BulletViewOnOnCollide(BulletConfig bulletConfig, Collider other)
		{
			if (this.CompareTag("Bullet") && other.CompareTag("Bullet"))
				return;

			// ignore collision with Enemy or Boundary
			if (other.name=="Boundary")
				return;
			
			Destroy(other.gameObject);
			Destroy(gameObject);
		}

		private void Update()
		{
			for (int i = 0; i < bullets.Count; i++)
			{
				var bulletView = bullets[i];
				bulletView.Rigidbody.velocity = transform.forward * bulletView.Config.speed;
			}
		}
		
		public void RemoveBullet(BulletView bulletView)
		{
			bullets.Remove(bulletView);
			Destroy(bulletView);
		}
	}
}