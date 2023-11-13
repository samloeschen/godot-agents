using Godot;
using System;

public partial class Title : RichTextLabel {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	float t = 0f;
	public override void _Process(double delta) {

		t += (float)delta;
		float a = Mathf.SmoothStep(0f, 1f, t) - Mathf.SmoothStep(5f, 7f, t);
		var mod = this.SelfModulate;
		mod.A = a;
		this.SelfModulate = mod;
	}
}
