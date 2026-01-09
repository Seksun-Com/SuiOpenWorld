using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //[SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private ProjectileBehavior projectilePrefab;
    [SerializeField] private Transform shootPoint;
    private float lastAttackTime = Mathf.Infinity;
    private Animator anim;
    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.OnWall()) 
            return;
        if (Input.GetMouseButton(0) && lastAttackTime >= attackCooldown)
        {
            Attack();
            if (playerMovement.IsGrounded())
                anim.SetTrigger("attack");
            else
                anim.SetTrigger("jumpAttack");
            
        }
        lastAttackTime += Time.deltaTime;
    }

    void Attack()
    {
        ProjectileBehavior newProjectile = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
        newProjectile.SetDirection(transform.localScale.x);
        lastAttackTime = 0f;
    }
}
