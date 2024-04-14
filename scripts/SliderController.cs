using Godot;
using System;

public partial class SliderController : VSlider {
	AudioManager _musicManager;

    public override void _Ready() {
		_musicManager = GetNode<AudioManager>("/root/AudioManager");
		Value = _musicManager.Volume;
    }

    private void _on_value_changed(float value) {
		_musicManager.UpdateSound(value);
	}
}
