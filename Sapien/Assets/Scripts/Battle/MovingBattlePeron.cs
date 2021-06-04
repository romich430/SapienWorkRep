using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingBattlePeron : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform point;
    public Animator Anim;
    public GameObject particleShag;
 
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(AgentWait());
    }
    IEnumerator AgentWait()
    {
        yield return new WaitForSeconds(2);
        agent.SetDestination(point.position);
        StartCoroutine(ParticleShag(particleShag));
    }
    IEnumerator ParticleShag(GameObject particle)
    {while (agent.enabled)
        {
            Instantiate(particle, agent.gameObject.transform);
            yield return new WaitForSeconds(Random.Range(1,2));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, point.position) < .5f)
        {
            agent.enabled = false;
            Anim.SetTrigger("stop");

        }
    }
}
