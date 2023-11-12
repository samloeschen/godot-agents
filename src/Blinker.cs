using Godot;
using System;

public partial class Blinker : Node, IBehaviourNode
{
	[Export(PropertyHint.Range, "0,2")] public double waitTime;

	TextureRect eye;
	TextureRect pupil;
	Timer timer;

	public override void _PhysicsProcess(double delta)
	{
		if (GD.Randf() < 0.005f) Blink();
		if (!this.timer.IsStopped())
		{
			var scale = this.pupil.Scale;
			var timePercentage = this.timer.TimeLeft / this.timer.WaitTime;

			var newY = (float)((Math.Cos(timePercentage * Math.PI * 2) + 1) / 2);

			this.eye.Scale = new Vector2(this.eye.Scale.X, newY);
			this.pupil.Scale = new Vector2(this.pupil.Scale.X, newY);
		}
	}

	public void Blink()
	{
		if (this.timer.IsStopped()) this.timer.Start();
	}

	public void StopBlinking() {
		this.timer.Stop();
	}

	public void Init(AgentDependencies deps)
	{
		if (deps.GetNodeOrNull<RigidBody2D>() is { } rb)
		{
			this.pupil = rb.GetNode<TextureRect>("Pupil");
			this.eye = rb.GetNode<TextureRect>("Eye");
			this.timer = (Timer)rb.FindChild("BlinkTimer");
			//this.timer.Timeout += StopBlinking;
		}
	}
}
