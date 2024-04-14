using Godot;
using System;
using System.Collections.Generic;

public partial class HeartManager : HBoxContainer {
	[Export(PropertyHint.File, "*.tscn")] String _heartContainer;
	List<HeartContainer> _life = new();

	int _baseLife = 4;
	bool _play = true;
	bool _isInvincible = false;
	float _elapsedTime = 0;
	const float _invincibilityDuration = 1.2f;
	const float _blinkDuration = 0.3f;
	PlayerController _player;
	float _lastBlink = 0;

	public override void _Ready() {
		_player = GetNode<PlayerController>("/root/Game/Player");
		_baseLife += (int) GetNode<GameManager>("/root/GameManager").SelectedDifficulty;
		for(int i = 0; i < _baseLife; i++) {
			HeartContainer container = GD.Load<PackedScene>(_heartContainer).Instantiate() as HeartContainer;
			container.Scale = new(5, 5);
			AddChild(container);
			_life.Add(container);
		}
		
	}

    public override void _Process(double delta) {
		if(!_isInvincible) {
			return;
		}

        if(_invincibilityDuration < _elapsedTime) {
			_isInvincible = false;
			_player.Visible = true;
		} else if(_blinkDuration < _elapsedTime - _lastBlink) {
			_lastBlink = _elapsedTime;
			_player.Visible = !_player.Visible;
		}
		
		_elapsedTime += (float) delta;
    }

    public void Damaged() {
		if(_isInvincible) {
			return;
		}

		UpdateSound();
		if(_play) {
			GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
		}
		int idx = _life.FindLastIndex(e => e.IsFull);
		_life[idx].Update(false);

		if(idx == 0) {
			GetNode<EnemySpawner>("/root/Game/EnemySpawner").OnDeath();
			_player.Die();
		} else {
			_elapsedTime = 0;
			_lastBlink = 0;
			_isInvincible = true;
		}
	}

	private void UpdateSound() {
		int volume = GetNode<AudioManager>("/root/AudioManager").Volume;
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").VolumeDb = Math.Min(volume - 80, 20);
		_play = volume != 0;
	}
}
