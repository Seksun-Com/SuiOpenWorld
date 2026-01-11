using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] float attackCooldown;
    [SerializeField] float meleeDistance;
    [SerializeField] float rangedDistance;
    [SerializeField] float colliderDistance;
    [SerializeField] int attackDamage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject[] fireballs;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInRange() && cooldownTimer >= attackCooldown)
        {
            float random = Random.Range(0,1);
            if(random <= 0.5)
                Attack();
            else
                RangedAttack();
        }
    }
    private bool PlayerInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * meleeDistance * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * meleeDistance, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * meleeDistance * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * meleeDistance, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void Attack()
    {
        cooldownTimer = 0;
        anim.SetTrigger("Attack");
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * meleeDistance * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * meleeDistance, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Health>()?.TakeDamage(attackDamage);
        }
    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        anim.SetTrigger("RangedAttack");
        fireballs[FindBulletIndex()].transform.position = shootPoint.position;
        fireballs[FindBulletIndex()].GetComponent<EnemyProjectile>().ActiveProjectile();
    }
    private int FindBulletIndex()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
