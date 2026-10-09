using UnityEngine;
using UnityEngine.AI;

public class SwordDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    //Sword Collider
    [SerializeField] private Collider weaponCollider;

    //Victory Screen
    [SerializeField] private healthBar enemyHealth;
    [SerializeField] private MainMenuManager gameVictoryScreen;
    [SerializeField] private GameObject victoryPanel;

    private void Update()
    {
        Debug.Log("Update is running");

        Debug.Log(enemyHealth == null
            ? "enemyHealth is NULL"
            : "enemyHealth is assigned");

        Debug.Log(gameVictoryScreen == null
            ? "gameVictoryScreen is NULL"
            : "gameVictoryScreen is assigned");

        Debug.Log(victoryPanel == null
            ? "victoryPanel is NULL"
            : "victoryPanel is assigned");

        if (enemyHealth.health <= 0)
        {
            gameVictoryScreen.Show(victoryPanel);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log(message: "Enemy Hit");
            enemyHealth.TakeDamage(damage);
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
