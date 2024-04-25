using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GnomeWander : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool firstPointSetOn = true;
    public Transform[] points;
    public Transform[] points2;
    public int index;
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
        if(Vector3.Distance(transform.position, target) < 2 || !agent.hasPath)
        {
            IterateIndex();
            UpdateDestination();
        }
        
    }
    public void UpdateDestination()
    {
        if (firstPointSetOn)
        {
            if (points.Length > 1)
            {
                target = points[index].position;
                agent.SetDestination(target);
            }
            else
            {
                agent.SetDestination(points[0].position);
            }
        }
        else
        {
            target = points2[index].position;
            agent.SetDestination(target);
        }
    }
    void IterateIndex()
    {
        if (firstPointSetOn)
        {
            if (points.Length > 1)
            {
                index++;
            }
        }
        else
        {
            if (points2.Length > 1)
            {
                index++;
            }
        }

        if(firstPointSetOn){
            if (index == points.Length)
            {
                index = 0;
            }
        }
        else {
            if(index == points2.Length)
            {
                index = 0;
            }
        }
    }
}
