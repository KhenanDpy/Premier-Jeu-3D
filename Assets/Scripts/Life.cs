using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] GameObject[] hearts;
    public int life;
    public Movements player;
    public GameObject deadGO;
    public Transform respawn;
    public GameController controller;

    //faire un bouton pour respawn

    void Start()
    {
        Init();
    }

    public void Init()
    {
        transform.position = respawn.position;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.alive = true;
        deadGO.SetActive(false);
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
        life = hearts.Length;
        

        controller.ResetAll();
    }

    void Update()
    {


        if (transform.position.y < -50f)
        {
            player.alive = false;
        }

        if (player.alive == false)
        {
            player.GetComponent<Rigidbody>().useGravity = false;
            deadGO.SetActive(true);
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
                player.alive = false;
            }
        }
    }

    public void EndGame()
    {
        Application.Quit();
        Debug.Log("On quitte");
    }
}
