using System.Collections;
using System.Collections.Generic;
using CleverCrow.Fluid.BTs.Tasks;
using UnityEngine;

public class Attack : TaskBase
{
    private Animator anim;
    private Transform transform;
    private Transform targetTransform;

    public Attack(Animator anim, Transform transform, Transform targetTransform)
    {
        this.anim = anim;
        this.transform = transform;
        this.targetTransform = targetTransform;
    }
    public override TaskStatus Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, targetTransform.position) <= 10f)
        {
            Debug.Log("Attacking");
            anim.SetBool("isRunning", false);
            anim.SetBool("isFighting", true);
            return TaskStatus.Continue;
        }
        else
        {
            anim.SetBool("isFighting", false);
            return TaskStatus.Failure;
        }

    }
}
