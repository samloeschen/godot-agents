using Godot;
using System;
using System.Collections.Generic;
public partial class AgentConnector: Area2D, IBehaviourNode {
    [Export] public Godot.Collections.Array<int> validPolarities = new Godot.Collections.Array<int>();
    [Export] public int maxConnections = 1;
    [Export] public bool overrideAgentPolarity = false;
    [Export] public int customPolarity = -1;

    List<AgentConnector> _activeConns;
    AgentDependencies _deps;
    int? _polarity;
    public int? GetPolarity() => _polarity;

    public AgentDependencies GetAgent() => _deps;

    public Vector2 GetVelocity() => _deps.LinearVelocity;

    // this passes the OTHER agent, not itself
    public event Action<AgentDependencies> OnConnected;

    public void Init(AgentDependencies deps) {
        _deps = deps;
        if (overrideAgentPolarity) {
            _polarity = customPolarity;
        }
        else if (deps.GetNodeOrNull<AgentPolarity>() is {} ap) {
            _polarity = ap.polarity;
        } else {
            _polarity = null;
        }
    }

    public override void _Ready() {
        _activeConns = new List<AgentConnector>(32);
        AreaEntered += Entered;
        // AreaExited += Exited;
    }

    void Entered(Area2D area) {
        if (area is AgentConnector conn) {
            if (_activeConns.Contains(conn)) return;
            var other = conn.GetAgent();
            if (other == _deps) return;

            if (conn._polarity is {} polarity) {
                if (validPolarities.Contains(polarity)) {
                    OnConnected?.Invoke(other);
                    ConnectionManager.CreateConnection(this, conn);
                }
            }
        }
    }
}