using DefaultNamespace.StaticData;
using JetBrains.Annotations;
using UniRx;
using Zenject;

namespace Models
{
	[UsedImplicitly]
	public class PlayerShipModel : IInitializable
	{
		[Inject]
		public PlayerConfig playerConfig;
		public ReactiveProperty<int> Hp  { get; private set; }
		public IReadOnlyReactiveProperty<bool> IsDead { get; private set; }
		
		public void Initialize()
		{
			Hp = new ReactiveProperty<int>(playerConfig.lifeAmount);
			var hpProperty = Hp.Select(x => x <= 0);
			IsDead = hpProperty.ToReactiveProperty();	
		}
	}
	
}