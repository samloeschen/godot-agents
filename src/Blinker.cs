using Godot;
using System;

public enum BlinkAllowedAxes
{
	X,
	Y,
	XandY,
}

public partial class Blinker : Node, IBehaviourNode
{
	[Export(PropertyHint.Range, "0,1")] public double blinkChance;
	[Export] public double blinkSpeed = 1.0;
	[Export] public BlinkAllowedAxes allowedAxes;

	TextureRect eye;
	TextureRect pupil;
	double blinkState;
	bool blinking;
	Action BlinkFunc;

	public override void _PhysicsProcess(double delta)
	{
		if (this.blinking)
		{
			this.BlinkFunc?.Invoke();
		}
		else if (GD.Randf() < blinkChance)
		{
			GD.Print("Starting blinking");
			this.blinking = true;
			switch (this.allowedAxes)
			{
				case BlinkAllowedAxes.X:
					this.BlinkFunc += BlinkX;
					break;
				case BlinkAllowedAxes.Y:
					this.BlinkFunc += BlinkY;
					break;
				case BlinkAllowedAxes.XandY:
					if (GD.Randf() < 0.5) {
						this.BlinkFunc += BlinkX;
					} else {
						this.BlinkFunc += BlinkY;
					}
					break;
			}
		}

		if (this.blinkState >= 1.0) StopBlinking();
		this.blinkState += delta * this.blinkSpeed;
	}

	private void BlinkX()
	{
		var newScale = (float)((Math.Cos(this.blinkState * Math.PI * 2) + 1) / 2);
		var eyeY = this.eye.Scale.Y;
		var pupilY = this.pupil.Scale.Y;
		this.eye.Scale = new Vector2(newScale, eyeY);
		this.pupil.Scale = new Vector2(newScale, pupilY);
	}

	private void BlinkY()
	{
		var newScale = (float)((Math.Cos(this.blinkState * Math.PI * 2) + 1) / 2);
		var eyeX = this.eye.Scale.X;
		var pupilX = this.pupil.Scale.X;
		this.eye.Scale = new Vector2(eyeX, newScale);
		this.pupil.Scale = new Vector2(pupilX, newScale);
	}

	public void StopBlinking()
	{
		this.blinking = false;
		this.blinkState = 0.0;
		this.BlinkFunc = null;
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
