using DefaultNamespace.StaticData;
using Models;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class LevelPresenter : MonoBehaviour
	{
		[Inject] 
		private GameStateModel gameStateModel;
		
		private void InitLevel()
		{
			LevelConfig levelConfig = gameStateModel.GetCurrentLevelConfig();
			
			CreateEnemies(levelConfig);
		}
		
		private void CreateEnemies(LevelConfig levelConfig)
		{
			foreach (EnemyLocation enemyLocation in levelConfig.enemyInfos)
			{
				CreateEnemy(enemyLocation);
			}
		}

		private void CreateEnemy(EnemyLocation enemyLocation)
		{
			var enemyView = Instantiate(enemyLocation.enemyConfig.viewPrefab);
			enemyView.transform.position = new Vector3(enemyLocation.startPosition.X, enemyLocation.startPosition.Y);
		}
	}
}