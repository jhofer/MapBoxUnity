using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable {

    // Use this for initialization
    void ApplyDamage(float damage);

    bool IsDead();
}
