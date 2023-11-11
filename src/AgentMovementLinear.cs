using Godot;
public partial class AgentMovementLinear: Node, IBehaviourNode {
    [Export] public float maxSpeed;
    [Export] public float accel;

    AgentGoal _goal;
    RigidBody2D _rb;
       
    public void Init(AgentDependencies deps) {
        _goal = deps.GetNodeOrNull<AgentGoal>();
        _rb = deps.GetNodeOrNull<RigidBody2D>();
    }

    public override void _PhysicsProcess(double delta) {
        if (this is {
            _goal: {} goal,
            _rb: {} rb,
        }) {
            rb.LinearVelocity += accel * goal.GoalDir;
            var deltaV = accel * goal.GoalDir;
            var currSpd = rb.LinearVelocity.Length();
            if (currSpd + deltaV.Length() < maxSpeed || (rb.LinearVelocity + deltaV).Length() < currSpd) {
                rb.LinearVelocity = rb.LinearVelocity.Normalized() * maxSpeed;
            }
        }
    }
}