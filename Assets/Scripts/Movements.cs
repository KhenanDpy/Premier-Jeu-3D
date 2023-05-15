using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/*
 * The movements of the player. It also has every animations linked to each movement.
 * The physics isn't good, there are adjusments to do.
 */

public class Movements : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    public bool lookUp;
    public bool alive   = true;

    [SerializeField]
    Camera lerpCamera;

    [SerializeField]
    float jump, speedLimit, zoom; 

    private RaycastHit slope;

    // Parameters of the mouse
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


    // Changeable in the editor if necessary
    public float raycastRange;
    private bool OnSlope() // when on a slope, the normal change so the mouvement is orientated in the good direction
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
        // To force the cursor to stay centered
        // UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Damageing")) // When taking damage there is a little knockback (not really working)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((transform.up - transform.forward).normalized * jump, ForceMode.Impulse);
        }
    }

    void Update()
    {
        if (alive)
        {

            // Position the sphere to the feet of the player
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            // It tests if the sphere is on the groundedlayers. If yes, then the player is grounded
            isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
            animator.SetBool("Grounded", isGrounded);

            if (isGrounded)
                rb.drag = 5;
            else
                rb.drag = 0;

            // If no key is pressed
            if ((Input.anyKey == false))
            {
                animator.SetBool("Idle", true); // the idle animation is activated
            }
            else
            {
                animator.SetBool("Idle", false); // the idle animation is desactivated
            }


            limit = speedLimit;
            if (Input.GetKey(KeyCode.LeftShift)) // if shift is pressed, the player moves faster
            {
                limit *= 1.5f;
            }
                
            if(!isGrounded) // to add air control
            {
                limit *= airSpeedFactor; // airSpeedFactor is adjustable from the editor
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

            // If spacebar is pressed
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                animator.SetBool("Jump", true); // the jump animation is activated
                animator.SetBool("Grounded", false); // the grounded animation is desactivated

                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(transform.up * jump, ForceMode.Impulse);
            }

        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

    }

    // for better fluidity
    private void FixedUpdate()
    {

        // management of the mouse
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
        // rotation of the mouse
        rotationY += Input.GetAxis("Mouse X") * sensitivityH;
        rb.transform.localEulerAngles = new Vector3(0, rotationY, 0);

        if (lookUp) // to activate the vertical mouvement of the camera (from editor)
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

            float dy = 1.75f * (1f + rotationX / maxRotationX); // Variation of the height of the camera to its rotation
            float dz = -zoom * Mathf.Cos(rotationX * Mathf.PI / 180f); // Distance of the camera
            lerpCamera.gameObject.SetActive(true);


            cameraWantedPosition = this.transform.position + new Vector3(0f, dy, 0f) + this.transform.forward * dz;

            lerpCamera.transform.position = Vector3.Lerp(lerpCamera.transform.position, cameraWantedPosition, lerpCameraSpeed * Time.deltaTime);

            lerpCamera.transform.rotation = this.transform.rotation * Quaternion.Euler(rotationX, 0, 0);
            
        }



        // Zoom with the mouse scroll wheel
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
