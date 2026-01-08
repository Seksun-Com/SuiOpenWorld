using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int healthAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().addHealth(healthAmount);
            gameObject.SetActive(false);
        }
    }
}