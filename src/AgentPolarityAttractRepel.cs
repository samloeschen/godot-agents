using System.Collections.Generic;
using Godot;
public partial class AgentPolarityAttractRepel: Node, IBehaviourNode {
    [Export] public int polarity;
    [Export] public float weight = 1f;
    AgentGoal _goal;
    AgentDetector _detect;
    RigidBody2D _rb;
    List<AgentPolarity> _nearbyPolarities;
    
    public void Init(AgentDependencies deps) {
        _goal = deps.GetNodeOrNull<AgentGoal>();
        _detect = deps.GetNodeOrNull<AgentDetector>();
        _rb = deps.GetNodeOrNull<RigidBody2D>();
        _nearbyPolarities = new List<AgentPolarity>(32);
        if (_detect != null) {
            _detect.AgentDetected += (AgentDependencies a) => {
                if (a.GetNodeOrNull<AgentPolarity>() is {} polarity) {
                    if (!_nearbyPolarities.Contains(polarity) && polarity.polarity == this.polarity) {
                        _nearbyPolarities.Add(polarity);
                    }
                }
            }; 
            _detect.AgentDetected += (AgentDependencies a) => {
                if (a.GetNodeOrNull<AgentPolarity>() is {} polarity) {
                    if (_nearbyPolarities.Contains(polarity)) {
                        _nearbyPolarities.Remove(polarity);
                    }
                }
            };
        }
    }

    public override void _Process(double delta) {
        if (this is {
            _goal: {} goal,
            _detect: {} detect,
            _rb: {} rb,
        }) {
            var dir = Vector2.Zero;
            var radius = detect.Radius;
            var maxWeight = 0f;
            for (int i = 0; i < _nearbyPolarities.Count; i++) {
                var offset = rb.Position - _nearbyPolarities[i].GetPosition();
                float weight = 1f - Mathf.Clamp(offset.Length() / radius, 0f, 1f);
                maxWeight = Mathf.Max(weight, maxWeight);
                dir += offset.Normalized() * weight;
            }
            dir = dir.Normalized();
            goal.SetGoal(dir, maxWeight * weight);
        }
    }
}