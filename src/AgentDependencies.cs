using System.Collections.Generic;
using System.Linq;
using Godot;
public partial class AgentDependencies: RigidBody2D {

	Dictionary<System.Type, List<Node>> depsDict;
	public T GetNodeOrNull<T>() where T: Node {
		if (depsDict.TryGetValue(typeof(T), out var list)) {
			return (T)list[0];
		}
		return null;
	}
	AgentBehaviours _agentBehaviours;
	public override void _Ready() {
		int nChildren = GetChildCount();
		depsDict = new System.Collections.Generic.Dictionary<System.Type, List<Node>>(nChildren) {
			{ typeof(RigidBody2D), new List<Node>() {this} },
			{ typeof(AgentDependencies), new List<Node>() {this} }
		};

		void AddNode(Node n) {
			var t = n.GetType();
			if (depsDict.TryGetValue(t, out var list)) {
				list.Add(n);
			}
			else {
				depsDict.Add(t, new List<Node>(32) { n });
			}
		}

		for (int i = 0; i < nChildren; i++) {
			var child = GetChild(i);
			AddNode(child);
			if (child is AgentBehaviours behaviours) {
				int nBehaviours = behaviours.GetChildCount();
				for (int j = 0; j < nBehaviours; j++) {
					var behaviour = behaviours.GetChild(j);
					AddNode(behaviour);
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
