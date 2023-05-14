using UnityEngine;

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
                enemyKilled++;
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
