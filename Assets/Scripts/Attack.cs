using UnityEngine;
using UnityEngine.AI;

/*
 * Allows the player to kill the monsters and to take damage from them. 
 */
public class Attack : MonoBehaviour
{
    public Life life;
    public GameObject[] monsters;
    public int enemyKilled = 0;

    bool canTouch; // to create an invunerability frame when getting hit

    void Start()
    {
        canTouch = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (canTouch)
        {
            if (collision.collider.gameObject.CompareTag("Weakness")) // if weakpoint touched, we kill it
            {
                collision.gameObject.SetActive(false);
                enemyKilled++;  // +1 on the monster kill counter
            }
            else if (collision.collider.gameObject.CompareTag("Boss"))  // else if the boss is touched, his speed increases. If he reach a certain speed, he dies
            {
                if(collision.gameObject.GetComponent<NavMeshAgent>().speed > 8)
                {
                    collision.gameObject.SetActive(false);
                    enemyKilled += 5;  // +5 on the monster kill counter
                }
                else
                {
                    collision.gameObject.GetComponent<NavMeshAgent>().speed *= 2f;
                }

                
            }
            else if (collision.collider.gameObject.CompareTag("Damageing")) // else we take 1 dmg and we become invunerable
            {
                life.TakeDamage(1);
                canTouch = false;
                Invoke(nameof(ResetCollision), 2); // 2 seconds delay before becoming vulnerable again
            }
        }

    }

    private void ResetCollision()
    {
        canTouch = true;
    }

    public void ResetMonsters()
    {
        enemyKilled = 0;
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].gameObject.SetActive(true);
        }
    }
}
