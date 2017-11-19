using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUtils {



	// Use this for initialization
	public static Vector2d GetGeoLocation(Vector3 position)
    {
        var mapGo = GameObject.Find("Map");
        var map = mapGo.GetComponent<IMap>();
        return position.GetGeoPosition(map.CenterMercator, map.WorldRelativeScale);
   
    }
}
