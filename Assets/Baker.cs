using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Baker : MonoBehaviour
{
    private float _elapsedTime;

    [SerializeField]
    private float _updateInterval ;

    [SerializeField]
    AbstractTileProvider tileProvider;

    bool shouldBake = true;

    // Use this for initialization
    void Start()
    {
        tileProvider.OnTileAdded += TilesChanged;
        tileProvider.OnTileRemoved += TilesChanged;
    }

    private void TilesChanged(Mapbox.Map.UnwrappedTileId obj)
    {
        shouldBake = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _updateInterval || shouldBake)
        {
            GetComponent<NavMeshSurface>().BuildNavMesh();
            _elapsedTime = 0f;
            shouldBake = false;
        }


    }
}
