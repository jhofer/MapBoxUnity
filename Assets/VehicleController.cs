using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleController : MonoBehaviour, ISelectable, IMovable {

    public Vector3 target;
    private object agent;
    private float startTime;

    public void Move(Vector3 target)
    {
       

        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    public void Select()
    {
        Debug.Log("Klick Vehcile");
        GameManager.instance.AddSelection(gameObject);
    }



    // Use this for initialization
    void Start () {

        NavMeshHit closestHit;

        //if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
        //    gameObject.transform.position = closestHit.position;
        //else
        //    Debug.LogError("Could not find position on NavMesh!");
    }
	
	// Update is called once per frame
	void Update () {
        //if (Time.time > .1f)
        //{
        //    GetComponent<NavMeshAgent>().enabled = true;
        //}

    }



}
