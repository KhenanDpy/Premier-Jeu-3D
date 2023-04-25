using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTempToKill : MonoBehaviour
{
    Life life;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
