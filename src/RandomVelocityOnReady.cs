using Godot;
using System;

public partial class RandomVelocityOnReady : Node, IBehaviourNode {

	public void Init(AgentDependencies deps) {
		if (deps.GetNodeOrNull<RigidBody2D>() is {} rb) {
			// rb.Position = new Vector2(5f, 10f);
			rb.SetAxisVelocity(Vector2.Right * 10f);
			rb.LinearVelocity += Vector2.Right * 10f;
		}
	}

    public override void _PhysicsProcess(double delta) {
		
    }
}
