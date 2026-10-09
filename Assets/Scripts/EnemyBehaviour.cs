
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    Transform target;
    NavMeshAgent enemy;
    private GameObject player;
    
    private float _enemyFace;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");

        if (player != null)
        {
            target = player.transform;
        } else
        {
            Debug.Log(message: "Player Not Found!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            //Enemy Rotate
            enemy.updateRotation = true;
            //Enemy movement
            enemy.SetDestination(target.position);
        }
    }
}
