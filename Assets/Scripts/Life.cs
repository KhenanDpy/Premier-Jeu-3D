using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] GameObject[] hearts; 
    public int life;
    bool dead;

    public Transform respawn;

    //faire un bouton pour respawn

    void Start()
    {
        Init();
    }

    void Init()
    {
        dead = false;
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
        life = hearts.Length;
    }

    void Update()
    {
        if (dead == true)
        {
            Debug.Log("you died");
            // TODO death code

            Init();
        }
    }

    public void TakeDamage(int damage)
    {
        if (life >= 1)
        {
            life -= damage;
            hearts[life].gameObject.SetActive(false);
            if (life < 1)
            {
                dead = true;
            }
        }
    }
}
