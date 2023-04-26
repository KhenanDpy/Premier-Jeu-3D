using UnityEngine;
using UnityEngine.UI;

public class OldMovements : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    public bool lookUp;

    [SerializeField]
    Text text;

    [SerializeField]
    Camera _camera;

    [SerializeField]
    float force, jump, speedLimit, acceleration, zoom;

    /* Paramétrage de la souris */
    float rotationX = 0f;
    float maxRotationX = 30f;
    float minRotationX = -30f;
    float rotationY = 0f;
    public float sensitivityH;
    public float sensitivityV;

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
        force = 2 * rb.mass * acceleration;

        // rotation par la souris
        rotationY += Input.GetAxis("Mouse X") * sensitivityH;
        rb.transform.localEulerAngles = new Vector3(0, rotationY, 0);

        if (lookUp) // pour activer le mouvement vertical de la caméra
        {
            float deltaX = -Input.GetAxis("Mouse Y") * sensitivityV;
            if ((deltaX > 0) && (rotationX < maxRotationX))
            {
                rotationX += deltaX;
                if (rotationX > maxRotationX)
                    rotationX = maxRotationX;
            }
            else if ((deltaX < 0) && (rotationX > minRotationX))
            {
                rotationX += deltaX;
                if (rotationX < minRotationX)
                    rotationX = minRotationX;
            }
            float dy = 1.75f * (1f + rotationX / maxRotationX);
            float dz = -zoom * Mathf.Cos(rotationX * Mathf.PI / 180f);
            _camera.transform.SetLocalPositionAndRotation(new Vector3(0f, dy, dz), Quaternion.identity);
            _camera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        }


        //à mettre dans la soutenance : pour faire tourner la caméra autour du personnage
        //                              mais il faudrait adapter les déplacements (par rapport à la caméra) et les animations  

        /*posCamera.Set(rb.transform.position.x + distance * Mathf.Cos(rotationY * Mathf.PI / 180f),
                      rb.transform.position.y+3,
                      rb.transform.position.z + distance * Mathf.Sin(rotationY * Mathf.PI / 180f));

        camera.transform.SetPositionAndRotation(posCamera, Quaternion.identity);
        camera.transform.LookAt(rb.transform.position);*/

        animator.SetBool("Forward", false);
        animator.SetBool("Backward", false);
        animator.SetBool("BackwardLeft", false);
        animator.SetBool("BackwardRight", false);
        animator.SetBool("ForwardLeft", false);
        animator.SetBool("ForwardRight", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);


        // Positionnement de la sphère au pied du personnage
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        // On teste les collisions de la sphère dans le GroundedLayers
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        animator.SetBool("Grounded", isGrounded);


        //if (isGrounded) // si on est au sol, on peut se déplacer
        //{
        animator.SetBool("Jump", false);

        // Si on n'appuie sur aucune touche
        if ((Input.anyKey == false))
        {
            animator.SetBool("Idle", true); // L'animation Idle s'active
        }
        else
        {
            animator.SetBool("Idle", false); // L'animation Idle se désactive
        }

        // Si on appuie sur la touche "Z" ET "Q"
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("ForwardLeft", true);
            if (rb.velocity.magnitude < speedLimit)
                rb.AddForce((transform.forward - transform.right) * force);
        }
        // Sinon si on appuie sur la touche "Z" ET "D"
        else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.D))
        {
            animator.SetBool("ForwardRight", true);
            if (rb.velocity.magnitude < speedLimit)
                rb.AddForce((transform.forward + transform.right) * force);
        }
        // Si on appuie sur la touche "S" ET "Q"
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("BackwardLeft", true);
            if (rb.velocity.magnitude < speedLimit)
                rb.AddForce((-transform.forward - transform.right) * force);
        }
        // Sinon si on appuie sur la touche "S" ET "D"
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            animator.SetBool("BackwardRight", true);
            if (rb.velocity.magnitude < speedLimit)
                rb.AddForce((-transform.forward + transform.right) * force);
        }
        // Sinon si on appuie sur la touche "Z"
        else if (Input.GetKey(KeyCode.Z))
        {
            animator.SetBool("Forward", true); // L'animation Forward s'active
            if (rb.velocity.magnitude < speedLimit)
                rb.AddForce(transform.forward * force);
        }
        // Sinon si on appuie sur la touche "S"
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Backward", true); // L'animation Backward s'active
            if (rb.velocity.magnitude < speedLimit)
                rb.AddForce(-transform.forward * force);
        }
        // Sinon si on appuie sur la touche "Q"
        else if (Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("Left", true);
            if (rb.velocity.magnitude < speedLimit)
                rb.AddForce(-transform.right * force);
        }
        // Sinon si on appuie sur la touche "D"
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Right", true);
            if (rb.velocity.magnitude < speedLimit)
                rb.AddForce(transform.right * force);
        }

        // Si on appuie sur la barre d'espace
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("Jump", true); // L'animation Jump s'active
            animator.SetBool("Grounded", false); // L'animation Grounded se désactive
            rb.AddForce(transform.up * jump);

        }

        // Zoom avec la molette de la souris
        if (Input.mouseScrollDelta.y > 0 && zoom > 0)
        {
            zoom--;
        }
        else if (Input.mouseScrollDelta.y < 0 && zoom < 5)
        {
            zoom++;
        }

        text.text = "Vitesse : " + rb.velocity.magnitude.ToString();
        //}
    }
}
