using Godot;
using System;

public partial class MenuManager : Control {
	[Export] BoxContainer _baseOptions;
	[Export] BoxContainer _difficultyOptions;

    public override void _Ready() {
        ChangeVisibility(true);
    }

    private void _on_play_pressed() {
		ChangeVisibility(false);
	}

	private void _on_quit_pressed() {
		GetTree().Quit();
	}

	private void _on_easy_pressed() {
		_start(GameManager.Difficulty.easy);
	}

	private void _on_medium_pressed() {
		_start(GameManager.Difficulty.medium);
	}

	private void _on_hardcore_pressed() {
		_start(GameManager.Difficulty.hardcore);
	}

	private void _on_go_back_pressed() {
		ChangeVisibility(true);
	}

	private void ChangeVisibility(bool @base) {
		_baseOptions.Visible = @base;
		_difficultyOptions.Visible = !@base;
	}


	private void _start(GameManager.Difficulty difficulty) {
		GetNode<GameManager>("/root/GameManager").SetDifficulty(difficulty);
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
}
