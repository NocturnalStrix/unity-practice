using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float health;
    public float lerpSpeed = 0.05f;
    public float damageCooldown = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider == null)
        {
            Debug.Log("Health Slider is NULL on: " +gameObject.name +" | ID: "+gameObject.GetEntityId());
        }
        if(healthSlider.value != health)
        {
            healthSlider.value=health;
        }

        if(healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value,health,lerpSpeed);
        }
        if (damageCooldown <= 1.0f)
        {
            damageCooldown-= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (damageCooldown <= 0.0f)
        {
            health -= damage;
            damageCooldown=1.0f;
        }
    }
}
