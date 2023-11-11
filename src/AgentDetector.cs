using Godot;
using System;
using System.Diagnostics;
using System.Drawing;

public partial class AgentDetector : Area2D {
	public event Action<AgentDependencies> AgentDetected;
	public event Action<AgentDependencies> AgentLost;

	public float Radius { get; private set; }
    public override void _Ready() {
		BodyShapeEntered += Entered;
		BodyShapeExited += Exited;

		if (GetNode<CollisionShape2D>("CollisionShape2D") is {} shape) {
			Radius = shape.Shape.GetRect().Size.Length() * 0.5f;
		}
    }



    public void Entered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex) {
		if (body is AgentDependencies agent) {
		 	AgentDetected?.Invoke(agent);
		}
	}

	public void Exited(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex) {
		if (body is AgentDependencies agent) {
			AgentLost?.Invoke(agent);
		}
	}
}
