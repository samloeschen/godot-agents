using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Godot;
public partial class ConnectionManager: Node {
    [Export] public float springForce;
    [Export] public float overUnderDamp;

    struct ConnectionInfo {
        public AgentConnector a;
        public AgentConnector b;
        public Vector2 springVel;
        public Vector2 separation;
    }
    static List<ConnectionInfo> ActiveConns = new List<ConnectionInfo>(128);
    static Node instance;

    public override void _Ready() {
        instance = this;
    }

    public override void _Process(double delta) {
        foreach (var conn in ActiveConns) {
            var pa = conn.a.GlobalPosition - GetViewport().GetVisibleRect().Size * 0.5f;
            var pb = conn.b.GlobalPosition - GetViewport().GetVisibleRect().Size * 0.5f;

            pa.Y *= -1f;
            pb.Y *= -1f;

            DebugDraw.Line(pa.ToVector3(), pb.ToVector3(), Colors.Green);
            // GD.Print($"a: { pa } b: { pb}");
        }
    }

    public override void _PhysicsProcess(double delta) {
        Vector2 pa, pb;
        foreach (var conn in ActiveConns) {
            pa = conn.a.GlobalPosition;
            pb = conn.b.GlobalPosition;
            Vector2 displacement = pa - pb;
            
            float dispDist = displacement.Length();
            float distError = dispDist - conn.separation.Length();
            Vector2 relativeVel = conn.a.GetVelocity() - conn.b.GetVelocity();
            
            float d = overUnderDamp * (2f * Mathf.Sqrt(springForce));
            Vector2 accel = -(springForce * displacement.Normalized() * distError) - (d * relativeVel);

            // Vector2 deltaV = -accel;
            conn.a.GetAgent().ApplyForce(accel * (float)delta);
            conn.b.GetAgent().ApplyForce(-accel * (float)delta);
        }
    }

    public static void CreateConnection(AgentConnector a, AgentConnector b) {

        foreach (var conn in ActiveConns) {
            if (conn.a == a || conn.b == b) return;
        }

        float dist = (a.GetAgent().GlobalPosition - b.GetAgent().GlobalPosition).Length();
        ActiveConns.Add(new ConnectionInfo {
            a = a,
            b = b,
            springVel = Vector2.Zero,
            separation = (a.GlobalPosition - b.GlobalPosition) * 0.25f,
        });
    }

    public static void BreakConnection(AgentConnector a, AgentConnector b) {

    }
}