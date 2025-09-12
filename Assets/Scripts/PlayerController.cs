using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool canJump;
    private bool isGrounded;
    private Rigidbody rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce;
    [SerializeField] private float forwardForce;
    [SerializeField] private float groundCheckDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance , groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            canJump = true;
        }
    }
    void FixedUpdate()
    {
        if (canJump)
        {
             Vector3 jumpVector = Vector3.up * jumpForce + transform.forward * forwardForce;
            rb.AddForce(jumpVector, ForceMode.Impulse);
            canJump = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
