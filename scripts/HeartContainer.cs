using Godot;
using System;

public partial class HeartContainer : Control {
	[Export] TextureRect _full;
	
	[Export] TextureRect _empty;

	public bool IsFull => _full.Visible;

	public override void _Ready() {
		Update(true);
	}

	public void Update(bool full) {
		_full.Visible = full;
		_empty.Visible = !full;
	}
}
