using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Movements : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

    [SerializeField]
    Text text;

    [SerializeField]
    List<AnimatorController> animations = new List<AnimatorController>();

    [SerializeField]
    float speed, jump, limit;

    float rotationY = 0f;
    public float sensitivity = 15f;

    //Grounded
    bool isGrounded;
    float groundedOffset = -0.14f;
    float groundedRadius = 0.28f;
    public LayerMask groundLayers;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // rotation par la souris
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rb.transform.localEulerAngles = new Vector3(0, rotationY, 0);

        animator.SetBool("Forward", false);

        // si on est au sol, on peut se déplacer
        if (rb.position.y <= 0.02)
        {
            animator.SetBool("Jump", false);

            // Positionnement de la sphère au pied de l'Armature
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            // On teste les collisions de la sphère dans le GroundedLayers
            isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
            animator.SetBool("Grounded", isGrounded);

            if ((Input.anyKey == false))//&& (rb.position.y<=0.1f))
            {
                //animator.runtimeAnimatorController = animations.ToArray()[1];
                //rb.velocity = Vector3.zero; //stop les glissades
                animator.SetBool("Idle", true);
                rb.drag = 5f; // pour pas glisser
            }
            else
            {
                animator.SetBool("Idle", false);
                rb.drag = 0f;
            }

            if (Input.GetKey(KeyCode.Z))
            {
                animator.SetBool("Forward", true);
                //animator.runtimeAnimatorController = animations.ToArray()[7];
                if (rb.velocity.magnitude < limit)
                    rb.AddForce(transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //animator.runtimeAnimatorController = animations.ToArray()[7];
                if (rb.velocity.magnitude < limit)
                    rb.AddForce(transform.forward * -speed);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                //animator.runtimeAnimatorController = animations.ToArray()[5];
                if (rb.velocity.magnitude < limit)
                    rb.AddForce(transform.right * -speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                //animator.runtimeAnimatorController = animations.ToArray()[5];
                if (rb.velocity.magnitude < limit)
                    rb.AddForce(transform.right * speed);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Jump", true);
                animator.SetBool("Grounded", false);
                //animator.runtimeAnimatorController = animations.ToArray()[2];
                rb.AddForce(transform.up * jump);

            }

            text.text = "Vitesse : " + rb.velocity.magnitude.ToString();
            /*else
            {
                animator.runtimeAnimatorController = animations.ToArray()[1];
            }*/
        }
    }
}
