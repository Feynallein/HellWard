using Godot;
using System;
using System.Collections.Generic;

public partial class HeartManager : HBoxContainer {
	[Export(PropertyHint.File, "*.tscn")] String _heartContainer;
	List<HeartContainer> _life = new();

	int _baseLife = 4;

	public override void _Ready() {
		_baseLife += (int) GetNode<GameManager>("/root/GameManager").SelectedDifficulty;
		for(int i = 0; i < _baseLife; i++) {
			HeartContainer container = GD.Load<PackedScene>(_heartContainer).Instantiate() as HeartContainer;
			container.Scale = new(5, 5);
			AddChild(container);
			_life.Add(container);
		}
		
	}

	public void Damaged() {
		int idx = _life.FindLastIndex(e => e.IsFull);
		_life[idx].Update(false);

		if(idx == 0) {
			GetNode<EnemySpawner>("/root/Game/EnemySpawner").OnDeath();
			GetNode<PlayerController>("/root/Game/Player").Die();
		}
	}
}
