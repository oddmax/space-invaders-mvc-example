using System;
using System.Collections.Generic;
using Data;
using DefaultNamespace.Signals;
using DefaultNamespace.StaticData;
using Models;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class EnemyPresenter : MonoBehaviour, IInitializable
	{
		[Inject] 
		private GameStateModel gameStateModel;
		
		[Inject] 
		private SignalBus signalBus;
		
		private List<EnemyView> enemies = new List<EnemyView>();
		
		private void Start()
		{
			signalBus.Subscribe<LevelChangedSignal>(ResetEnemies);
			signalBus.Subscribe<SpawnEnemySignal>(OnSpawnEnemy);
		}

		private void OnSpawnEnemy(SpawnEnemySignal spawnEnemySignal)
		{
			CreateEnemy(spawnEnemySignal.EnemyData, spawnEnemySignal.Coordinates);
		}

		private void CreateEnemy(EnemyData enemyData, Vector3 coordinates)
		{
			var enemyView = Instantiate(enemyData.Config.viewPrefab).GetComponent<EnemyView>();
			enemyView.transform.position = coordinates;
			enemies.Add(enemyView);
		}

		private void ResetEnemies()
		{
			foreach (var enemyView in enemies)
			{
				Destroy(enemyView.gameObject);
			}
		}

		public void Initialize()
		{
			Debug.Log("Init!!!");
		}
	}
}