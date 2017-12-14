using UnityEngine;
using System.Collections;

public class TurretTurn : MonoBehaviour
{
    //This script was created for use in the resource "Combat Systems-Constructor", 
    //the script is not perfect;), if you find errors in it or improve, 
    //please share information with me. Thanks.

    public Transform HorizontalAxis;
    public Transform VerticalAxis;

    public string EnemiesTag;






    public weap[] Weap;
    [System.Serializable]
    public struct weap
    {
        public Transform Weapon;
        public float FireRate;  //1			// Number in seconds which controls how often the player can fire


    }
    private Animator animatorWeap;
    private float[] nextFire;                     // Float to store the time the player will be allowed to fire again, after firing
    private Vector3 dirToTarget;
    private Quaternion newRotationX, newRotationY;
    private float TargetDistance;
    private Quaternion HorizontalAxis_v, VerticalAxis_v;
    private UnitData unitData;
    private AimSystem aimSystem;
    [SerializeField, HideInInspector]
    Quaternion original, originalBarrel;
    private Vector3 dirXZ, forwardXZ, dirYZ, forwardYZ;
    //The search for the target will be carried out with the help of cortex every 0.1 
    private float searchTimeDelay = 0.1f;
    // Use this for initialization

    void Start()
    {
        unitData = GetComponent<UnitData>();
        aimSystem = GetComponent<AimSystem>();
        originalBarrel = VerticalAxis.transform.rotation;


        System.Array.Resize(ref nextFire, Weap.Length);

        HorizontalAxis_v = HorizontalAxis.transform.rotation;
        original = Quaternion.Euler(HorizontalAxis_v.eulerAngles.x, 0, 0);


    }

    // Update is called once per frame
    void LateUpdate()
    {


        // HorizontalAxis.localRotation = original;
        if (!aimSystem.nearestTarget)
        {

            HorizontalAxis_v = Quaternion.RotateTowards(HorizontalAxis_v, HorizontalAxis.transform.rotation, unitData.SpeedTurn / 10);
            HorizontalAxis.rotation = HorizontalAxis_v;

            return;
        }
        var target = aimSystem.nearestTarget.GetComponent<Collider>().bounds.center;


        dirToTarget = (target - HorizontalAxis.transform.position);


        Vector3 originalForward = new Vector3(0, 0, 1);//Vector3.up;// original *
        Vector3 yAxis = Vector3.up; // world y axis
        dirXZ = Vector3.ProjectOnPlane(dirToTarget, yAxis);

        forwardXZ = Vector3.ProjectOnPlane(originalForward, yAxis);
        float yAngle = Vector3.Angle(dirXZ, forwardXZ) * Mathf.Sign(Vector3.Dot(yAxis, Vector3.Cross(forwardXZ, dirXZ)));
        float parentY = transform.rotation.eulerAngles.y;
        if (parentY < 0) parentY = -parentY;
        if (parentY > 180) parentY -= 360;
        float yClamped = Mathf.Clamp(yAngle, -unitData.HorizontalConstraint + parentY, unitData.HorizontalConstraint + parentY);
        Quaternion yRotation = Quaternion.AngleAxis(yClamped, Vector3.up);

        Quaternion xRotation = Quaternion.Euler(0, 0, 0);
        float xClamped = 0;
        float xAngle = 0;
        if (yClamped == yAngle)
        {
            dirToTarget = (target - VerticalAxis.transform.position);
            originalForward = yRotation * new Vector3(0, 0, 1);
            Vector3 xAxis = yRotation * Vector3.right; // our local x axis
            dirYZ = Vector3.ProjectOnPlane(dirToTarget, xAxis);
            forwardYZ = Vector3.ProjectOnPlane(originalForward, xAxis);
            xAngle = Vector3.Angle(dirYZ, forwardYZ) * Mathf.Sign(Vector3.Dot(xAxis, Vector3.Cross(forwardYZ, dirYZ)));
            xClamped = Mathf.Clamp(xAngle, -unitData.UpConstraint, -unitData.DownConstraint);
            xRotation = Quaternion.AngleAxis(xClamped, Vector3.right);
        }


        if (HorizontalAxis.transform == VerticalAxis.transform)
        {
            newRotationX = yRotation * original * xRotation;
            HorizontalAxis_v = Quaternion.RotateTowards(HorizontalAxis_v, newRotationX, unitData.SpeedTurn / 10);
        }
        else
        {
            newRotationX = yRotation * original;
            HorizontalAxis_v = Quaternion.RotateTowards(HorizontalAxis_v, newRotationX, unitData.SpeedTurn / 10);
            newRotationY = originalBarrel * xRotation;
            VerticalAxis.localRotation = Quaternion.RotateTowards(VerticalAxis.localRotation, newRotationY, unitData.SpeedTurn / 10);
        }
        HorizontalAxis.rotation = HorizontalAxis_v;


        ////fire

        //if(xClamped==xAngle && yClamped==yAngle && TargetDistance< unitData.range - (unitData.range / 4) && HorizontalAxis_v.eulerAngles ==newRotationX.eulerAngles  )
        //for (int i = 0; i < Weap.Length ; i++){
        //	if (nextFire[i] <= 0) {

        //		 nextFire[i] = Weap[i].FireRate;
        //		 animatorWeap = Weap[i].Weapon.GetComponent<Animator> ();
        //		 animatorWeap.SetBool ("Fire", true);

        //	}else 
        //	if (nextFire[i] > 0) {
        //		nextFire[i] -= 0.01f;

        //	}
        //}

    }


    void OnDrawGizmos()
    {
        /*	
            Gizmos.color = Color.blue;
            Gizmos.DrawLine (HorizontalAxis.transform.position, HorizontalAxis.transform.position + dirXZ);
            Gizmos.DrawLine (HorizontalAxis.transform.position, HorizontalAxis.transform.position + forwardXZ);
            Gizmos.color = Color.green;
            Gizmos.DrawLine (HorizontalAxis.transform.position, HorizontalAxis.transform.position + dirYZ);
            Gizmos.DrawLine (HorizontalAxis.transform.position, HorizontalAxis.transform.position + forwardYZ);
            */
    }
}