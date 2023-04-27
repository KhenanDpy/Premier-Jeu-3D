using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScriptTempToKill : MonoBehaviour // Mettre tout le contenu de ce script dans un autre plus tard, celui là ne sert que de test.
{
    public Life life;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Weakness"))
        {
            Debug.Log("coule");
            Destroy(collision.gameObject);
        }else if (collision.collider.gameObject.CompareTag("Damageing"))
        {
            Debug.Log("touche");
            life.TakeDamage(1);
        }
    }

    private void ResetCollision()
    {
        //canTouch = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
