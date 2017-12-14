using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AimSystem : MonoBehaviour {

    private UnitData unitData;


    [Header("Targets")]
    public GameObject nearestTarget;

    public bool Locked = false;

    void Start()
    {
        unitData = this.GetComponent<UnitData>();
    }

    void FixedUpdate()
    {


        Collider[] colliders = Physics.OverlapSphere(transform.position, unitData.range);
        List<GameObject> inRange = new List<GameObject>();
        for (int i = 0; i <= colliders.Length - 1; i++)
        {
            var go = colliders[i].gameObject;
            var isAttackable = unitData.attackable.Contains(go.tag);
            var isNotThis = go != gameObject;



            if (isAttackable && isNotThis)
            {
                var isNotSameOwner = true;//go.GetComponent<EntityData>().owner != unitData.owner;
                if (isNotSameOwner)
                    inRange.Add(colliders[i].gameObject);
            }
        }
        if (inRange.Count > 0)
        {
            var sorted = inRange.OrderBy(go => Vector3.Distance(go.transform.position, transform.position));
            this.nearestTarget = sorted.First();
        }
        else
        {
            this.nearestTarget = null;
        }



    }

}
