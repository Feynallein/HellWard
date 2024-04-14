using Godot;
using System;

public partial class ProjectileController : Node2D {
	static private RandomNumberGenerator _random = new();
	const int _baseSpeed = 3;

	float _speed;
	Vector2? _target;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if(_target == null) {
			return;
		}

		Vector2 target = (Vector2) _target;

		Position += (target - Position).Normalized() * _speed;
	}

	public void Go(Vector2 target) {
		_target = target;
		_speed = _random.RandfRange(_baseSpeed, _baseSpeed + (int) GetNode<GameManager>("/root/GameManager").SelectedDifficulty);
	}
}
