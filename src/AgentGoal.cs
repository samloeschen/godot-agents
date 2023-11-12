using Godot;
using System.Collections.Generic;
using System.Diagnostics;
public partial class AgentGoal : Node, IBehaviourNode {
    [Export] float goalAccel = 0.5f;
    public Vector2 GoalDir => _evaluatedGoal;
    public float Throttle => _goalThrottle;
    float _goalThrottle = 0f;

    Vector2 _evaluatedGoal;
    List<Goal> _currentGoals;

    public override void _Ready() {
        _currentGoals = new List<Goal>(32);
        _evaluatedGoal = new Vector2((float)GD.Randf() - 0.5f, (float)GD.Randf() - 0.5f).Normalized();
    }

    public override void _Process(double delta) {
        Vector2 newGoal = Vector2.Zero;
        float totalWeight = 0f;
        foreach (var goal in _currentGoals) {
            totalWeight += goal.weight;
        }
        float throttleTarget;
        if (totalWeight > 0f) {
            throttleTarget = 1f;
            foreach (var goal in _currentGoals) {
                newGoal += goal.direction * (goal.weight / totalWeight);
            }   
            _evaluatedGoal = newGoal;
        } else {
            throttleTarget = 0f;
        }
        _goalThrottle = Mathf.Lerp(_goalThrottle, throttleTarget, 1f - Mathf.Exp((float)delta * goalAccel));
        // DebugDraw.Line(Vector3.Zero, Vector2.Up.ToVector3() * _goalThrottle * 10f, Colors.Red);

        _currentGoals.Clear();
    }

    public void Init(AgentDependencies deps) {
        // _evaluatedGoal = Vector2.Zero;
    }

    public void SetGoal(Vector2 direction, float weight) {
        _currentGoals.Add(new Goal {
            direction = direction,
            weight = weight,
        });
    }
}

struct Goal {
    public Vector2 direction;
    public float weight;
}