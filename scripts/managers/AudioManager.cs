using Godot;
using System;

public partial class AudioManager : AudioStreamPlayer {
	public const int defaultVolume = 0; //50;

	private int _volume;
	public int Volume => _volume;

	[Export] AudioStream _medium;

	public override void _Ready() {
		UpdateSound(defaultVolume);

		if(_volume != 0) {
			Play();
		}
	}

	public void UpdateSound(float value) {
		_volume = (int) value;
		VolumeDb = _volume - 75;

		if(_volume == 0 && Playing) {
			Stop();
		} else if(_volume != 0 && !Playing) {
			Play();
		}
	}

	public void ChangeAudio(int? audio) {
		Stream = audio switch {
			null => null,
			_ => _medium,
		};
	}
}
