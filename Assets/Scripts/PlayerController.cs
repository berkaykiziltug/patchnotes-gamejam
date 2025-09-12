using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.5f; 
    [SerializeField] float jumpPower;
    private bool canJump;
    [SerializeField]private bool isGrounded;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    private float jumpForwardDistance = 4;
 
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.3f; 
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jumping");
            canJump = false;
            Jump();
        }
        
    }

    private void Jump()
    {
        Vector3 targetPosition = transform.position + transform.forward * jumpForwardDistance;
        transform.DOJump(targetPosition, jumpPower, 1, jumpDuration)
                 .SetEase(Ease.Linear)
                 .OnStart(() =>
                 {
                     //Rotating during jump
                     transform.DOLocalRotate(new Vector3(360f, 0f, 0f), jumpDuration, RotateMode.LocalAxisAdd)
                              .SetEase(Ease.Linear);
                 }).OnComplete(() =>
                 {
                     canJump = true;
                 });
    }
    

        void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 rayOrigin = transform.position + Vector3.up * 0.3f;
        Vector3 rayEnd = rayOrigin + Vector3.down * groundCheckDistance;

        Gizmos.DrawLine(rayOrigin, rayEnd);
    }
}
