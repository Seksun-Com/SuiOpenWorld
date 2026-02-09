using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] public int startingHealth;
    public int currentHealth { get; private set; }
    [Header("iFrames Settings")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    //References
    private Animator anim;
    private bool dead;
    private bool invunerability;
    private void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if(!invunerability)
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
        anim.SetTrigger("die");
        //Deactive all components
        foreach(Behaviour c in components){
            c.enabled = false;
        }
        // Play death animation or effects
        dead = true;
    }

    public void addHealth(float _health)
    {
        if (dead) return;
        currentHealth = Mathf.Clamp(currentHealth + (int)_health, 0, startingHealth);
        Debug.Log("Health Added! Current Health: " + currentHealth);
    }

    private IEnumerator Invunerability()
    {
        invunerability = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.75f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invunerability = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
