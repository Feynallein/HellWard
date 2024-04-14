using Godot;
using System;

public partial class ManaBar : HSlider {
	const int _max = 15;
	int _value = 14;

	[Export] Color _baseColor;
	[Export] float _lightenedCoef;

	float _t = 0;
	bool _up = true;
	float _speed = 0.5f;

	public override void _Ready() {
		MaxValue = _max;
		Value = _value;
		GetNode<Label>("Label2").Visible = false;
	}

	public override void _Process(double delta) {
		if(_value == _max) {
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

    public override void _Input(InputEvent @event) {
        if(Input.IsActionPressed("ui_select") && _value == _max) {
			GetNode<Ultra>("/root/Game/Ultra").Activated();
			GetNode<Label>("Label2").Visible = false;
			Value = 0;
		} 
    }

    private void _on_value_changed(float value) {
		_value = (int) value;
		GetNode<Label>("Label").Text = _value + "/" + _max;

		if(_value == _max) {
			GetNode<Label>("Label2").Visible = true;
		}
	}
}
