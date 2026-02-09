using UnityEngine;

public class ShooterTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject[] bullets;
    private float cooldownTimer;
    private void Attack()
    {
        cooldownTimer = 0;
        bullets[FindBulletIndex()].transform.position = shootPoint.position;
        bullets[FindBulletIndex()].GetComponent<EnemyProjectile>().ActiveProjectile();
    }
    private int FindBulletIndex()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
