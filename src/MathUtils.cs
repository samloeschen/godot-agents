using Godot;
public static class MathUtils {
    public static float WrapDeltaAngle(float angle) {
        while (angle <= -Mathf.Pi) {
            angle += 2f * Mathf.Pi;
        }
        while (angle > Mathf.Pi) {
            angle -= 2f * Mathf.Pi;
        }
        return angle;
    }
}