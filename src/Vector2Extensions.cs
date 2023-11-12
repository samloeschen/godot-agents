using Godot;
public static class Vector2Extensions {
    public static Vector3 ToVector3(this Vector2 v, float z = 0f) => new Vector3(v.X, v.Y, z);

    public static Vector2 Rotate(this Vector2 v, float angle) {
        Vector2 rotated;
        float cos_angle = Mathf.Cos(angle);
        float sin_angle = Mathf.Sin(angle);
        rotated.X = v.X * cos_angle - v.Y * sin_angle;
        rotated.Y = v.X * sin_angle + v.Y * cos_angle;
        return rotated;
    }
}