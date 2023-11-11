using Godot;
using System.Collections.Generic;
public partial class AgentGoal : Node, IBehaviourNode {
    public float returnSpeed;

    public Vector2 GoalDir => _evaluatedGoal;
    Vector2 _evaluatedGoal;
    List<Goal> _currentGoals;

    public override void _Ready() {
        _currentGoals = new List<Goal>(32);
    }

    public override void _Process(double delta) {
        Vector2 newGoal = Vector2.Zero;
        float totalWeight = 0f;
        foreach (var goal in _currentGoals) {
            totalWeight += goal.weight;
        }
        if (totalWeight > 0f) {
            foreach (var goal in _currentGoals) {
                newGoal += goal.direction * (goal.weight / totalWeight);
            }
            _evaluatedGoal = newGoal;
        } else {
            _evaluatedGoal = _evaluatedGoal.Lerp(Vector2.Zero, 1f - Mathf.Exp((float)delta * returnSpeed));
        }
    }

    public void Init(AgentDependencies deps) {
        _evaluatedGoal = Vector2.Zero;
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