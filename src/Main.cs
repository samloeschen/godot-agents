using Godot;
partial class Main: Node {
    [Export] public SubViewport worldViewport;
    [Export] public ColorRect displayRect;
    [Export] public ColorRect backgroundRect;

    public override void _Ready() {
        // set up viewport, backgrounds, etc
        var displayMat = new ShaderMaterial {
            Shader = CustomShaders.CANVAS_BLIT,
        };
        displayMat.SetShaderParameter("src", worldViewport.GetTexture());
        displayRect.Material = displayMat;

        var uvTestMat = new ShaderMaterial {
            Shader = CustomShaders.UV_TEST,
        };
        backgroundRect.Material = uvTestMat;
    }
}