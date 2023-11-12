using Godot;

public partial class HideEye : Node, IBehaviourNode
{
	AgentDependencies ad;
	RigidBody2D rb;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!(rb.GetNodeOrNull<AgentDetector>("AgentDetector") is { } ad)) return;
		this.ad.Collision += delegate { CloseEye(); };
	}

	public void CloseEye()
	{
		this.rb.GetNode<TextureRect>("Eye").Hide();
		this.rb.GetNode<TextureRect>("Pupil").Hide();
	}

	public void Init(AgentDependencies deps)
	{
		this.ad = deps;
		if (!(deps.GetNodeOrNull<RigidBody2D>() is { } rb)) return;
		this.rb = rb;
	}
}
