using System.Collections.Generic;
using DefaultNamespace.Signals;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class EnemiesAttackSystem
	{
		private SignalBus signalBus;
		private List<EnemyView> enemyViews;

		private float nextShootingTime;

		public EnemiesAttackSystem(SignalBus signalBus, List<EnemyView> enemies)
		{
			this.signalBus = signalBus;
			enemyViews = enemies;
		}

		public void Start()
		{
			nextShootingTime = Time.time;
		}

		public void Update()
		{
			if (Time.time > nextShootingTime)
			{
				Shoot();
			}
		}

		private void Shoot()
		{
			if(enemyViews.Count == 0) 
				return;
			
			var randomEnemyIndex = Random.Range(0, enemyViews.Count - 1);
			Debug.Log(">>>" + randomEnemyIndex);
			var enemyView = enemyViews[randomEnemyIndex];
			signalBus.Fire(new SpawnBulletSignal(enemyView.EnemyData.Config.bulletConfig, enemyView.GetWeaponCoordinates(), enemyView.GetRotation()));
			nextShootingTime = nextShootingTime + Random.Range(1, 2);
		}
	}
}