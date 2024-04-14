using Godot;
using System;

public partial class ProjectileController : CharacterBody2D {
	static private RandomNumberGenerator _random = new();
	const int _baseSpeed = 300;

	float _speed;
	Vector2? _target;

    public override void _PhysicsProcess(double delta) {
		if(_target == null) {
			return;
		}

		Vector2 target = (Vector2) _target;
		KinematicCollision2D collision = MoveAndCollide((target - Position).Normalized() * _speed * (float) delta);

		if(collision != null) {
			QueueFree();
		}
	}

	public void Go(Vector2 target) {
		_target = target;
		_speed = _random.RandfRange(_baseSpeed, _baseSpeed + (int) GetNode<GameManager>("/root/GameManager").SelectedDifficulty * 100 + 100);
	}
}
