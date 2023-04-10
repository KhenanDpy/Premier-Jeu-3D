using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Movements : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

    [SerializeField]
    float rotate;

    [SerializeField]
    List<AnimatorController> animations = new List<AnimatorController>();

    private Vector3 oldMousePosition;

    Vector3 m_EulerAngleVelocity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        oldMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        if ((Input.anyKey == false) && (rb.position.y<=0.1f))
        {
            animator.runtimeAnimatorController = animations.ToArray()[1];
            //rb.velocity = Vector3.zero;
        }
            
        if (Input.GetKey(KeyCode.Z))
        {
            animator.runtimeAnimatorController = animations.ToArray()[7];
            if (rb.velocity.magnitude < 10f)
                rb.AddForce(transform.forward * 6f) ;
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.runtimeAnimatorController = animations.ToArray()[7];
            if (rb.velocity.magnitude < 10f)
                rb.AddForce(transform.forward * -6f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            animator.runtimeAnimatorController = animations.ToArray()[5];
            if (rb.velocity.magnitude < 10f)
                rb.AddForce(transform.right * -6f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.runtimeAnimatorController = animations.ToArray()[5];
            if (rb.velocity.magnitude < 10f)
                rb.AddForce(transform.right * 6f);
        }
        if (Input.mousePosition != oldMousePosition)
        {
            float rotation = Input.mousePosition.x - oldMousePosition.x;
            //rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -10, 0), 1);
            rb.AddTorque(transform.up * rotation, ForceMode.VelocityChange);
            oldMousePosition = Input.mousePosition;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.runtimeAnimatorController = animations.ToArray()[2];
            rb.AddForce(transform.up * 20f);
        }
        /*else
        {
            animator.runtimeAnimatorController = animations.ToArray()[1];
        }*/
    }
}
