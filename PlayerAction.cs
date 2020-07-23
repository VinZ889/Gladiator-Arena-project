
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerAction : MonoBehaviour
{
    private CharacterController playController;
    

    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    
    [SerializeField] float attackRange = 0.5f;
  
       
    [Header("Behavoiur")]
    [SerializeField] float aurtherDamage = 35f;
       

    public LayerMask targetLayer;
    Rigidbody rb;
    Transform target;
    public float Jump = 5f;
    public float WForce = 5f;

    private PlayerShield shield;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    

    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        playController = GetComponent<CharacterController>();
       
    }

    void Start()
    {
        shield = GetComponent<PlayerShield>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //rb.AddRelativeForce(Vector3.up * Jump * Time.deltaTime); //shall be able to work for character with rigidybody(Tested)
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //rb.AddRelativeForce(Vector3.forward * WForce * Time.deltaTime);//shall be able to work for character with rigidybody(Right Tested)
            GetComponent<Animator>().SetTrigger("Walk"); 
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //rb.AddRelativeForce(-Vector3.forward * WForce * Time.deltaTime);//shall be able to work for character with rigidybody(Left Tested)
            //GetComponent<Animator>().SetTrigger("Walk");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            shield.ActivateShield(true);
            //Needs to add animation to your character
            //Debug.Log("Block");
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            shield.ActivateShield(false);
            //Needs to add animation to your character
            //Debug.Log("Unblock");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AttackTarget();
        }
    }

        

   private void AttackTarget()
    {
        GetComponent<Animator>().SetTrigger("Attack");

        Collider[] hittarget = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayer);

        foreach (Collider target in hittarget)
        {
             target.GetComponent<EnemyHealth>().TakenDamage(aurtherDamage);
             Debug.Log("Attack");
        }
                      
        Stamina.instance.ReduceStamina(150f);
        
   }

        
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
              
    }
}
