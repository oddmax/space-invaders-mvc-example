
using JetBrains.Annotations;
using UniRx;
using Zenject;

[UsedImplicitly]
public class PlayerScoreModel : IInitializable
{
	public ReactiveProperty<int> Score;

	public void Initialize()
	{
		Reset();
	}

	public void Reset()
	{
		Score = new ReactiveProperty<int>(0);
	}
}

