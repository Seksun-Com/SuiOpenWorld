using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int startingHealth;
    public int currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
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
        Debug.Log("Hit! Current Health: " + currentHealth);
    }

    private void Die()
    {
        // Play death animation or effects
        anim.SetTrigger("die");
        Debug.Log("Dead!");
        GetComponent<PlayerMovement>().enabled = false;
        dead = true;
        // Additional logic for death (e.g., disable player controls)
    }

    public void addHealth(float _health)
    {
        if (dead) return;
        currentHealth = Mathf.Clamp(currentHealth + (int)_health, 0, startingHealth);
        Debug.Log("Health Added! Current Health: " + currentHealth);
    }
}
