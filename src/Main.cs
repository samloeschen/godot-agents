using Godot;
using System;

partial class Main : Node
{
    RigidBody2D target;

    public override void _Process(double delta)
    {
        var mouse = GetViewport().GetMousePosition();

        if (Input.IsActionJustPressed("user-input")) SetTarget(mouse);
        if (Input.IsActionPressed("user-input")) {
            if (this.target == null) {
                GD.Print("No target!");
                return;
            }

            GD.Print($"{mouse} - {this.target.Position}");
            var force = mouse - this.target.Position;
            force = force.Normalized();
            this.target.ApplyForce(force * 100);
        } else {
            this.target = null;
        }
    }

    private void SetTarget(Vector2 pos)
    {
        GD.Print($"Getting closest agent to: {pos}");
        if (this.target == null)
        {
            this.target = this
                .FindChild("Agents")
                ?.GetChildNodeOfType<RigidBody2D>();
        }
        // There are no agents
        if (this.target == null)
        {
            GD.Print("There are no agents");
            return;
        }

        foreach (RigidBody2D agent in this.FindChild("Agents").GetChildren())
        {
            var curDist = pos.DistanceTo(this.target.Position);
            var newDist = pos.DistanceTo(agent.Position);
            if (newDist < curDist) {
                GD.Print($"Accepting {agent.Position}");
                this.target = agent;
            } else {
                GD.Print($"Rejecting {agent.Position}");
            }
        }
    }
}
