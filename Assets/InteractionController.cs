using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using System.Linq;
using UnityEngine;
using Mapbox.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using UnityEngine.AI;
using Mapbox.Examples;
using System;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    GameObject prefab;


    Ray _ray;


    float _elapsedTime;


    Vector2d _currentLatitudeLongitude;





    private void Start()
    {
        GameManager.instance.mouseManager.OnClick += Click;
        GameManager.instance.mouseManager.OnLongClick += LongClick;
    }

    private void LongClick(int obj)
    {
        RaycastHit hit;
        _elapsedTime = 0f;
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out hit, 1000.0f))
        {
            var point = hit.point;
            //point.y += 1;
            _currentLatitudeLongitude = point.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
            
            var go =  Instantiate(prefab,hit.point,Quaternion.identity);
            //go.AddComponent<NavMeshAgent>();

        }
    }

    private void Click(int obj)
    {
        RaycastHit hit;
        _elapsedTime = 0f;
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out hit, 1000.0f))
        {


            MonoBehaviour[] list = hit.transform.gameObject.GetComponentsInParent<MonoBehaviour>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is ISelectable)
                {
                    ISelectable selectable = (ISelectable)mb;
                    selectable.Select();
                    return;
                }
            }

            _currentLatitudeLongitude = hit.point.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);

            foreach (GameObject gameObject in GameManager.instance.selection)
            {
                MonoBehaviour[] movables = gameObject.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour mb in movables)
                {
                    if (mb is IMovable)
                    {
                        IMovable selectable = (IMovable)mb;
                        selectable.Move(hit.point);
                       
                    }
                }
            }
        }

    }
}