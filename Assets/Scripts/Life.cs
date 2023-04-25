using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] GameObject[] hearts; 
    int life;
    bool dead;

    void Start()
    {
        life = hearts.Length;
    }

    void Update()
    {
        if (dead == true)
        {
            Debug.Log("you died");
            // TODO death code
        }
    }

    public void TakeDamage(int damage)
    {
        if (life >= 1)
        {
            life -= damage;
            Destroy(hearts[life].gameObject);
            if (life < 1)
            {
                dead = true;
            }
        }
    }
}
