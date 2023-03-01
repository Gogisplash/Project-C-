using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiEnnemie : MonoBehaviour
{


    //public GameObject Player;
    public InstanceGame PlayerInstance;
    public GameObject Spawn;
    public GameObject FisrtCheckPoint;
    public GameObject SecondCheckPoint;
    GameObject[] gameObjectArray;
    int CheckPoint = 0;

    //Agent de Navigation
    NavMeshAgent navMeshAgent;

    //Animations
    Animator animator;
    
    public float AttackDistance;
    public float ChaseDistance;

    //Action actuelle
    public string currentAction;

    //string STAND_STATE;
    string WALK_STATE;
    string ATTACK_STATE;
    string Idle;

    //Attack variable 
    public static event Action OnHit;
    float lastAttack = 0f;
    float attackSpeed = 2f;

    void Awake()
    {
       
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        WALK_STATE = "isWalking";
        ATTACK_STATE = "isAttacking";
        Idle = "Idle";
        currentAction = Idle;
        gameObjectArray = new GameObject[4];
        gameObjectArray[0] = Spawn;
        gameObjectArray[1] = FisrtCheckPoint;
        gameObjectArray[2] = SecondCheckPoint;
       
        navMeshAgent.SetDestination(FisrtCheckPoint.transform.position);
    }
    void Update()
    {
        if (this != null)
        {
            ResetAnimation();
            if (Patroning()) 
            {
                return;
            } else
            {
                ResetAnimation();
                navMeshAgent.SetDestination(PlayerInstance.PlayerInstance.transform.position);
                navMeshAgent.speed = 0;
                if (navMeshAgent.remainingDistance < ChaseDistance && navMeshAgent.remainingDistance > AttackDistance)
                {
                    Walk();
                }
                else if (navMeshAgent.remainingDistance <= AttackDistance)
                {
                    Attack();
                }
            }
        }
        lastAttack += Time.deltaTime;
    }

    bool Patroning()
    {
        float Distance = Vector3.Distance(PlayerInstance.PlayerInstance.transform.position, this.transform.position);
        if (Distance <= ChaseDistance)
        {
            
            return false;
        }
        else
        {
            ChangeDestination();
            Walk();
            if (navMeshAgent.remainingDistance == 0)
            {
                if (CheckPoint == 2)
                {
                    CheckPoint = 0;
                }
                else
                    CheckPoint += 1;
                ChangeDestination();
            }
            return true;
        }
    }

    void ChangeDestination()
    {
        navMeshAgent.SetDestination(gameObjectArray[CheckPoint].transform.position);
    }

    void Walk()
    {
        navMeshAgent.speed = 3;
        //L'action est maintenant "Walk"
        currentAction = WALK_STATE;
        //Le paramètre "Walk" de l'animator = true
        animator.SetBool(currentAction, true);
    }
    void Attack()
    {
        navMeshAgent.speed = 0;
        
        //L'action est maintenant "Attack"
        currentAction = ATTACK_STATE;
        
        //Le paramètre "Attack" de l'animator = true
        animator.SetBool(currentAction, true);

        //Timer entre les attaques 
        if(lastAttack >= attackSpeed)
        {
            OnHit?.Invoke();
            lastAttack = 0f;
        }
    }

    private void ResetAnimation()
    {
        animator.SetBool(WALK_STATE, false);
        animator.SetBool(ATTACK_STATE, false);
    }
}
