using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] public int startingHealth;
    public int currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    [Header("iFrames Settings")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private SpriteRenderer spriteRend;
    private void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - (int)_damage, 0, startingHealth);
        if(currentHealth <= 0)
        {
            if(!dead) Die();
        }else
        {
            Hit();
        }
    }

    private void Hit()
    {
        // Play hit animation or effects
        anim.SetTrigger("hit");
        StartCoroutine(Invunerability());
        // Debug.Log("Hit! Current Health: " + currentHealth);
    }

    private void Die()
    {
        // Play death animation or effects
        anim.SetTrigger("die");
        GetComponent<PlayerMovement>().enabled = false;
        dead = true;
        // Debug.Log("Dead!");
        // Additional logic for death (e.g., disable player controls)
    }

    public void addHealth(float _health)
    {
        if (dead) return;
        currentHealth = Mathf.Clamp(currentHealth + (int)_health, 0, startingHealth);
        Debug.Log("Health Added! Current Health: " + currentHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.75f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
