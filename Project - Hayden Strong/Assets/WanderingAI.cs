using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    public float distanceToCheck; // This should be larger than the height of your tallest floor
    public NavMeshAgent agent;
    public AudioSource mySource;
    public float DistancePerSoundTrigger;
    [SerializeField]
    public AudioClip[] clips;
    public float volumeLevel;
    private Vector3 prevPos;
    private float distance = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 randomPoint = Random.insideUnitSphere * distanceToCheck; // Make a random point that is possibly on the navmesh
        NavMeshHit hit;
        
        if (prevPos != null)
        {
            distance += Vector3.Distance(prevPos, agent.transform.position);
            if (distance >= DistancePerSoundTrigger)
            {
                distance = 0;
                // play sound
                mySource.PlayOneShot(clips[Random.Range(0, clips.Length)], volumeLevel);
            }
        }
        prevPos = agent.transform.position;

        if (NavMesh.SamplePosition(randomPoint, out hit, distanceToCheck, NavMesh.AllAreas)) // Returns a bool indicating success or failure   
        {
            if (agent.remainingDistance == 0)
            {
                agent.SetDestination(hit.position);
            }
        }
    }
}
