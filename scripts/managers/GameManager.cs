using Godot;
using System;

public partial class GameManager : Node {
	public enum Difficulty {easy, medium, hardcore};

	private Difficulty? _difficulty = null;

	public Difficulty? SelectedDifficulty => _difficulty;

	public void SetDifficulty(Difficulty? difficulty) {
		_difficulty = difficulty;
	}

	float _elaspedTime = 0;

	public void StartTimer() {
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
		GetNode<AudioManager>("/root/AudioManager").Play();
		_elaspedTime = 0;
	}

	public void ToMenu() {
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
		GetNode<AudioManager>("/root/AudioManager").Stop();
	}

    public override void _Process(double delta) {
        _elaspedTime += (float) delta;
    }

	public int Timer => (int) Math.Floor(_elaspedTime);
}
