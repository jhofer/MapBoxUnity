using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationUtils : MonoBehaviour {



	// Use this for initialization
	public static void MoveToTarget(Transform transform, Vector3 target, float speed = 10)
    {
        if (Vector3.Distance(transform.transform.position, target) > 0.3f)
        {
            Vector3 targetDir = target - transform.transform.position;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }
}
