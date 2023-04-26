using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScriptTempToKill : MonoBehaviour // Mettre tout le contenu de ce script dans un autre plus tard, celui là ne sert que de test.
{
    public Life life;

    float invincibilityFrame = 0.0f;

    bool touched = false;

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
            touched = true;
            if (touched && invincibilityFrame <= 0.0f)
            {
                life.TakeDamage(1);
                invincibilityFrame += Time.deltaTime;
            }
            touched = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityFrame >= invincibilityFrame+Time.deltaTime)
        {
            invincibilityFrame = 0.0f;
        }
    }
}
