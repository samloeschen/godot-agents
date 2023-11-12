using Godot;
public partial class RandomGoal: Node, IBehaviourNode {
    
    [Export] public float minInterval = 0.5f;
    [Export] public float maxInteral = 2f;
    [Export] public float goalWeight = 1f;
    [Export] public float steerRangeDegrees = 30f;

    float _timer;
    Vector2 _direction;
    AgentGoal _goal;
    RigidBody2D _rb;
    public void Init(AgentDependencies deps) {
        _goal = deps.GetNodeOrNull<AgentGoal>();
        _rb = deps;
        _timer = (float)GD.RandRange(minInterval, maxInteral);
    }


    public override void _Process(double delta) {
        if (!(this is {
            _goal: {} goal,
            _rb: {} rb,
        })) return;

        _timer -= (float)delta;
        if (_timer < 0f) {
            float angle = Mathf.DegToRad((float)GD.RandRange(-steerRangeDegrees, steerRangeDegrees));
            _direction = rb.LinearVelocity.Normalized().Rotate(angle);
            _timer = (float)GD.RandRange(minInterval, maxInteral);
        }
        goal.SetGoal(_direction, goalWeight);
    }
}