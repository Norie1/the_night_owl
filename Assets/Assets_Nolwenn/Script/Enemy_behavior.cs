using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behavior : MonoBehaviour
{
    //Name attack animation "Enemy_Attack" so script can be used for multiple enemy
    //Flip may not work properly, use sprite facing left

    //Bug : Attack animation isn't played completely, enemy doesn't wait for end of cooldown to attack again
    
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    public bool isInRange; //Check if enemy is in range
    public GameObject hotZone;
    public GameObject triggerArea;

    private Animator anim;
    private float distance; //Store distance between enemy and target
    [SerializeField] private bool attackMode;
    [SerializeField] private bool cooling; //Check if enemy is cooling after attack
    private float intTimer;

    private void Awake() {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!InsideOfLimits() && !isInRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack")) //Enemy isn't inside his boundaries, player isn't in range and enemy isn't attacking
        {
            SelectTarget(); //Select next target to move towards
        } 

        if(!attackMode) //Enemy is not in attackMode
        {
            Move();

            if(!isInRange)
            {
                StopAttack();
            }
        }

        if(isInRange) //Player is in range
        {
            EnemyLogic();
        }
        /* else 
        {
            StopAttack();
        }*/
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack")) //Target is too far to be attacked
        {
            StopAttack();
        }
        else if(attackDistance >= distance && !cooling) //Target is near enemy and can be attacked
        {
            Attack();
        }

        if(cooling)
        {   
            Cooldown();
            //anim.SetBool("Attack", false);
        }
    }

    void Move(){
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack(){
        timer = intTimer; //Reset timer when player enter attack range
        attackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void StopAttack(){
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void Cooldown() {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    public void TriggerCooling() {
        cooling = true;
    }

    public void TriggerEndAttack() 
    {
        anim.SetBool("Attack", false);
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
        
        //Enemy will target farthest limit
        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 0f;
        }
        else 
        { 
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;
    }
}
