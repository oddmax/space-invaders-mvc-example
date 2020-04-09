using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Presenters
{
	public enum MovementDirection
	{
		Right,
		Left,
		Down
	}

	public class EnemiesMovementSystem
	{
		private class EnemyMovement
		{
			public MovementDirection Direction;
			public float Time;
		}
		
		private int counter;
		private List<EnemyView> enemyViews;
		private IEnumerator coroutine;

		private EnemyMovement currentMovementDirection => movementPattern[currentMovementPatternIndex];
		
		private float nextMovementTime;
		private int currentMovementPatternIndex;

		private EnemyMovement[] movementPattern = new[]
		{
			new EnemyMovement {Direction = MovementDirection.Right, Time = 5f},
			new EnemyMovement {Direction = MovementDirection.Down, Time = 1f},
			new EnemyMovement {Direction = MovementDirection.Left, Time = 5f},
			new EnemyMovement {Direction = MovementDirection.Down, Time = 1f},
		};

		public EnemiesMovementSystem(List<EnemyView> enemies)
		{
			enemyViews = enemies;
		}

		public void Start()
		{
			currentMovementPatternIndex = 0;
			nextMovementTime = Time.time + movementPattern[currentMovementPatternIndex].Time;
			ChangeMovementDirection();
		}
		
		private void ChangeMovementDirection()
		{
			var movement = GetDirection(currentMovementDirection.Direction);
			foreach (var enemyView in enemyViews)
			{
				enemyView.Rigidbody.velocity = movement * 1f * (1 + counter * 0.3f);
			}
		}

		public void Update()
		{
			if (Time.time > nextMovementTime)
			{
				currentMovementPatternIndex++;
				if (currentMovementPatternIndex >= movementPattern.Length)
					currentMovementPatternIndex = 0;
				
				nextMovementTime = Time.time + movementPattern[currentMovementPatternIndex].Time;
				ChangeMovementDirection();
			}
		}

		private Vector3 GetDirection(MovementDirection direction)
		{
			switch (direction)
			{
				case MovementDirection.Down:
					return Vector3.back;
				case MovementDirection.Right:
					return Vector3.right;
				case MovementDirection.Left:
					return Vector3.left;
			}
			return Vector3.zero;
		}

	}
}