
using JetBrains.Annotations;
using UniRx;
using Zenject;

[UsedImplicitly]
public class PlayerScoreModel : IInitializable
{
	public ReactiveProperty<int> Score;

	public void Initialize()
	{
		Score = new ReactiveProperty<int>(0);
		Reset();
	}

	public void Reset()
	{
		Score.Value = 0;
	}
}

