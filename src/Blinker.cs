using Godot;
using System;

public partial class Blinker : Node, IBehaviourNode
{
	[Export(PropertyHint.Range, "0,1")] public double blinkChance;
	[Export] public double blinkSpeed = 1.0;

	TextureRect eye;
	TextureRect pupil;
	double blinkState;
	bool blinking;

	public override void _PhysicsProcess(double delta)
	{
		if (GD.Randf() < blinkChance) Blink();

		if (this.blinkState >= 1.0) StopBlinking();
		if (this.blinking)
		{
			var scale = this.pupil.Scale;
			var newY = (float)((Math.Cos(this.blinkState * Math.PI * 2) + 1) / 2);

			this.eye.Scale = new Vector2(this.eye.Scale.X, newY);
			this.pupil.Scale = new Vector2(this.pupil.Scale.X, newY);
		}

		this.blinkState += delta * this.blinkSpeed;
	}

	public void Blink()
	{
		if (!this.blinking) this.blinking = true;
	}

	public void StopBlinking()
	{
		this.blinking = false;
		this.blinkState = 0.0;
	}

	public void Init(AgentDependencies deps)
	{
		this.blinkState = 0.0;
		this.blinking = false;

		if (deps.GetNodeOrNull<RigidBody2D>() is { } rb)
		{
			this.pupil = rb.GetNode<TextureRect>("Pupil");
			this.eye = rb.GetNode<TextureRect>("Eye");
		}
	}
}
