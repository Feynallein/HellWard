using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyController : AnimatedSprite2D {
	[Export(PropertyHint.File, "*.tscn")] String _projectile;
	[Export] float _speed = 0.5f;
	
	float _t = 0;
	bool _spawned = false;
	bool _flyAway = false;
	Vector2 _playerPos;

	bool _play = true;

	public override void _Ready() {
		Modulate = new(Modulate, 0);
		_playerPos = GetNode<Node2D>("/root/Game/Player").GlobalPosition;
	}

	public override void _Process(double delta) {
		if(_flyAway) {
			Position += -(_playerPos - Position).Normalized() * (float) delta * 150;
		}

		if(_spawned) {
			return;
		}

		Modulate = new(Modulate, 1 * _t);
		_t += _speed * (float) delta;

		if(1 <= _t) {
			_spawned = true;
			_WaitToShoot();
		}
	}

	private void _on_visible_on_screen_notifier_2d_screen_exited() {
		QueueFree();
	}

	private async void _WaitToShoot() {
		await Task.Delay(TimeSpan.FromMilliseconds(1000));

		if(IsInstanceValid(this)) {
			Play("Attack");
		}
	}

	private void UpdateSound() {
		int volume = GetNode<AudioManager>("/root/AudioManager").Volume;
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").VolumeDb = Math.Min(volume - 80, 20);
		_play = volume != 0;
	}

	private async void _Shoot() {
		UpdateSound();
		if(_play) {
			GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
		}
		
		Node2D player = GetNode<Node2D>("/root/Game/Player");
		ProjectileController projectile = GD.Load<PackedScene>(_projectile).Instantiate() as ProjectileController;
		projectile.Position = Position;
		GetTree().Root.AddChild(projectile);
		projectile.Go(player.Position);
		await Task.Delay(TimeSpan.FromMilliseconds(2000));
		
		if(IsInstanceValid(this)) {
			_flyAway = true;
		}
		
	}

	private void _on_animation_finished() {
		if(Animation == "Attack") {
			_Shoot();
			Play("Idle");
		}
	}
}
