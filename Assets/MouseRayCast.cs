﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using System.Linq;
using UnityEngine;
using Mapbox.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class MouseRayCast : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;




    Ray _ray;
 

    float _elapsedTime;


    Vector2d _currentLatitudeLongitude;




    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
           
                RaycastHit hit;
                _elapsedTime = 0f;
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out hit, 1000.0f))
                {

                    _currentLatitudeLongitude = hit.point.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
                    Debug.Log(_currentLatitudeLongitude);

                }
            
        }
    }
}