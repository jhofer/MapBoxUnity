using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    private AimSystem aimSystem;
    private UnitData unitData;
    float time = 0;
    // Use this for initialization
    void Start () {
        aimSystem = GetComponent<AimSystem>();
        unitData = GetComponent<UnitData>();
    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<IDestroyable>().IsDead()) return;
        
        if (aimSystem.nearestTarget == null) return;

        if (time <= unitData.fireDelay)
            time += Time.deltaTime;



        var destroyable = aimSystem.nearestTarget.GetComponent<IDestroyable>();
        if(destroyable!= null && aimSystem.Locked)
        {
            if (time > unitData.fireDelay && !destroyable.IsDead())
            {
                var shootingAnimator = GetComponentInChildren<IShootingAnimation>();

                destroyable.ApplyDamage(unitData.ammoDamage);
                shootingAnimator.Fire();
                time = 0;

            }
        }
    }
}
