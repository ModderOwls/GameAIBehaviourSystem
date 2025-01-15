using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class BehaviourCompositeNode : BehaviourNode
{
    public List<BehaviourNode> children = new List<BehaviourNode>();
}
