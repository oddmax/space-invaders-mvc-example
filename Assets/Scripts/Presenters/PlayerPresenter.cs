using DefaultNamespace.StaticData;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class PlayerPresenter : MonoBehaviour
	{
		[Inject] 
		private PlayerConfig playerConfig;

		[Inject] 
		private DiContainer diContainer;

		private void Start()
		{
			ResetPlayer();
		}

		private void ResetPlayer()
		{
			var playerView= diContainer.InstantiatePrefab(playerConfig.viewPrefab);
			
		}
	}
}