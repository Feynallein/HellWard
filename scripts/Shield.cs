using Godot;
using System;

public partial class Shield : Node2D {
	const double _start = Math.PI;
	const double _stop = 2*Math.PI;

	[Export] int _radius = 3;
	Vector2 _playerPos;
	Vector2 _viewPort;
	bool _play = true;
	public override void _Ready() {
		_playerPos = GetNode<Node2D>("/root/Game/Player").GlobalPosition;
		_viewPort = GetViewport().GetVisibleRect().Size;
	}

	public override void _Process(double delta) {
		float t = GetMousePercentage();
		Position = GetPos(t);
		LookAt(_playerPos);
	}

	private float GetMousePercentage() {
		float mousePos = GetGlobalMousePosition().X;
		return Math.Clamp(mousePos / _viewPort.X, 0, 1);
	}

	private Vector2 GetPos(float t) {
		float x = _playerPos.X + _radius * (float) Math.Cos(_start + t * (_stop - _start));
		float y = _playerPos.Y + _radius * (float) Math.Sin(_start + t * (_stop - _start));
		return new(x, y);
	}

	public void Blocked() {
		UpdateSound();
		if(_play) {
			GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
		}

		GetTree().Root.GetNode<HSlider>("Game/CanvasLayer/ManaBar").Value++;
	}

	private void UpdateSound() {
		int volume = GetNode<AudioManager>("/root/AudioManager").Volume;
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").VolumeDb = Math.Min(volume - 80, 20);
		_play = volume != 0;
	}
}
