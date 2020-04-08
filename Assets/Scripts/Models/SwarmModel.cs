using System;
using System.Collections.Generic;
using Data;
using DefaultNamespace.Signals;
using DefaultNamespace.StaticData;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Models
{
	[UsedImplicitly]
	public class SwarmModel : IInitializable, IDisposable
	{
		[Inject] 
		private SignalBus signalBus;
		
		private List<EnemyData> enemies = new List<EnemyData>();
		
		public void Create(LevelConfig levelConfig)
		{
			foreach (var configEnemyInfo in levelConfig.enemyInfos)
			{
				var enemyData = new EnemyData(configEnemyInfo.enemyConfig);
				var spawnCoordinates = new Vector3(configEnemyInfo.startPosition.x, 0, configEnemyInfo.startPosition.y);
				signalBus.Fire(new SpawnEnemySignal(enemyData, spawnCoordinates));
			}
		}
		private void Reset()
		{
			enemies.Clear();
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