using Godot;
using System;

public partial class EnemySpawner : Node2D {
	double _initialDifficulty;
	double _difficultyScale;

	[Export(PropertyHint.File, "*.tscn")] String _EnemyNode;

	static private RandomNumberGenerator _random = new();

	double _elapsedTime = 0;

	double _lastSpawned = 0;

	Rect2 _spawnArea;
	Vector2 _screenSize;
	GameManager _gameManager;

    public override void _Ready() {
		_gameManager = GetNode<GameManager>("/root/GameManager");
        CollisionShape2D shape = GetNode<CollisionShape2D>("Area/SpawnArea");

		_spawnArea = shape.Shape.GetRect();
		_screenSize = GetViewport().GetVisibleRect().Size;

		_initialDifficulty = 3 - (int) _gameManager.SelectedDifficulty;
		_difficultyScale = 0.015 * (1 + (int) _gameManager.SelectedDifficulty * 0.005);
    }

    public override void _Process(double delta) {
		_elapsedTime += delta;

		if(_DifficultyCurve(_lastSpawned) < _elapsedTime - _lastSpawned) {
			_Spawn();
			_lastSpawned = _elapsedTime;
		}
	}

	private double _DifficultyCurve(double x) {
		return _initialDifficulty * Math.Exp(-_difficultyScale * x);
	}

	private void _Spawn() {
		float x = 0;
		float y = 0;

		do {
			x = _random.RandfRange(0, _screenSize.X);
			y = _random.RandfRange(0, _screenSize.Y);
		} while(_spawnArea.HasPoint(new(x, y)));

		Node2D spawn = GD.Load<PackedScene>(_EnemyNode).Instantiate() as Node2D;
		spawn.Position = new(x, y);
		AddChild(spawn);
	}
}
