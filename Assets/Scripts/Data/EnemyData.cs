using DefaultNamespace.StaticData;
using UniRx;

namespace Data
{
	public class EnemyData
	{
		public EnemyConfig Config { get; private set; }
		public ReactiveProperty<int> Hp { get; private set; }

		public EnemyData(EnemyConfig config)
		{
			Config = config;
			Hp = new ReactiveProperty<int>(config.lifeAmount);
		}
	}
}