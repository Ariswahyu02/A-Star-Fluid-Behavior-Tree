using UnityEngine;
using System.Collections;

public class ChasePlayer : MonoBehaviour
{
	private Animator anim;
	public Transform target;
	public Rigidbody targetRb;
	[SerializeField] private float speed = 20;
	Vector3[] path;
	bool targetIsMoving;
	int targetIndex;

	void Start()
	{
		anim = GetComponent<Animator>();
		targetRb = target.GetComponent<Rigidbody>();
		StartCoroutine(UpdatePath());

	}

	private void Update()
	{
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

	IEnumerator UpdatePath()
	{

		while (Vector3.Distance(transform.position, target.position) > .1f)
		{
			PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
			yield return new WaitForSeconds(1f);
		}
	}

	IEnumerator Waiting()
	{
		yield return new WaitForSeconds(3f);
	}

	private void OnEnable()
	{
		StartCoroutine(UpdatePath());
	}

	private void OnDisable()
	{
		PathRequestManager.NullRequest();
		path = null;
		StopAllCoroutines();
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
