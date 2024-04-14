using Godot;
using System;
using System.Threading.Tasks;

public partial class Ultra : PointLight2D {
	bool _play = true;

	public override void _Ready() {
		Visible = false;
		ProcessMode = ProcessModeEnum.Disabled;
	}

	public async void Activated() {
		UpdateSound();
		Visible = true;
		ProcessMode = ProcessModeEnum.Inherit;
		if(_play) {
			GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
		}
		
		await Task.Delay(TimeSpan.FromMilliseconds(2000));
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").Stop();
		ProcessMode = ProcessModeEnum.Disabled;
		Visible = false;
	}

	private void UpdateSound() {
		int volume = GetNode<AudioManager>("/root/AudioManager").Volume;
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").VolumeDb = Math.Min(volume - 80, 20);
		_play = volume != 0;
	}
}
