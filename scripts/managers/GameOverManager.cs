using Godot;
using System;

public partial class GameOverManager : Control {
    public override void _Ready() {
        GetNode<Label>("/root/Control/TimeHeader/Time").Text = ": " + GetNode<GameManager>("/root/GameManager").Timer + "s";
    }

    private void _on_retry_pressed() {
		GetNode<GameManager>("/root/GameManager").StartTimer();
	}

	private void _on_main_menu_pressed() {
		GetNode<GameManager>("/root/GameManager").ToMenu();
	}
}
