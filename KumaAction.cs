using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KumaAction : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] Transform attackPoint; //Change to GameObject attackPoint;
    [SerializeField] Transform attackPoint1;
    [SerializeField] float attackRange = 0.6f;
    [Header("Detection")]
    [SerializeField] float detactZone = 20f;
    [SerializeField] float turnSpeed = 1.5f;
    [SerializeField] float kumaDamage = 70f;


    Transform target;

    public LayerMask enemyLayer;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    float distanceToTarget = Mathf.Infinity;
    NavMeshAgent navMeshAgent;
    //EnemyHealth health;
    

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void FixedUpdate()
    {
        //if (health.IsDead()) // add on 13/6/2020
        //{
        //enabled = false;
        //navMeshAgent.enabled = false;
        //} // added on 13062020
        SearchTarget();
        if (target)
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position);
            if (distanceToTarget <= detactZone)
            {
                FaceTarget();
                EngageTarget();
            }
        }
        else
            //SearchTarget();
            GetComponent<Animator>().SetBool("Chase", false);
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        AttackTarget();

    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Chase", true);
        if (distanceToTarget >= detactZone) // Latest Update
        {
            GetComponent<Animator>().SetTrigger("Face");// Latest Update
            FaceTarget();// Latest Update
        }
    }

    private void AttackTarget()
    {
        if (Time.time >= nextAttackTime)
        {
            GetComponent<Animator>().SetTrigger("Attack");
            Collider[] hittarget = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider target in hittarget)
            {
                target.GetComponent<PlayerHealth>().TakenDamage(kumaDamage);
                //gameObject.SetActive(false); to make your attack once
            }
            //GetComponent<Animator>().SetTrigger("Attack");
            Collider[] hittarget1 = Physics.OverlapSphere(attackPoint1.position, attackRange, enemyLayer);

            foreach (Collider target in hittarget1)
            {
                target.GetComponent<PlayerHealth>().TakenDamage(kumaDamage);
                //attackPoint1.SetActive(false); //to make your attack once
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
        FaceTarget();
        SearchTarget();
    }

    private void SearchTarget()
    {
        var sceneTargets = FindObjectsOfType<PlayerHealth>();
        if (sceneTargets.Length == 0)
        {
            return;
        }
        Transform closestTarget = sceneTargets[0].transform;

        foreach (PlayerHealth testTarget in sceneTargets)
        {
            closestTarget = GetClosest(closestTarget, testTarget.transform);
        }
        target = closestTarget;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);
        if (distToA < distToB)
        {
            return transformA;
        }
        return transformB;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    //void ActivateAttackPoint()
    //{
    //attackPoint.SetActive(true);
    //}
    //void DeactivateAttackPoint()
    //{
        //if (attackPoint.activateInHierarchy)
        //{
            //attackPoint.SetActive(false);
        //}
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detactZone);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPoint1.position, attackRange);
    }
}
