using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
partial class AgentBehaviours: Node {
    List<IBehaviourNode> behaviours;
    public override void _Ready() {
        int nChildren = GetChildCount();
        behaviours = new List<IBehaviourNode>(nChildren);
        for (int i = 0; i < nChildren; i++) {
            if (GetChild(i) is IBehaviourNode behaviour) {
                behaviours.Add(behaviour);
            }
        }

        void GetBehaviours(Node n) {
            int numChildren = n.GetChildCount();
            for (int i = 0; i < numChildren; i++) {
                var child = n.GetChild(i);
                GetBehaviours(child);
                if (child is IBehaviourNode behaviour) {
                    behaviours.Add(behaviour);
                }
            }
        }
        GetBehaviours(this);
    }

    public void InitBehaviours(AgentDependencies deps) {
        foreach (var b in behaviours) {
            b.Init(deps);
        }
    }
}

