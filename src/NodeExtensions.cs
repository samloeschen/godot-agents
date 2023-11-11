using Godot;

public static class NodeExtensions {
    public static T GetChildNodeOfType<T>(this Node n) where T: Node {
        int nChildren = n.GetChildCount();
        for (int i = 0; i < nChildren; i++) {
            if (n.GetChild(i) is T result) {
                return result;
            }
        }
        return null;
    }
}