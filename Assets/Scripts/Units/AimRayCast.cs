using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRayCast : MonoBehaviour {
    private UnitData unitData;
    private AimSystem aimSystem;

    // Use this for initialization
    void Start()
    {
        unitData = GetComponentInParent<UnitData>();
        aimSystem = GetComponentInParent<AimSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        float range = unitData.range; //get range from the ShootingSystem script

        if (Physics.Raycast(transform.position, transform.forward, out hit, unitData.range))
        {
            aimSystem.Locked = (aimSystem.nearestTarget != null && aimSystem.nearestTarget == hit.transform.gameObject);

        }
    }
}
