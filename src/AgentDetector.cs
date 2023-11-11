using Godot;
using System;
using System.Diagnostics;

public partial class AgentDetector : Area2D {
	public event Action<AgentDependencies> AgentDetected;
    public override void _Ready() {
		BodyShapeEntered += Evt;
    }
    public void Evt(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex) {
		if (body is AgentDependencies agent) {
		 	AgentDetected?.Invoke(agent);
		}
	}
}
