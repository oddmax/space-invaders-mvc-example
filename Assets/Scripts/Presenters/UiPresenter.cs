using Data;
using DefaultNamespace.Signals;
using Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class UiPresenter : MonoBehaviour
	{
		[Inject] 
		private GameStateModel gameStateModel;
		
		[Inject] 
		private SignalBus signalBus;
		
		[Inject] 
		private PlayerScoreModel playerScoreModel;
		
		[Inject] 
		private PlayerShipModel playerShipModel;

		public Text scoreText;
		public Text livesText;
		public Button restartButton;
		public Text gameOverText;

		private void Start()
		{
			signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
			
			playerScoreModel.Score.SubscribeToText(scoreText);
			playerShipModel.Hp.SubscribeToText(livesText);

			restartButton.onClick.AddListener(OnRestartClick);
		}

		private void OnRestartClick()
		{
			gameStateModel.State = GameState.LEVEL;
			signalBus.Fire<ChangeLevelSignal>(new ChangeLevelSignal(0));
		}

		private void OnGameStateChanged(GameStateChangedSignal gameStateChangedSignal)
		{
			restartButton.gameObject.SetActive(gameStateChangedSignal.GameState != GameState.LEVEL);
		}
	}
}