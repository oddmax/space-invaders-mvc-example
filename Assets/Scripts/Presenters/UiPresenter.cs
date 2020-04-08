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
		public Text levelText;
		public Button restartButton;
		public Text gameOverText;

		private void Start()
		{
			signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
			signalBus.Subscribe<LevelChangedSignal>(OnLevelChanged);
			
			playerScoreModel.Score.SubscribeToText(scoreText);
			playerShipModel.Hp.SubscribeToText(livesText);

			

			restartButton.onClick.AddListener(OnRestartClick);
		}

		private void OnLevelChanged(LevelChangedSignal obj)
		{
			UpdateLevelText();
		}

		private void UpdateLevelText()
		{
			levelText.text = (gameStateModel.CurrentLevelIndex + 1).ToString();
		}

		private void OnRestartClick()
		{
			gameStateModel.State = GameState.LEVEL;
			signalBus.Fire<ChangeLevelSignal>(new ChangeLevelSignal(0));
		}

		private void OnGameStateChanged(GameStateChangedSignal gameStateChangedSignal)
		{
			restartButton.gameObject.SetActive(gameStateChangedSignal.GameState != GameState.LEVEL);
			gameOverText.gameObject.SetActive(gameStateChangedSignal.GameState == GameState.GAME_OVER);
		}
	}
}