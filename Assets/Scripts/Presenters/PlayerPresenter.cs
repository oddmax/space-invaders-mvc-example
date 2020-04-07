
using DefaultNamespace.StaticData;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Presenters
{
	public class PlayerPresenter : MonoBehaviour
	{
		[Inject] 
		private PlayerConfig playerConfig;
		
		private void Reset()
		{
			var playerView = Instantiate(playerConfig.viewPrefab);
		}
	}
}