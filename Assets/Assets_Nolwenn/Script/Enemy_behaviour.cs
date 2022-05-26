using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    //Name attack animation "Enemy_Attack" so script can be used for multiple enemy
    //Flip may not work properly, use sprite facing left
    
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool isInRange; //Check if enemy is in range
    public GameObject hotZone;
    public GameObject triggerArea;

    private Animator anim;
    private float distance; //Store distance between enemy and target
    private bool inAttackRange;
    private bool attackMode;
    private bool cooling; //Check if enemy is cooling after attack
    private float intTimer;

    private void Awake() {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attackMode && !inAttackRange) //Enemy isn't in attack mode and player isn't in attack range
        {
            Move();
        }

        if(isInRange) //If player is in range
        {
            EnemyLogic();
        }

        if(!InsideOfLimits() && !isInRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack")) //If enemy not in limits, player not in range, not playing attack animation
        {
            SelectTarget();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        inAttackRange = attackDistance >= distance;

        if(distance > attackDistance) //Target is too far to be attacked
        {
            attackMode = false;
        }
        else if(inAttackRange && !cooling) //Target is near enemy and can be attacked
        {
            Attack();
        }

        if(cooling) //Enemy attack cooling
        {   
            Cooldown();
        }
    }

    void Move(){
        anim.SetBool("canWalk", true); //Activate walk animation

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack")) //If not in attack animation
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack(){
        timer = intTimer; //Reset timer when player enter attack range
        attackMode = true; //Enter attack mode

        anim.SetBool("canWalk", false); //Deactivate walk animation
        anim.SetBool("Attack", true); //Activate attack animation
    }

    public void StopAttack(){
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void Cooldown() {
        timer -= Time.deltaTime;
        if(inAttackRange) { anim.SetBool("canWalk", false); }

        if(timer <= 0 /*&& cooling*/) //Cooling check before cooldown is called
        {
            cooling = false;
            timer = intTimer;
        }
    }

    public void TriggerCooling() {
        //Debug.Log("Trigger cooling");
        cooling = true;
    }

    public void TriggerAttackFalse()
    {
        anim.SetBool("Attack", false);
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    
    //Select waypoints to move towards
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

    //Rotate enemy to face target
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
