using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourCompositeNode : BehaviourNode
{
    public List<BehaviourNode> children = new List<BehaviourNode>();
}
