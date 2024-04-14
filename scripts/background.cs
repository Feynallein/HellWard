using Godot;
using System;

public partial class background : Sprite2D{
	public override void _Ready() {
		Scale = GetViewportRect().Size;
	}
}
