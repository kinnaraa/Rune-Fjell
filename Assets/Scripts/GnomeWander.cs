using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GnomeWander : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] points;
    int index;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target) < 1)
        {
            IterateIndex();
            UpdateDestination();
        }
    }
    void UpdateDestination()
    {
        target = points[index].position;
        agent.SetDestination(target);
    }
    void IterateIndex()
    {
        index++;
        if(index == points.Length)
        {
            index = 0;
        }
    }
}
