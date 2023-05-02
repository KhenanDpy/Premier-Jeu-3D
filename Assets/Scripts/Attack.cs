using UnityEngine;

public class Attack : MonoBehaviour
{
    public Life life;

    bool canTouch;

    void Start()
    {
        canTouch = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (canTouch)
        {
            if (collision.collider.gameObject.CompareTag("Weakness"))
            {
                Debug.Log("coule");
                Destroy(collision.gameObject);
            }
            else if (collision.collider.gameObject.CompareTag("Damageing"))
            {
                Debug.Log("touche");
                life.TakeDamage(1);
                canTouch = false;
                Invoke(nameof(ResetCollision), 2); // un délai de 2 sec
            }
        }

    }

    private void ResetCollision()
    {
        canTouch = true;
    }
}
