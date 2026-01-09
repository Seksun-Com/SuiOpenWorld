using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide with " + collision.name);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
