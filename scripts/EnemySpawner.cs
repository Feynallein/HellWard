using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawner : AnimatedSprite2D {
	const float _padding = 150;

	double _initialDifficulty;
	double _difficultyScale;

	[Export(PropertyHint.File, "*.tscn")] String _EnemyNode;

	static private RandomNumberGenerator _random = new();

	double _elapsedTime = 0;

	double _lastSpawned = 0;

	Vector2 _screenSize;

	GameManager _gameManager;

	Vector2 _playerPos;
	List<Node2D> _spawnedEnemies = new();
	bool _preventSpawn = false;

    public override void _Ready() {
		_gameManager = GetNode<GameManager>("/root/GameManager");
        _playerPos = GetNode<Node2D>("/root/Game/Player").GlobalPosition;

		_screenSize = GetViewport().GetVisibleRect().Size;

		_initialDifficulty = 3 - (int) _gameManager.SelectedDifficulty;
		_difficultyScale = 0.015 * (1 + (int) _gameManager.SelectedDifficulty * 0.005);
    }

    public override void _Process(double delta) {
		_elapsedTime += delta;

		if(_DifficultyCurve(_lastSpawned) < _elapsedTime - _lastSpawned && !_preventSpawn) {
			_Spawn();
			_lastSpawned = _elapsedTime;
		}
	}

	private double _DifficultyCurve(double x) {
		return _initialDifficulty * Math.Exp(-_difficultyScale * x);
	}

	private void _Spawn() {
		Play("summoning");
		float x = 0;
		float y = 0;

		do {
			x = _random.RandfRange(0, _screenSize.X);
			y = _random.RandfRange(0, _screenSize.Y - _padding);
		} while(_playerPos.DistanceTo(new(x, y)) < 250);

		Node2D spawn = GD.Load<PackedScene>(_EnemyNode).Instantiate() as Node2D;
		spawn.GlobalPosition = new(x, y);
		GetTree().Root.AddChild(spawn);
		_spawnedEnemies.Add(spawn);
	}

	private void _on_animation_finished() {
		if(Animation != "idle") {
			Play("idle");
		}
	}

	public void OnDeath() {
		_preventSpawn = true;
		Play("victory");
		_spawnedEnemies.ForEach((e) => {
			if(IsInstanceValid(e)) {
				e.QueueFree();
			}
		});
	}
}
