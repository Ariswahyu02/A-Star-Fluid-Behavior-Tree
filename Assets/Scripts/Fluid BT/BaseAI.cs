using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;

public class BaseAI : MonoBehaviour
{
    [SerializeField]
    private BehaviorTree _tree;
    public Transform[] roamPosition;
    private SphereCollider sphereCollider;
    private ChasePlayer unit;
    private PatrolAI patrol;
    private Animator anim;
    public Transform target;
    public Rigidbody targetRb;

    private void Awake()
    {
        targetRb = target.GetComponent<Rigidbody>();
        unit = GetComponent<ChasePlayer>();
        anim = GetComponent<Animator>();
        patrol = GetComponent<PatrolAI>();
        sphereCollider = GetComponent<SphereCollider>();

        _tree = new BehaviorTreeBuilder(gameObject)
            .Selector()
                .Sequence("Chase")
                    .Do("Chasing", new Chasing(transform, sphereCollider, unit, target, patrol).Update)
                    .Do("Attack", new Attack(anim, transform, target).Update)
                .End()
                .Do("Patrol", new Patrol(sphereCollider, patrol, target).Update)

            .Build();
    }

    private void Update()
    {
        // Update our tree every frame

        _tree.Tick();
    }


}