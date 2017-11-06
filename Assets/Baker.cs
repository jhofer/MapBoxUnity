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
    void LateUpdate()
    {
        var elapsedTime = Time.time - triggerHappend;
        if (elapsedTime> _updateInterval && shouldBake)
        {
            GetComponent<NavMeshSurface>().BuildNavMesh();
        
            shouldBake = false;
        }


    }
}
