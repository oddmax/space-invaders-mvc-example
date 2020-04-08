using DefaultNamespace.Signals;
using DefaultNamespace.StaticData;
using Models;
using UniRx;
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
		
		[SerializeField]
		public GameObject playerExplosion;

		private PlayerView playerView;

		private void Start()
		{
			playerShipModel.IsDead.Where(isDead => isDead == true)
				.Subscribe(_ => OnShipDestroy());
			
			signalBus.Subscribe<LevelChangedSignal>(ResetPlayer);
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
			if ((Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space)) && playerShipModel.CanFire(time) && playerView != null)
			{
				playerShipModel.SetFireTime(time);
				signalBus.Fire(new SpawnBulletSignal( playerConfig.bulletConfig, playerView.GetWeaponCoordinates(), playerView.GetRotation()));
			}
		}
		
		private void OnShipDestroy()
		{
			Destroy(playerView);
			Instantiate(playerExplosion, playerView.transform.position, playerView.transform.rotation);
		}
	}
}