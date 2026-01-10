using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool triggered;
    private bool active;
    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupFiretrap();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(1);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        // Play activation animation or effects
        triggered = true;
        spriteRenderer.color = Color.red;

        // Wait for activation delay
        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        // Stay active and wait for reset Firetrap
        yield return new WaitForSeconds(activeTime);
        SetupFiretrap();
    }
    private void SetupFiretrap()
    {
        triggered = false;
        active = false;
        anim.SetBool("activated", false);
        spriteRenderer.color = Color.white;
    }
}
