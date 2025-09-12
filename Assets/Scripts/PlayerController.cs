using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.5f; // time in seconds for the jump
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
        Vector3 rayOrigin = transform.position + Vector3.up * 0.3f; //
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

    //Number of spins player can do. 4 maybe a bit too much. Can reduce it to 3 or 2.
    int spins = UnityEngine.Random.Range(1, 4); 

    
    float rotationDuration = jumpDuration; 

    transform.DOJump(targetPosition, jumpPower, 1, jumpDuration)
             .SetEase(Ease.Linear)
             .OnStart(() =>
             {
                 
                 transform.DOLocalRotate(new Vector3(360f * spins, 0f, 0f), rotationDuration, RotateMode.FastBeyond360)
                          .SetEase(Ease.Linear);
             })
             .OnComplete(() =>
             {
                 
                 transform.rotation = Quaternion.identity;
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
