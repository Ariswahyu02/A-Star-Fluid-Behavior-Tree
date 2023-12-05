using UnityEngine;
using System.Collections;

public class PatrolAI : MonoBehaviour
{
    private Animator anim;
    public Transform[] roamPosition;
    public Rigidbody targetRb;
    [SerializeField] private float speed = 20;
    Vector3[] path;
    bool targetIsMoving;
    int targetIndex;
    bool isRoaming = false;
    int randomizer;

    void Start()
    {
        anim = GetComponent<Animator>();
        randomizer = Random.Range(0, this.roamPosition.Length);
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {

        // if (Vector3.Distance(transform.position, roamPosition[randomizer].position) > .1f)
        // {
        //     if (!isRoaming)
        //     {
        //         anim.SetBool("isRunning", true);
        //         PathRequestManager.RequestPath(transform.position, roamPosition[randomizer].position, OnPathFound);
        //         isRoaming = true;
        //     }


        // }
        // else
        // {
        //     anim.SetBool("isRunning", false);
        //     randomizer = Random.Range(0, this.roamPosition.Length);
        //     isRoaming = false;
        // }

    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            if (path.Length > 0)
                StartCoroutine("FollowPath");
        }
    }

    private void OnDisable()
    {
        PathRequestManager.NullRequest();
        path = null;
        StopAllCoroutines();
    }

    IEnumerator UpdatePath()
    {

        Transform ramdomPos = roamPosition[randomizer];

        if (Vector3.Distance(transform.position, ramdomPos.position) > .1f)
        {
            PathRequestManager.RequestPath(transform.position, ramdomPos.position, OnPathFound);

            yield return new WaitForSeconds(4f);
        }
        randomizer = Random.Range(0, this.roamPosition.Length);
        StartCoroutine(UpdatePath());
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    anim.SetBool("isRunning", false);
                    yield break;
                }
                currentWaypoint = path[targetIndex];
                anim.SetBool("isRunning", true);
            }

            Vector3 directionToTarget = currentWaypoint - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            float rotationSpeed = 10f;
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = smoothedRotation;

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}
