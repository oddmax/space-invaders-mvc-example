using Data;
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
			
			//signalBus.Subscribe<LevelChangedSignal>(ResetPlayer);
			signalBus.Subscribe<PlayerHitSignal>(OnPlayerHit);
			signalBus.Subscribe<GameStateChangedSignal>(OnGameSateChange);
		}
		
		private void OnGameSateChange(GameStateChangedSignal gameStateChangedSignal)
		{
			if (gameStateChangedSignal.GameState != GameState.LEVEL)
			{
				if(playerView != null)
					Destroy(playerView.gameObject);
			}
			else
			{
				ResetPlayer();
			}
		}
		
		private void OnPlayerHit(PlayerHitSignal obj)
		{
			playerShipModel.Hp.Value--;
		}

		private void ResetPlayer()
		{
			if(playerView != null)
				Destroy(playerView.gameObject);
			
			playerView = diContainer.InstantiatePrefab(playerConfig.viewPrefab).GetComponent<PlayerView>();
		}
		
		void Update()
		{
			var time = Time.time;
			// Should we fire a bullet?
			if (playerView != null && (Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space)) && playerShipModel.CanFire(time))
			{
				playerShipModel.SetFireTime(time);
				signalBus.Fire(new SpawnBulletSignal( playerConfig.bulletConfig, playerView.GetWeaponCoordinates(), playerView.GetRotation()));
			}
		}
		
		private void OnShipDestroy()
		{
			if (playerView != null)
			{
				Instantiate(playerExplosion, playerView.transform.position, playerView.transform.rotation);
				Destroy(playerView.gameObject);
			}
		}
	}
}