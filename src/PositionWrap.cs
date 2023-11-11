using Godot;

public partial class PositionWrap : Node, IBehaviourNode
{
	private AgentDependencies deps;

	public void Init(AgentDependencies deps)
	{
		this.deps = deps;
	}

	public override void _Process(double _delta)
	{
		return;
		if (!(deps.GetNodeOrNull<RigidBody2D>() is { } rb)) return;


		var screen = GetViewport().GetVisibleRect().Size;
		var x = rb.Position.X;
		var y = rb.Position.Y;

		if (x > screen.X) x -= screen.X;
		if (y > screen.Y) y -= screen.Y;

		if (x != rb.Position.X || y != rb.Position.Y)
		{
			rb.Position = new Vector2(x, y);
		}
	}
}
