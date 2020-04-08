using DefaultNamespace.Signals;
using DefaultNamespace.StaticData;
using Models;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class PlayerPresenter : MonoBehaviour
	{
		[Inject] 
		private PlayerConfig playerConfig;
		
		[Inject] 
		private PlayerShipModel playerShipModel;
		
		[Inject] 
		private SignalBus signalBus;

		[Inject] 
		private DiContainer diContainer;

		private PlayerView playerView;

		private void Start()
		{
			ResetPlayer();
		}

		private void ResetPlayer()
		{
			Destroy(playerView);
			playerView = diContainer.InstantiatePrefab(playerConfig.viewPrefab).GetComponent<PlayerView>();
		}
		
		void Update()
		{
			var time = Time.time;
			// Should we fire a bullet?
			if ((Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space)) && playerShipModel.CanFire(time))
			{
				playerShipModel.SetFireTime(time);
				signalBus.Fire(new SpawnBulletSignal( playerConfig.bulletConfig, playerView.GetWeaponCoordinates(), playerView.GetRotation()));
			}
		}
	}
}