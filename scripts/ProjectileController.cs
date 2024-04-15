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
			if(collision.GetCollider() is StaticBody2D) {
				StaticBody2D collider = collision.GetCollider() as StaticBody2D;
				PlayerController player = collider.GetParentOrNull<PlayerController>();

				if(player != null) {
					GetTree().Root.GetNode<HeartManager>("Game/CanvasLayer/HeartManager").Damaged();
				}

				QueueFree();
			} else if(collision.GetCollider() is CharacterBody2D) {
				CharacterBody2D collider = collision.GetCollider() as CharacterBody2D;
				Shield shield = collider.GetParentOrNull<Shield>();

				if(shield != null) {
					shield.Blocked();
					QueueFree();
				}
			}	
		}
	}

	public void Go(Vector2 target) {
		LookAt(target);
		_target = target;
		_speed = _random.RandfRange(_baseSpeed, _baseSpeed + (int) GetNode<GameManager>("/root/GameManager").SelectedDifficulty * 100 + 100);
	}
}
