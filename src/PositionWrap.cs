using Godot;

public partial class PositionWrap : Node, IBehaviourNode
{
	private AgentDependencies deps;
	private Vector2 agentSize;

	public void Init(AgentDependencies deps)
	{
		this.deps = deps;
		if (!(deps.GetNodeOrNull<ColorRect>() is { } rb)) return;

		agentSize = rb.GetRect().Size;
	}

	public override void _Process(double _delta)
	{
		if (!(deps.GetNodeOrNull<ColorRect>() is { } rb)) return;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!(this.deps.GetNodeOrNull<RigidBody2D>() is { } rb)) return;

		var screen = GetViewport().GetVisibleRect().Size;
		var x = rb.Position.X;
		var y = rb.Position.Y;

		var halfX = this.agentSize.X / 2;
		var halfY = this.agentSize.Y / 2;

		if (x > screen.X + halfX) x -= screen.X + this.agentSize.X;
		if (x < 0 - halfX) x += screen.X + this.agentSize.X;

		if (y > screen.Y + halfY) y -= screen.Y + this.agentSize.Y;
		if (y < 0 - halfY) y += screen.Y + this.agentSize.Y;

		rb.Position = new Vector2(x, y);
	}
}
