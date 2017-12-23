
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

public class MouseManager
{


    private Dictionary<int, bool> down = new Dictionary<int, bool>();
    private Dictionary<int, Vector3> origin = new Dictionary<int, Vector3>();
    private Dictionary<int, Vector3> delta = new Dictionary<int, Vector3>();
    private Dictionary<int, float> downTime = new Dictionary<int, float>();

    public event Action<int> OnClick = delegate { };
    public event Action<int> OnLongClick = delegate { };
    public event Action<int, Vector3> OnDrag = delegate { };
    public event Action<int, Vector3> OnDragStop = delegate { };

    float dragStartTime = 0.3f;


    void CheckClick(int button)
    {
        var clickTimeDelta = Time.time - downTime[button];
        if(clickTimeDelta >= 1)
        {
            Debug.Log("Long click");
            OnLongClick(button);
        }
        else
        {
            Debug.Log("Long click");
            OnClick(button);
        }
     
    }


    float GetMouseDistance(int button)
    {
        var mouseOrigin = origin[button];
        var mousePos = Input.mousePosition;
        var mouseDistance = Vector3.Distance(mouseOrigin, mousePos);
        return mouseDistance;
    }

    float GetClickTime(int button)
    {
       return Time.time - downTime[button];
    }

    void TriggerShortClick(int button)
    {
        var clickTimeDelta = Time.time - downTime[button];
        if (clickTimeDelta <= 0.3)
        {
            Debug.Log("click");
            OnClick(button);
        }
    }

    void CheckClickAndDrag(int button)
    {

        if (Input.GetMouseButtonDown(button))
        {
            origin[button] = Input.mousePosition;
            downTime[button] = Time.time;
            down[button] = true;
        }


        if (Input.GetMouseButton(button))
        {



			if ( down[button] && GetMouseDistance(button) <= 10f && GetClickTime(button) > 1)
            {
                Debug.Log("Long click");
                OnLongClick(button);
                down[button] = false;
            }
        }
        else
        {
            down[button] = false;
        }


        

        if (Input.GetMouseButtonUp(button)) {
                

            if (origin.ContainsKey(button))
            {

                var mouseOrigin = origin[button];
                var mousePos = Input.mousePosition;
                var mouseDistance = Vector3.Distance(mouseOrigin, mousePos);
                if (mouseDistance <= 4f)
                {
                        TriggerShortClick(button);
                }
                else
                {
                    OnDragStop(button, mouseOrigin);
                }
            }
            else {
                    TriggerShortClick(button);
                }

           
        }
        else if (down.ContainsKey(button) && down[button])
        {

            var mouseOrigin = origin[button];
            var mousePos = Input.mousePosition;
            var mouseDistance = Vector3.Distance(mouseOrigin, mousePos);
            if (mouseDistance >= 4f)
            {
                Debug.Log("Drag" + mouseDistance);
                OnDrag(button, mouseOrigin);
            }
         }
    
       

    }


    public void CheckInput()
    {
        CheckClickAndDrag(0);
       

    }
}