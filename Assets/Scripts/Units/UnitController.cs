using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour, ISelectable, IMovable {

    public Vector3 target;
    private object agent;
    private float startTime;
   

    public Vector2d GetLocation()
    {
       return MapUtils.GetGeoLocation(transform.position);
    
    }

    public void Move(Vector3 target)
    {
       

        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    public void Select()
    {
   
		GameManager.instance.AddSelection(gameObject.AddComponent<EntityData>());
    }



    // Use this for initialization
    void Start () {
		GameManager.instance.AddUnit("Jonas", GetComponent<EntityData>());//TODO: set real player 
        NavMeshHit closestHit;

        //if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
        //    gameObject.transform.position = closestHit.position;
        //else
        //    Debug.LogError("Could not find position on NavMesh!");
    }
	
	// Update is called once per frame
	void Update () {

       var projector =  GetComponentInChildren<Projector>(true);
		projector.enabled = GameManager.instance.IsUnitSelected(GetComponent<EntityData>());
        //if (Time.time > .1f)
        //{
        //    GetComponent<NavMeshAgent>().enabled = true;
        //}

    }



}
