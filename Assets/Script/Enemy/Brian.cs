using UnityEngine;

public class Brian : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private int distanceMove;
    private Vector3 initialPosition;
    private bool movingLeft;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide with " + collision.name);
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
