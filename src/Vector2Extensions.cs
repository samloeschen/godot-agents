using Godot;
public static class Vector2Extensions {
    public static Vector3 ToVector3(this Vector2 v, float z = 0f) => new Vector3(v.X, v.Y, z);
}