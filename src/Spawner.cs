using Godot;
partial class Spawner: Node {

	[Export] public int numSpawnPoints = 32;
	[Export] public uint seed = 1;
	[Export] public Node agentParent;

	[Export] public PackedScene[] scenesToSpawn;
	Vector2[] spawnPts;

	public override void _Ready() {
		// TODO bake these
		spawnPts = new Vector2[numSpawnPoints];
		BlueNoisePoints.GenerateBlueNoiseSamplePoints(ref spawnPts, seed);

		int idx = 0;
		foreach (var pt in spawnPts) {
			var root = scenesToSpawn[idx].Instantiate();

			idx = (idx + 1) % scenesToSpawn.Length;
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

    public override void _Process(double delta) {

		var p = GetViewport().GetVisibleRect().GetCenter().ToVector3();
        DebugDraw.Line(p, p + Vector2.Up.ToVector3() * 10f, Colors.Red, Colors.White);
    }
}
