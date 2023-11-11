using Godot;
partial class Spawner: Node {

    [Export] public int numSpawnPoints = 32;
    [Export] public uint seed = 1;
    [Export] public Node agentParent;

    [Export] public PackedScene sceneToSpawn;
    Vector2[] spawnPts;

    public override void _Ready() {
        // TODO bake these
        spawnPts = new Vector2[numSpawnPoints];
        BlueNoisePoints.GenerateBlueNoiseSamplePoints(ref spawnPts, seed);

        foreach (var pt in spawnPts) {
            var root = sceneToSpawn.Instantiate();
            agentParent.AddChild(root);
            if (root is AgentDependencies deps) {
                if (deps.GetNodeOrNull<RigidBody2D>() is {} rb) {
                    rb.Position = pt * GetViewport().GetVisibleRect().Size;
                } else if (root is Node2D node2D) {
                    node2D.Position = pt * GetViewport().GetVisibleRect().Size;
                }
            }
        }
    }
}