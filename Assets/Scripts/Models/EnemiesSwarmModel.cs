using System;
using System.Collections.Generic;
using Data;
using DefaultNamespace.Signals;
using DefaultNamespace.StaticData;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Models
{
	[UsedImplicitly]
	public class EnemiesSwarmModel : IInitializable, IDisposable
	{
		[Inject] 
		private SignalBus signalBus;
		
		private List<EnemyData> enemies = new List<EnemyData>();
		
		public void Create(LevelConfig levelConfig)
		{
			Reset();
			foreach (var configEnemyInfo in levelConfig.enemyInfos)
			{
				var enemyData = new EnemyData(configEnemyInfo.enemyConfig);
				var spawnCoordinates = new Vector3(configEnemyInfo.startPosition.x, 0, configEnemyInfo.startPosition.y);
				
				signalBus.Fire(new SpawnEnemiesSignal(enemyData, spawnCoordinates));
				
				enemyData.IsDead.Where(isDead => isDead == true)
					.Subscribe(_ => { OnDestroyEnemy(enemyData); });
				
				enemies.Add(enemyData);
			}
		}
		
		public void Reset()
		{
			enemies.Clear();
		}
		
		public bool HasNoEnemies()
		{
			return enemies.Count == 0;
		}

		private void OnDestroyEnemy(EnemyData enemyData)
		{
			enemies.Remove(enemyData);
		}

		public void Initialize()
		{
		}

		public void Dispose()
		{
			Reset();
		}

		
	}
}