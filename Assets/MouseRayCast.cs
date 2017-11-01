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

public class MouseRayCast : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;


    [SerializeField]
    public Transform prefab;


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


                MonoBehaviour[] list = hit.collider.gameObject.GetComponents<MonoBehaviour>();
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

                GameManager.instance.ClickOnMap(hit.point, _currentLatitudeLongitude);


            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            _elapsedTime = 0f;
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out hit, 1000.0f))
            {
                var spanPos = hit.point;
                //spanPos.y -=1f;
                var go = Instantiate(prefab, spanPos, Quaternion.identity).gameObject;
                NavMeshHit closestHit;
                if (NavMesh.SamplePosition(spanPos, out closestHit, 500,1))
                {
                    go.transform.position = closestHit.position;
                   
                    //TODO
                }else
                Debug.LogError("Could not find position on NavMesh!");
            }
        }
    }
}