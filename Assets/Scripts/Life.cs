using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        transform.position = respawn.position;                                      //!\ POUVOIR AJOUTER/ENLEVER DES COEURS AVANT DE COMMENTER CE CODE /!\\
        player.GetComponent<Rigidbody>().useGravity = true;
        player.alive = true;
        deadGO.SetActive(false);
        heartsActive.Clear();
        controller.ResetAll();
        
        for (int j = 0; j < lifePoints; j++)
        {
            heartsActive.Add(hearts[j]);
        }
        for (int i = 0; i < heartsActive.Count; i++)
        {                          // Créer un ensemble de coeur ajustable dans le canvas (chercher sur internet)
            heartsActive[i].gameObject.SetActive(true);
        }
        life = heartsActive.Count;
        
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
            heartsActive[life].gameObject.SetActive(false);
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
