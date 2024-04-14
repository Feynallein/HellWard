using Godot;
using System;

public partial class PlayerController : AnimatedSprite2D {
	public override void _Ready() {

	}

	public void Die() {
		Play("Death");
	}

	private void _on_animation_finished() {
		GD.Print("CALLBACKED");
		if(Animation == "Death") {
			GetNode<AudioManager>("/root/AudioManager").ChangeAudio(null);
			GetTree().ChangeSceneToFile("res://scenes/game_over.tscn");
		}
	}
}
