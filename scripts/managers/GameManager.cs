using Godot;
using System;

public partial class GameManager : Node {
	public enum Difficulty {easy, medium, hardcore};

	private Difficulty? _difficulty = Difficulty.easy;

	public Difficulty? SelectedDifficulty => _difficulty;

	public void SetDifficulty(Difficulty difficulty) {
		_difficulty = difficulty;
		GetNode<AudioManager>("/root/AudioManager").ChangeAudio((int) _difficulty);
	}
}
