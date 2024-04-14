using Godot;
using System;

public partial class EnemySpawner : Node2D {
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

    public override void _Ready() {
		_gameManager = GetNode<GameManager>("/root/GameManager");
        _playerPos = GetNode<Node2D>("/root/Game/Player").GlobalPosition;

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
			x = _random.RandfRange(0, _screenSize.X - _padding);
			y = _random.RandfRange(0, _screenSize.Y - _padding);
		} while(_playerPos.DistanceTo(new(x, y)) < 150);

		Node2D spawn = GD.Load<PackedScene>(_EnemyNode).Instantiate() as Node2D;
		spawn.Position = new(x, y);
		AddChild(spawn);
	}
}
