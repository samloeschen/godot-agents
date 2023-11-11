using Godot;
public partial class AgentPolarity : Node, IBehaviourNode {
	[Export] public int polarity;

	RigidBody2D _rb;
	public Vector2 GetVelocity() => _rb.LinearVelocity;
	public Vector2 GetPosition() => _rb.Position;
	public void Init(AgentDependencies deps) {
		_rb = deps.GetNodeOrNull<RigidBody2D>();
	}
}
