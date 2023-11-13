using Godot;
using System;

public partial class ColorRect : Godot.ColorRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	float t = 0f;
	public override void _Process(double delta) {
		t += (float)delta;
		float a = Mathf.SmoothStep(1f, 4f, t);
		if (Material is ShaderMaterial mat) {
			mat.SetShaderParameter("fun_alpha", a);
		}
	}
}
