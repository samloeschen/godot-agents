using System.Collections.Generic;
using System.Diagnostics;
using Godot;
public partial class DebugDraw: Node {

    static List<ImmediateMesh> IMPool = new(16);
    static List<MeshInstance3D> NodePool = new(16);
    static List<Instance> ActiveInstances = new(16);
    static Node root;
    static StandardMaterial3D DefaultMaterial;

// public static void Line(Line2 line, Color color, float duration = 0f) =>
    // Line(line.A.ToVector3(), line.B.ToVector3(), color, duration);

public static void Line(Vector3 A, Vector3 B, Color color, float duration = 0f) =>
    Line(A, B, color, color, duration);
public static void Line(Vector3 A, Vector3 B, Color color1, Color color2, float duration = 0f) {
        var (mesh, node) = GetInstances();
        mesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
        mesh.SurfaceSetColor(color1);

        A = A - root.GetViewport().GetVisibleRect().GetCenter().ToVector3();
        B = B - root.GetViewport().GetVisibleRect().GetCenter().ToVector3();

        A.Y *= -1f;
        B.Y *= -1f;

        mesh.SurfaceAddVertex(A);
        mesh.SurfaceSetColor(color2);
        mesh.SurfaceAddVertex(B);
        mesh.SurfaceEnd();

        ActiveInstances.Add(new Instance {
            mesh = mesh,
            node = node,
            duration = duration,
            frameCount = 0,
        });
    }

    public override void _Ready() {
        root = this;
        DefaultMaterial = new() {
            ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded,
            BlendMode = BaseMaterial3D.BlendModeEnum.Mix,
            VertexColorUseAsAlbedo = true,
            Transparency = BaseMaterial3D.TransparencyEnum.Alpha,
            CullMode = BaseMaterial3D.CullModeEnum.Back
        };
    }


    public override void _Process(double delta) {
        for (int i = ActiveInstances.Count - 1; i >= 0; i--) {
            var inst = ActiveInstances[i];
            if (inst.frameCount > 0 && inst.duration <= 0f) {
                ActiveInstances.RemoveAt(i);
                inst.mesh.ClearSurfaces();
                IMPool.Add(inst.mesh);
                NodePool.Add(inst.node);
                continue; 
            }
            inst.duration -= (float)delta;
            
            inst.frameCount++;
            ActiveInstances[i] = inst;
        }
    }

    static (ImmediateMesh mesh, MeshInstance3D node) GetInstances() {
        ImmediateMesh im;
        MeshInstance3D node;
        if (IMPool.Count > 0) {
            im = IMPool[^1];
            node = NodePool[^1];
            IMPool.RemoveAt(IMPool.Count - 1);
            NodePool.RemoveAt(NodePool.Count - 1);
        } else {
            im = new ImmediateMesh();
            node = new MeshInstance3D();
            node.MaterialOverride = DefaultMaterial;
            root.AddChild(node);
        }
        im.ClearSurfaces();
        node.Mesh = im;
        return (im, node);
    }

    struct Instance {
        public ImmediateMesh mesh;
        public MeshInstance3D node;
        public float duration;
        public int frameCount;
    }
}