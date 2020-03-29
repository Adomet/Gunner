using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{

    public enum AIType
    {
        Passive, Aggrasive
    };

    public enum AIState
    {
        Attack, Roam, Getresource
    };

    //--------- AI Brain----------------
   
    public AIType       Type        = AIType.Passive;
    public AIState      State       = AIState.Roam;
    public Tracker Tracker;
    public Transform target;
    private float targetdistance = 0.0f;
    public float range = 10000f;

    private float AIRotInput = 0.0f;
    public float AIRotInputValue = 1.0f;

    //------------------------------------


    public Gun Mygun;
    public float rotSpeed = 4.0f;
    private float tmprot;
    public float AISpeed = 4.0f;

    private Rigidbody MyRig;





    // Start is called before the first frame update
    void Start()
    {


        Tracker = FindObjectOfType<Tracker>();
        MyRig = GetComponent<Rigidbody>();

        InvokeRepeating("FindTarget", Random.Range(0.1f,1f), Random.Range(0.1f, 0.5f));


        // Randomness
        rotSpeed += Random.Range(0.01f, 1.0f);
        AISpeed += Random.Range(0.01f, 1.0f);
        AIRotInputValue += Random.Range(0.01f, 0.5f);
        range += Random.Range(0.1f, range/4);


        tmprot = rotSpeed;

        // @TODO Add randomized Aı poperties may be a seed for each
    }

    public void AIAttack()
    {
        // if got any target ---> go target and poke his face
        if (target == null)
        {
            State = AIState.Roam;
            return;
        }

        if (2f < Vector3.Distance(transform.position, target.position))
        { 
        Vector3 dir = target.position - (transform.position + (transform.right * 0.75f));
        Quaternion lookrotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(dir),rotSpeed * AIRotInput);
        transform.rotation = lookrotation;
        }


        // Calculate Rot
        AIRotInput = AIRotInputValue * (1.0f - (targetdistance / range)) ;



        Vector3 movement = (AISpeed * Vector3.forward) * Time.deltaTime;

        MyRig.MovePosition(transform.position + (transform.rotation * movement));





    }


    
    public void FindTarget()
    {
        if (!this.enabled)
            return;

             float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (Player enemy in Tracker.Players)
        {

            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy > 0f)
                {
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy.gameObject;
                    }
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetdistance = shortestDistance;
        }

        else
        {
            target = null;
        }
    }

    public void AIRoam()
    {
        // find a Mengageable target to poke
        // rotate random -> find nearest enemy analyze

        rotSpeed -= tmprot * (Random.Range(0.001f,0.005f));
        if (rotSpeed <= 0f)
        {
            rotSpeed = tmprot;
        }

       
        
         Vector3 movement = (AISpeed * Vector3.forward) * Time.deltaTime;
        
         MyRig.MovePosition(transform.position + (transform.rotation * movement));
        
         transform.Rotate(transform.up,rotSpeed);

        if (target != null)
        {
            rotSpeed = tmprot;
            State = AIState.Attack;
        }

       // else if (BoostBar < 3.0f)
       // {
       //     State = AIState.Getresource;
       // }

    }
    public void AIGetresource()
    {
        //Get some resource when too low 


    }

    //@TODO Make a Default strategy system based on AIType
    public void AIMove()
    {

        switch (Type)
        {

            case AIType.Aggrasive:
                {
                    switch (State)
                    {
                        case AIState.Attack:
                            AIAttack();
                            break;
                        case AIState.Roam:
                            AIRoam();
                            break;
                        case AIState.Getresource:
                            AIGetresource();
                            break;
                        default:
                            AIGetresource();
                            break;
                    }
                }
                break;
            case AIType.Passive:
                {
                    switch (State)
                    {
                        case AIState.Attack:
                            AIAttack();
                            break;
                        case AIState.Roam:
                            AIRoam();
                            break;
                        case AIState.Getresource:
                            AIGetresource();
                            break;
                        default:
                            AIGetresource();
                            break;
                    }
                }
                break;

        }

    }



 

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void FixedUpdate()
    {
        if (target != null)
            Mygun.Shoot();

        AIMove();   
    }
}
