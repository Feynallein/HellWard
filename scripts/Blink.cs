using Godot;
using System;

public partial class Blink : Sprite2D {
	[Export] Color _baseColor;	
	[Export] float _speed = 0.5f;
	[Export] float _lightenedCoef = 1f;

	float _t = 0;

	bool _up = true;

	public override void _Process(double delta) {
		Modulate = _baseColor.Lerp(_baseColor.Lightened(_lightenedCoef), _t);

		_t += _speed * (float) delta * (_up ? 1 : -1);

		if(1 <= _t) {
			_up = false;
		}

		if(_t <= 0) {
			_up = true;
		}
	}
}
