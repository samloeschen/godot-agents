using Godot;
using System;

public partial class BorderPush : Node, IBehaviourNode
{
    [Export] public float strength = 1.0f;

    RigidBody2D rb;
    ColorRect cr;

    public void Init(AgentDependencies deps)
    {
        if (deps.GetNodeOrNull<RigidBody2D>() is { } rb) this.rb = rb;
        if (deps.GetNodeOrNull<ColorRect>() is { } cr) this.cr = cr;
    }

    public override void _PhysicsProcess(double delta)
    {
        var screen = GetViewport().GetVisibleRect().Size;
        var screenHalfX = screen.X / 2;
        var screenHalfY = screen.Y / 2;
        var x = rb.Position.X;
        var y = rb.Position.Y;
        var halfX = this.cr?.GetRect().Size.X ?? 5f;
        var halfY = this.cr?.GetRect().Size.Y ?? 5f;

        var xforce = Math.Clamp(Math.Abs(x - screenHalfX) - screenHalfX, 0, int.MaxValue);
        var yforce = Math.Clamp(Math.Abs(y - screenHalfY) - screenHalfY, 0, int.MaxValue);
        var force = new Vector2(xforce, yforce);

        if (x > screen.X - halfX) force = new Vector2(-force.X, force.Y);
        if (y > screen.Y - halfY) force += new Vector2(force.X, -force.Y);

        rb.ApplyForce(force * strength);
    }
}
