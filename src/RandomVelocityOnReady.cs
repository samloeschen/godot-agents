using Godot;
using System;

public partial class RandomVelocityOnReady : Node, IBehaviourNode {
	public void Init(AgentDependencies deps) {
		if (deps.GetNodeOrNull<RigidBody2D>() is {} rb) {
			rb.SetAxisVelocity(Vector2.Right);
		}
	}
}
