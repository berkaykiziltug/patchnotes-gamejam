using System;
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
    [SerializeField] private float normalStaminaRegenRate = 30f;

    [SerializeField] private float increasedStaminaRegenRate = 40f;
    private bool hasStamina => stamina > 20;
    private int maxJumps = 2;
    private int jumpsLeft;


    private bool isRegenerating;

    //  private SphereCollider groundDetectionCollider;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        staminaSlider.minValue = 0f;
        staminaSlider.maxValue = 250f;
        staminaSlider.value = stamina;
        // groundDetectionCollider = GetComponent<SphereCollider>();\
        jumpsLeft = maxJumps;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsLeft = maxJumps;
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

        if (Input.GetKeyDown(KeyCode.Space) && stamina >= 20f && jumpsLeft > 0)
        {
            Debug.Log("Jumping");
            canJump = true;
            
        }
        else if (stamina < 20f)
        {
            canJump = false;
        }

        float currentRegenRate = (stamina <= staminaSlider.minValue) ? increasedStaminaRegenRate : normalStaminaRegenRate;

        if (stamina <= staminaSlider.maxValue)
        {
            stamina += currentRegenRate * Time.deltaTime;
            stamina = Mathf.Min(stamina, staminaSlider.maxValue);
            UpdateStaminaSlider(stamina);
        }
    
    }
    // private IEnumerator RegenerateStamina()
    // {
    //     yield return new WaitForSeconds(5f);

    //     while (stamina < staminaSlider.maxValue)
    //     {
    //         stamina += staminaRegenRate * Time.deltaTime;
    //         stamina = Mathf.Min(stamina, staminaSlider.maxValue);
    //         UpdateStaminaSlider(stamina);
    //         yield return null;
    //     }
    //     isRegenerating = false;
    //     // canJump = true;
    // }


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
        int randomJumpForce = UnityEngine.Random.Range(4, 15);
        float staminaCost = 0;
        switch (randomJumpForce)
        {
            case 4: staminaCost = 5; break;
            case 5: staminaCost = 10; break;
            case 6: staminaCost = 15; break;
            case 7: staminaCost = 20; break;
            case 8: staminaCost = 25; break;
            case 9: staminaCost = 30; break;
            case 10: staminaCost = 35; break;
            case 11: staminaCost = 40; break;
            case 12: staminaCost = 45; break;
            case 13: staminaCost = 50; break;
            case 14: staminaCost = 55; break;
            case 15: staminaCost = 60; break;
            default: staminaCost = 10; break;
        }
        Debug.Log($"Stamina cost is {staminaCost}");
        rb.AddForce(new Vector3(0, randomJumpForce, 0), ForceMode.Impulse);
        canJump = false;
        isGrounded = false;
        jumpsLeft--;
        stamina = Mathf.Max(0, stamina - staminaCost);
        UpdateStaminaSlider(stamina);
    }

    private void AddRotation()
    {
        float randomRotateForce = UnityEngine.Random.Range(.5f, .8f);
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
