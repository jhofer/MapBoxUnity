using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour {

    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    public GameObject fourLeggedMech;
	// Use this for initialization
	public void CreateFourLeggedMech(Vector3 position, Guid playerId)
    {
        var prefabData = fourLeggedMech.GetComponent<EntityData>();
        
        currentLatitudeLongitude = position.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);

        var go = Instantiate(prefab, hit.point, Quaternion.identity);
        go.transform.parent = unitsContainer.transform;

    }
}
