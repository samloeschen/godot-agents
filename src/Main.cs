using Godot;
using System;

partial class Main : Node
{
    RigidBody2D target;

    public override void _Process(double delta)
    {
        var mousePos = GetViewport().GetMousePosition();
        var windowSize = GetViewport().GetVisibleRect().Size;
        var mouse = new Vector2(
            (mousePos.X / windowSize.X) * 128,
            (mousePos.Y / windowSize.Y) * 128
        );

        // if (this.target != null)
        // {
        //     DebugDraw.Line(this.target.Position.ToVector3(), mouse.ToVector3(), Colors.Green);
        // }

        if (Input.IsActionJustPressed("reset")) Reset();

        if (Input.IsActionJustPressed("user-input")) SetTarget(mouse);
        if (Input.IsActionPressed("user-input"))
        {
            if (this.target == null) return;


            var force = mouse - this.target.Position;
            force = force.Normalized();
            this.target.ApplyForce(force * 10);
        }
        else
        {
            this.target = null;
        }
    }

    private void SetTarget(Vector2 pos)
    {
        if (this.target == null)
        {
            this.target = this
                .FindChild("Agents")
                ?.GetChildNodeOfType<RigidBody2D>();
        }
        // There are no agents
        if (this.target == null)
        {
            return;
        }

        foreach (RigidBody2D agent in this.FindChild("Agents").GetChildren())
        {
            var curDist = pos.DistanceTo(this.target.Position);
            var newDist = pos.DistanceTo(agent.Position);
            if (newDist < curDist) this.target = agent;
        }
    }

    private void Reset() {
        var agents = this.FindChild("Agents");
        foreach (Node agent in agents.GetChildren()) {
            agent.QueueFree();
        }

        var spawner = this.FindChild("Spawner") as Spawner;
        spawner._Ready();
        ConnectionManager.Clear();
    }
}
