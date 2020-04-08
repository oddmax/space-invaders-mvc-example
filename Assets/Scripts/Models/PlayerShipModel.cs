using DefaultNamespace.StaticData;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Models
{
	[UsedImplicitly]
	public class PlayerShipModel : IInitializable
	{
		[Inject] 
		public PlayerConfig playerConfig;

		public ReactiveProperty<int> Hp { get; set; }
		public IReadOnlyReactiveProperty<bool> IsDead { get; private set; }

		private float nextFire = 0.0f;

		public void Initialize()
		{
			Hp = new ReactiveProperty<int>(playerConfig.lifeAmount);
			var hpProperty = Hp.Select(x => x <= 0);
			IsDead = hpProperty.ToReactiveProperty();
			
			Reset();
		}
		
		public void Reset()
		{
			Hp.Value = playerConfig.lifeAmount;
		}

		public bool CanFire(float time)
		{
			return time > nextFire;
		}

		public void SetFireTime(float time)
		{
			nextFire = time + playerConfig.fireRate;
		}
	}
	
}