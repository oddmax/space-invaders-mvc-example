using System.Collections.Generic;
using Data;
using DefaultNamespace.Signals;
using Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class EnemyPresenter : MonoBehaviour
	{
		[Inject] 
		private SwarmModel swarmModel;
		
		[Inject] 
		private SignalBus signalBus;
		
		[SerializeField]
		public GameObject playerExplosion;
		
		private readonly List<EnemyView> enemies = new List<EnemyView>();
		
		private EnemiesAttackSystem enemiesAttackSystem;
		private EnemiesMovementSystem enemiesMovementSystem;
		
		private void Start()
		{
			signalBus.Subscribe<LevelChangedSignal>(ResetEnemies);
			signalBus.Subscribe<SpawnEnemiesSignal>(OnSpawnEnemy);
			signalBus.Subscribe<StartLevelSignal>(OnStartLevelSignal);
			signalBus.Subscribe<GameStateChangedSignal>(OnGameSateChange);
			
			enemiesAttackSystem = new EnemiesAttackSystem(signalBus, enemies);
			enemiesMovementSystem = new EnemiesMovementSystem(enemies);
		}

		private void OnGameSateChange(GameStateChangedSignal gameStateChangedSignal)
		{
			if (gameStateChangedSignal.GameState != GameState.LEVEL)
			{
				ResetEnemies();
			}
		}

		private void OnStartLevelSignal()
		{
			enemiesAttackSystem.Start();
			enemiesMovementSystem.Start();
		}

		private void OnSpawnEnemy(SpawnEnemiesSignal spawnEnemiesSignal)
		{
			CreateEnemy(spawnEnemiesSignal.EnemyData, spawnEnemiesSignal.Coordinates);
		}

		private void CreateEnemy(EnemyData enemyData, Vector3 coordinates)
		{
			var enemyView = Instantiate(enemyData.Config.viewPrefab).GetComponent<EnemyView>();
			enemyView.transform.position = coordinates;
			enemyView.Init(enemyData);
			enemyData.IsDead.Where(isDead => isDead == true)
				.Subscribe(_ => { OnDestroyEnemy(enemyData); });
			enemies.Add(enemyView);
			Vector3 movement = new Vector3(1, 0, 0);
		}

		private void OnDestroyEnemy(EnemyData enemyData)
		{
			foreach (var enemyView in enemies)
			{
				if (enemyView.EnemyData == enemyData)
				{
					Instantiate(playerExplosion, enemyView.transform.position, enemyView.transform.rotation);
					Destroy(enemyView.gameObject);
					enemies.Remove(enemyView);
					return;
				}
			}
		}

		private void ResetEnemies()
		{
			foreach (var enemyView in enemies)
			{
				Destroy(enemyView.gameObject);
			}
			enemies.Clear();
		}

		private void Update()
		{
			enemiesAttackSystem.Update();
			enemiesMovementSystem.Update();
		}
	}
}