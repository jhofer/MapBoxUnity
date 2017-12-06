using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Baker : MonoBehaviour
{

    [SerializeField]
    private float _updateInterval ;

    [SerializeField]
    AbstractTileProvider tileProvider;

    bool shouldBake = true;
    private float triggerHappend;

    // Use this for initialization
    void Start()
    {
        tileProvider.OnTileAdded += TilesChanged;
        tileProvider.OnTileRemoved += TilesChanged;
    }

    private void TilesChanged(Mapbox.Map.UnwrappedTileId obj)
    {
        shouldBake = true;
        triggerHappend = Time.time;
    }
   
    // Update is called once per frame
    void Update()
    {
        var agents = GameObject.FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
        var elapsedTime = Time.time - triggerHappend;
        if (elapsedTime> _updateInterval && shouldBake)
        {
            //save destinations
            Dictionary<NavMeshAgent, Vector3> destinations = new Dictionary<NavMeshAgent, Vector3>();
            Dictionary<NavMeshAgent, Vector3> speed = new Dictionary<NavMeshAgent, Vector3>();
            for (int i = 0; i < agents.Length; i++)
            {
                var agent = agents[i];
                destinations[agent] = agent.destination;
                speed[agent] = agent.velocity;
            }

           //build new navmesh
            GetComponent<NavMeshSurface>().BuildNavMesh();

            //restart all agents after BuildNavMesh
            for (int i = 0; i < agents.Length; i++)
            {
                var agent = agents[i];
                agent.SetDestination(destinations[agent]);
                agent.velocity=speed[agent];



            }


            shouldBake = false;
        }
      


    }
}
