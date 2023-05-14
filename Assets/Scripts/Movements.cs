using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Movements : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    public bool lookUp;
    public bool alive   = true;

    [SerializeField]
    Camera lerpCamera;

    [SerializeField]
    float jump, speedLimit, zoom; //force,acceleration, 

    private RaycastHit slope;

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
    float groundedRadius = 0.32f;
    public LayerMask groundLayers;
    public float airSpeedFactor;

    private Vector3 mouvement;
    private float limit;

    // front and right direction (-1, 0, or 1)
    private int fdir = 0, rdir = 0;

    /* Pour les sons */
    bool walk, run, landing;
    public Sounds sounds;


    // Pour trouver depuis l'éditeur la bonne valeur
    public float raycastRange;
    private bool OnSlope()
    {
        if (Physics.Raycast(rb.transform.position, Vector3.down, out slope, raycastRange))
        {
            float angle = Vector3.Angle(Vector3.up, slope.normal);
            return angle < 35 && angle != 0;
        }
        return false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        // pour forcer le curseur à rester au centre
        //       UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Damageing"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((transform.up - transform.forward).normalized * jump, ForceMode.Impulse);
            Debug.Log("on recule");
        }
    }

    void Update()
    {
        if (alive)
        {

            // Positionnement de la sphère au pied du personnage
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            // On teste les collisions de la sphère dans le GroundedLayers
            isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
            animator.SetBool("Grounded", isGrounded);

            if (isGrounded)
                rb.drag = 5;
            else
                rb.drag = 0;

            // Si on n'appuie sur aucune touche
            if ((Input.anyKey == false))
            {
                animator.SetBool("Idle", true); // L'animation Idle s'active
                sounds.audioSource.Stop();
            }
            else
            {
                animator.SetBool("Idle", false); // L'animation Idle se désactive
            }


            limit = speedLimit;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                limit *= 1.5f;
            }
                
            if(!isGrounded)
            {
                limit *= airSpeedFactor;
            }

            if (Input.GetKey(KeyCode.Z))
            {
                fdir = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                fdir = -1;
            }
            else
                fdir = 0;

            if (Input.GetKey(KeyCode.D))
            {
                rdir = 1;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                rdir = -1;
            }
            else
                rdir = 0;

            Animate();

            // Si on appuie sur la barre d'espace
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                animator.SetBool("Jump", true); // L'animation Jump s'active
                animator.SetBool("Grounded", false); // L'animation Grounded se désactive

                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(transform.up * jump, ForceMode.Impulse);
            }

        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

    }

    // on dirait que le déplacement est plus fluide quand c'est dans le fixedupdate plutôt que le update
    private void FixedUpdate()
    {

        // gestion de la souris
        WhereToLook();

        mouvement = (transform.forward * fdir + transform.right * rdir).normalized;
        if (OnSlope())
        {
            mouvement = Vector3.ProjectOnPlane(mouvement, slope.normal).normalized;

            if (rb.velocity.y > 0f)
            {
                mouvement = (mouvement + Vector3.down * 0.02f).normalized;
            }
        }

        rb.AddForce(mouvement * limit * 20f, ForceMode.Force);

        Vector3 horizontalSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (horizontalSpeed.magnitude > limit)
        {
            Vector3 maxSpeed = horizontalSpeed.normalized * limit;
            rb.velocity = new Vector3(maxSpeed.x, rb.velocity.y, maxSpeed.z);
        }
    }

    private Vector3 cameraWantedPosition;

    public float lerpCameraSpeed = 3;

    private void WhereToLook()
    {
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

            float dy = 1.75f * (1f + rotationX / maxRotationX); // Variation de hauteur de la caméra par rapport à la rotation
            float dz = -zoom * Mathf.Cos(rotationX * Mathf.PI / 180f); // Eloignement de la caméra
            lerpCamera.gameObject.SetActive(true);


            cameraWantedPosition = this.transform.position + new Vector3(0f, dy, 0f) + this.transform.forward * dz;

            lerpCamera.transform.position = Vector3.Lerp(lerpCamera.transform.position, cameraWantedPosition, lerpCameraSpeed * Time.deltaTime);

            lerpCamera.transform.rotation = this.transform.rotation * Quaternion.Euler(rotationX, 0, 0);
            
        }

        

        //à mettre dans la soutenance : pour faire tourner la caméra autour du personnage
        //                              mais il faudrait adapter les déplacements (par rapport à la caméra) et les animations  

        /*posCamera.Set(rb.transform.position.x + distance * Mathf.Cos(rotationY * Mathf.PI / 180f),
                      rb.transform.position.y+3,
                      rb.transform.position.z + distance * Mathf.Sin(rotationY * Mathf.PI / 180f));

        camera.transform.SetPositionAndRotation(posCamera, Quaternion.identity);
        camera.transform.LookAt(rb.transform.position);*/

        // Zoom avec la molette de la souris
        if (Input.mouseScrollDelta.y > 0 && zoom > 0)
        {
            zoom--;
        }
        else if (Input.mouseScrollDelta.y < 0 && zoom < 5)
        {
            zoom++;
        }

    }

    private void Animate()
    {
        animator.SetBool("Forward", false);
        animator.SetBool("Backward", false);
        animator.SetBool("BackwardLeft", false);
        animator.SetBool("BackwardRight", false);
        animator.SetBool("ForwardLeft", false);
        animator.SetBool("ForwardRight", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Jump", false);

        if (fdir == 1)
        {
            if (rdir == 1)
                animator.SetBool("ForwardRight", true);
            else if (rdir == -1)
                animator.SetBool("ForwardLeft", true);
            else
                animator.SetBool("Forward", true);
        }
        else if (fdir == -1)
        {
            if (rdir == 1)
                animator.SetBool("BackwardRight", true);
            else if (rdir == -1)
                animator.SetBool("BackwardLeft", true);
            else
                animator.SetBool("Backward", true);
        }
        else
        {
            if (rdir == 1)
                animator.SetBool("Right", true);
            else if (rdir == -1)
                animator.SetBool("Left", true);
        }
    }
}
