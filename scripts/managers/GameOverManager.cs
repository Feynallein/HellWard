using Godot;
using System;

public partial class GameOverManager : Control {
	private void _on_retry_pressed() {
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}

	private void _on_main_menu_pressed() {
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}
}
