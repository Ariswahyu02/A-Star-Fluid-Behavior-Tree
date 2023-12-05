using System.Collections;
using System.Collections.Generic;
using CleverCrow.Fluid.BTs.Tasks;
using UnityEngine;

public class Chasing : TaskBase
{
    private SphereCollider sphereCollider;
    private ChasePlayer chase;
    private PatrolAI patrol;
    private Transform transform;
    private Transform targetTransform;
    public Chasing(Transform transform, SphereCollider sphereCollider, ChasePlayer chase, Transform targetTransform, PatrolAI patrolAI)
    {
        this.transform = transform;
        this.sphereCollider = sphereCollider;
        this.chase = chase;
        this.targetTransform = targetTransform;
        this.patrol = patrolAI;
    }
    public override TaskStatus Update()
    {
        float worldSpaceRadius = sphereCollider.radius * Mathf.Max(sphereCollider.transform.lossyScale.x, Mathf.Max(sphereCollider.transform.lossyScale.y, sphereCollider.transform.lossyScale.z));
        base.Update();
        if (Vector3.Distance(transform.position, targetTransform.position) <= worldSpaceRadius)
        {
            Debug.Log(Vector3.Distance(transform.position, targetTransform.position));
            chase.enabled = true;
            patrol.enabled = false;
            return TaskStatus.Success;
        }
        else
        {

            chase.enabled = false;
            patrol.enabled = true;
            return TaskStatus.Failure;
        }
    }
}
