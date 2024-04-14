using Godot;
using System;

public partial class Blink : Sprite2D {
	[Export] Color _baseColor;	
	[Export] float _lightenedCoef = 1f;

	double _elapsedTime = 0;

	private double pulse(float bpm, float beatToPulseTo) {
		if(_elapsedTime < 7.5) {
			return 0;
		} else if(_elapsedTime < 22) {
			return Math.Pow((_elapsedTime - 7.5) / (22 - 7.5), 3);
		}

		return Math.Clamp(Math.Cos((_elapsedTime * Math.PI * bpm / (beatToPulseTo * 60)) % Math.PI), 0, 1);
	}

	public override void _Process(double delta) {
		_elapsedTime += delta;
		Modulate = _baseColor.Lerp(_baseColor.Lightened(_lightenedCoef), (float) pulse(130, 1));
	}
}
