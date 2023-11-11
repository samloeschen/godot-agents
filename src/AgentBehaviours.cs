using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
partial class AgentBehaviours: Node {
    List<IBehaviourNode> behaviours;
    public override void _Ready() {
        int numChildren = GetChildCount();
        behaviours = new List<IBehaviourNode>(numChildren);
        for (int i = 0; i < numChildren; i++) {
            if (GetChild(i) is IBehaviourNode behaviour) {
                behaviours.Add(behaviour);
            }
        }
    }

    public void InitBehaviours(AgentDependencies deps) {
        foreach (var b in behaviours) {
            b.Init(deps);
        }
    }
}

