using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleController : MonoBehaviour, ISelectable, IMovable {

    public Vector3 target;

    public void Move(Vector3 target)
    {
        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    public void Select()
    {
        Debug.Log("Klick Hamvee");
        GameManager.instance.AddSelection(gameObject);
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > .1f)
        {
            GetComponent<NavMeshAgent>().enabled = true;
        }
        // NavigationUtils.MoveToTarget(transform, target);
    }

  
  
}
