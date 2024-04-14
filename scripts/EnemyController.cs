using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyController : Node2D {
	[Export(PropertyHint.File, "*.tscn")] String _projectile;
	[Export] float _speed = 0.5f;
	
	float _t = 0;
	bool _spawned = false;

	public override void _Ready() {
		Modulate = new(Modulate, 0);
	}

	public override void _Process(double delta) {
		if(_spawned) {
			return;
		}

		Modulate = new(Modulate, 1 * _t);
		_t += _speed * (float) delta;

		if(1 <= _t) {
			_spawned = true;
			_Shoot();
		}
	}

	private async void _Shoot() {
		Node2D player = GetNode<Node2D>("/root/Game/Player");
		ProjectileController projectile = GD.Load<PackedScene>(_projectile).Instantiate() as ProjectileController;
		projectile.Position = Position;
		GetTree().Root.AddChild(projectile);
		projectile.Go(player.Position);
		await Task.Delay(TimeSpan.FromMilliseconds(2000));
		_FlyAway();
	}

	private void _FlyAway() {
		GD.Print("flown away");
		QueueFree();
	}
}
