using Godot;
using System;

public partial class RandomVelocityOnReady : Node, IBehaviourNode {

	[Export] public float initialVelocityMin;
	[Export] public float initialVelocityMax;

	public void Init(AgentDependencies deps) {
		if (deps.GetNodeOrNull<RigidBody2D>() is {} rb) {
			// rb.Position = new Vector2(5f, 10f);
			// rb.SetAxisVelocity(Vector2.Right * 10f);
			rb.LinearVelocity += new Vector2 {
				X = GD.Randf() - 0.5f,
				Y = GD.Randf() - 0.5f,
			}.Normalized() * (float)GD.RandRange(initialVelocityMin, initialVelocityMax);
		}
	}

    public override void _PhysicsProcess(double delta) {
		
    }
}
