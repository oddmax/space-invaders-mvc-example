using UniRx;

namespace Data
{
	public class EnemyData
	{
		public ReactiveProperty<bool> IsDead { get; private set; }
	}
}