using System.Collections;
using System.Collections.Generic;
using CleverCrow.Fluid.BTs.Tasks;
using UnityEngine;

public class Patrol : TaskBase
{
    private SphereCollider sphereCollider;
    private PatrolAI patrol;
    private Transform targetTransform;

    public Patrol(SphereCollider sphereCollider, PatrolAI patrol, Transform targetTransform)
    {
        this.sphereCollider = sphereCollider;
        this.patrol = patrol;
        this.targetTransform = targetTransform;
    }
    public override TaskStatus Update()
    {
        base.Update();
        if (sphereCollider.bounds.Contains(targetTransform.position))
        {
            patrol.enabled = false;
            return TaskStatus.Failure;
        }
        else
        {
            patrol.enabled = true;
            return TaskStatus.Success;

        }
    }

    protected override void OnExit()
    {
        // base.OnExit();
        // patrol.enabled = false;
    }
}
