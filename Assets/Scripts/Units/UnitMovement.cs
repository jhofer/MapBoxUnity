


using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    public float m_Speed = 3.0f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 1f;            // How fast the tank turns in degrees per second.
    public int currentHealth = 50;              //The tank's current health point total

    public AudioClip tankDead;
    public ParticleSystem m_ExplosionParticles;      //  the particles that will play on explosion.
    public ParticleSystem m_SmokeEffect;
    public int DeadMax = 4;                     //the number of death animations
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private NavMeshAgent agent;
    private AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds.

    private WaitForSeconds shotDuration = new WaitForSeconds(15f);    // WaitForSeconds hide object 


    private float posY;
    private bool m_dead;

    private Animator animator;
    private string animName, animNameOld;
    private int waitPointMove = 500; //waiting time at point of movement



    //public TargetPointsPos[] targetPointsPos;//(AI) Points for positions and commands
    //[System.Serializable]
    //public struct  TargetPointsPos {
    //	public Transform pos;
    //	public string act;	//1			// Number in seconds which controls how often the player can fire
    //}
    private byte sel_ltargetPointPos;                   //(enemy AI) selected targetPointPos in array

    private void Awake()
    {
        animator = GetComponent<Animator>();
      
        agent = GetComponent<NavMeshAgent>();
        animName = "Idle";
        //agent.updatePosition = false;

    }


    private void Update()
    {
        if (m_dead)
            return;

        if (agent.velocity.sqrMagnitude > 0){

            if (animName == "MoveWheelsForward")
            {
                animName = "ChangeToWalk";
            }
            if (animName == "ChangeToWalk")
            {
                animName = "WalkForward";
            }
            if (animName == "Idle")
            {
                animName = "WalkForward";
            }
            //turn towards  
        }
        else
        {
            if (animName == "Idle")
            {
                animName = "WalkForward";
            }
            animName = "Idle";
        }
        //     else {
        //	//The tank got to the target, choose another target position for movement
        //	if (targetPointsPos.Length > 1)
        //	if (targetPointsPos [sel_ltargetPointPos].act == "" || waitPointMove == 0) { 
        //		waitPointMove = 500;

        //		if (sel_ltargetPointPos < targetPointsPos.Length - 1)
        //			sel_ltargetPointPos++;
        //		else
        //			sel_ltargetPointPos = 0;
        //	} else {
        //		if (targetPointsPos [sel_ltargetPointPos].act == "wait" ) { 
        //			if (waitPointMove > 0)		waitPointMove--; 
        //			animName="Idle";
        //		}
        //	}
        //}
        //	}
        //////////////// for   AI //////////////// end


        if (animName != animNameOld)
        {
            Debug.Log("Set Animation");
            animNameOld = animName;
            animator.SetBool("ACS_"+animName, true);
        }
		DrawPath (agent.path);
    }



	void DrawPath(NavMeshPath path ){
		if(path.corners.Length < 2) //if the path has 1 or no corners, there is no need
			return;

		var line = GetComponent<LineRenderer> ();
		line.SetVertexCount(path.corners.Length); //set the array of positions to the amount of corners
		line.SetPosition(0,transform.position+Vector3.up*10);
		for(var i = 1; i < path.corners.Length; i++){
			line.SetPosition(i, path.corners[i]+Vector3.up*10); //go through each corner and set that to the line renderer's position
		}
	}



    void OnTriggerEnter(Collider col)
    {
        if (m_dead) return;
        if (col.gameObject.tag == "Shell")
        {
            Shell shell = col.GetComponent<Shell>();
            Damage(shell.shellDamage);
        }
    }


    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;

        //Check if health has fallen below zero
        if (currentHealth <= 0)
        { //DEAD
            m_dead = true;
            animator.SetBool("Dead" + (int)Random.Range(1, DeadMax), true);

            m_MovementAudio = GetComponent<AudioSource>();
            m_MovementAudio.clip = tankDead;
            m_MovementAudio.Play();

            transform.gameObject.tag = "Destroyed";

            Destroy(GetComponent<UnitMovement>());
            Destroy(GetComponent<TurretTurn>());
            m_ExplosionParticles.Play();
            m_SmokeEffect.gameObject.SetActive(true);
            m_SmokeEffect.Play();




            StartCoroutine(hideTnak());
        }
    }


    private IEnumerator hideTnak()
    {
        //Wait for 15 seconds
        yield return shotDuration;
        //hide tank
        //	gameObject.SetActive (false);
    }
}

 