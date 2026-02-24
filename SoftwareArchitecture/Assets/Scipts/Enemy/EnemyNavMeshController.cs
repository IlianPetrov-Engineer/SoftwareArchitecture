using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private float startSpeed;
    private Coroutine routine;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        startSpeed = navMeshAgent.speed;
    }

    public void ApplyFreeze(float slowness, float duration)
    {
        if (routine != null)
            StopAllCoroutines();

        routine = StartCoroutine(FreezeRoutine(slowness, duration));
    }

    private IEnumerator FreezeRoutine(float slowness, float duration)
    {
        navMeshAgent.speed = startSpeed * (1f - slowness);

        navMeshAgent.isStopped = navMeshAgent.speed <= 0.01f;

        yield return new WaitForSeconds(duration);

        navMeshAgent.speed = startSpeed;
        navMeshAgent.isStopped = false;
    }

    public void ApplyForce(Vector3 force, float maxDistance)
    {
        routine = StartCoroutine(PushCoroutine(force, maxDistance));
    }

    private IEnumerator PushCoroutine(Vector3 force, float maxDistance)
    {
        navMeshAgent.isStopped = true;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
            yield break;

        Vector3 startPos = transform.position;

        rb.angularVelocity = Vector3.zero;

        rb.AddForce(force, ForceMode.Impulse);

        while (Vector3.Distance(startPos, transform.position) < maxDistance)
                yield return null;

        rb.angularVelocity = Vector3.zero;
        navMeshAgent.isStopped = false;
    }
}
