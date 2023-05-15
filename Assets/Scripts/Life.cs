using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * The life points of the player.
 */

public class Life : MonoBehaviour
{
    [SerializeField] GameObject[] hearts;
    public List<GameObject> heartsActive = new List<GameObject>();
    public int lifePoints = 3;
    int life;
    public Movements player;
    public GameObject deadGO;
    public Transform respawn;
    public GameController controller;

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
        heartsActive.Clear();
        controller.ResetAll();
        
        for (int j = 0; j < lifePoints; j++)
        {
            heartsActive.Add(hearts[j]); // Create a list of hearts with the number entered in GameController from the editor
        }
        for (int i = 0; i < heartsActive.Count; i++)
        { 
            heartsActive[i].gameObject.SetActive(true); // Display those hearts
        }
        life = heartsActive.Count;
        
    }

    void Update()
    {


        if (transform.position.y < -50f) // if the player falls, he dies
        {
            player.alive = false;
        }

        if (player.alive == false) // if the player is dead, the gravity is stopped (else it will spin on itself)
        {
            player.GetComponent<Rigidbody>().useGravity = false;
            deadGO.SetActive(true);
        }
    }

    public void TakeDamage(int damage) // when taking damage, a heart is desactivated
    {
        if (life >= 1)
        {
            life -= damage;
            heartsActive[life].gameObject.SetActive(false);
            if (life < 1)
            {
                player.alive = false;
            }
        }
    }

    public void EndGame() // quit the game
    {
        Application.Quit();
    }
}
