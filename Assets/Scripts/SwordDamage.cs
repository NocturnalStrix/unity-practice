using UnityEngine;
using UnityEngine.AI;

public class SwordDamage : MonoBehaviour
{
    [SerializeField] private float _swordDamage;
    //Sword Collider
    [SerializeField] private Collider weaponCollider;

    //Victory Screen
    [SerializeField] private healthBar enemyHealth;
    [SerializeField] private MainMenuManager gameVictoryScreen;
    [SerializeField] private GameObject victoryPanel;

    private void Update()
    {
        // Debug.Log("Health: " + enemyHealth.health
        // + ", Defeated: " + gameVictoryScreen.enemyDefeated);
        if (enemyHealth.health <= 0 && !gameVictoryScreen.enemyDefeated)
        {
            gameVictoryScreen.enemyDefeated = true;
            gameVictoryScreen.Show(victoryPanel);
        }  
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log(message: "Enemy Hit");
            enemyHealth.TakeDamage(_swordDamage);
        }
    }

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }
}
