using Godot;
public partial class AgentDependencies: Node {
    System.Collections.Generic.Dictionary<System.Type, Node> depsDict;
    public T GetNodeOrNull<T>() where T: Node {
        if (depsDict.TryGetValue(typeof(T), out Node n)) {
            return (T)n;
        }
        return null;
    }
    AgentBehaviours _agentBehaviours;
    public override void _Ready() {
        int nChildren = GetChildCount();
        depsDict = new System.Collections.Generic.Dictionary<System.Type, Node>(nChildren);

        for (int i = 0; i < nChildren; i++) {
            var child = GetChild(i);
            depsDict.Add(child.GetType(), child);

            if (child is AgentBehaviours behaviours) {
                int nBehaviours = behaviours.GetChildCount();
                for (int j = 0; j < nBehaviours; j++) {
                    var behaviour = behaviours.GetChild(j);
                    depsDict.Add(behaviour.GetType(), behaviour);
                }
                _agentBehaviours = behaviours;
            }
        }
        {
            if (_agentBehaviours is {} behaviours) {
                behaviours.InitBehaviours(this);
            }
        }
    }
}