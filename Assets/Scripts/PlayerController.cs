using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] float jumpPower;
    private Rigidbody rb;
    private bool canJump;
    [SerializeField] private bool isGrounded;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    [SerializeField] private float stamina = 250;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private float staminaRegenRate = 30f;

    private bool hasStamina => stamina > 20;



    private bool isRegenerating;

    //  private SphereCollider groundDetectionCollider;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        staminaSlider.minValue = 0f;
        staminaSlider.maxValue = 250f;
        staminaSlider.value = stamina;
        // groundDetectionCollider = GetComponent<SphereCollider>();
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 rayOrigin = transform.position + Vector3.up * 0.3f; //
        // isGrounded = Physics.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && stamina >= 20f && !isRegenerating)
        {
            Debug.Log("Jumping");
            canJump = true;
        }
        if (stamina <= 20f && !isRegenerating)
        {
        isRegenerating = true;
        canJump = false;
        StartCoroutine(RegenerateStamina());
        }

       
    }
    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(5f);

        while (stamina < staminaSlider.maxValue)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Min(stamina, staminaSlider.maxValue);
            UpdateStaminaSlider(stamina);
            yield return null;
        }
        isRegenerating = false;
        // canJump = true;
    }


    void FixedUpdate()
    {
        if (canJump)
        {
            Jump();
            AddRotation();
            AddForwardForce();
        }
        canJump = false;
    }

    private void Jump()
    {
        float randomJumpForce = UnityEngine.Random.Range(4, 15);
        float staminaCost = Mathf.Lerp(4, 15, (randomJumpForce - 4) / (15 - 4));
        Debug.Log($"Stamina cost is {staminaCost}");
        rb.AddForce(new Vector3(0, randomJumpForce, 0), ForceMode.Impulse);
        canJump = false;
        isGrounded = false;
        stamina = Mathf.Max(0, stamina - staminaCost);
        UpdateStaminaSlider(stamina);
    }

    private void AddRotation()
    {
        float randomRotateForce = UnityEngine.Random.Range(1, 3);
        rb.AddTorque(new Vector3(randomRotateForce, 0, 0), ForceMode.Impulse);
    }
    private void AddForwardForce()
    {
        float randomForwardForce = UnityEngine.Random.Range(1, 8);
        rb.AddForce(new Vector3(0, 0, randomForwardForce), ForceMode.Impulse);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 rayOrigin = transform.position + Vector3.up * 0.3f;
        Vector3 rayEnd = rayOrigin + Vector3.down * groundCheckDistance;

        Gizmos.DrawLine(rayOrigin, rayEnd);
    }

    private void UpdateStaminaSlider(float value)
    {
        staminaSlider.DOValue(value, .25f);
    }
}
