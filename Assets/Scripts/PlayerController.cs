using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private AudioClip groundHitClip;
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private GameObject particleSpawnPosition;
    [SerializeField] private GameObject playerUIGO;
    [SerializeField] private AudioClip[] jumpClips;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private AudioSource playerAudioSource;
    private Rigidbody rb;
    private bool canJump;
    [SerializeField] private Transform playerStartPosition;

    [SerializeField] private Transform playerStartPosition2;

    [SerializeField] private Transform playerStartPosition3;
    
    [SerializeField] private bool isGrounded;
    [SerializeField] public bool canMove;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    [SerializeField] private float stamina = 250;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private float normalStaminaRegenRate = 30f;

    [SerializeField] private float increasedStaminaRegenRate = 40f;
    private bool hasStamina => stamina > 20;
    private int maxJumps = 2;
    public int jumpsLeft;

    private Vector3 scale;
    private bool isRegenerating;

    private bool isSecondGroundHit;

    private bool canTriggerHitSound;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSecondGroundHit = false;
        canTriggerHitSound = false;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        rb = GetComponent<Rigidbody>();
        staminaSlider.minValue = 0f;
        staminaSlider.maxValue = 250f;
        staminaSlider.value = stamina;
        jumpsLeft = maxJumps;
        canMove = true;
        scale = transform.localScale;
    }



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground") && isSecondGroundHit)
        {
            isGrounded = true;
            jumpsLeft = maxJumps;
            GameManager.Instance.canOpenMenu = true;
            
            
        }
        if (collision.gameObject.CompareTag("Lava"))
        {
            canMove = false;
            Debug.Log("LOSE!");
            StartCoroutine(ResetPlayerPosition());
            AudioManager.Instance.PlaySFX(groundHitClip);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            
        }
    }

    private IEnumerator ResetPlayerPosition()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        yield return new WaitForSeconds(1.5f);
        float elapsedTime = 0f;
        float duration = .2f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            yield return null;
        }
        startScale = targetScale;

        transform.position = playerStartPosition.position;
        transform.rotation = Quaternion.identity;
        StartCoroutine(ReEnableRigidBody());
        
    }

    private IEnumerator ReEnableRigidBody()
    {
        yield return new WaitForSeconds(.5f);
        float elapsedTime = 0f;
        float duration = .2f; //Can maybe crank this up a bit for slower reappearance.
        Vector3 startScale = transform.localScale; // this is the shrinked scale;
        Vector3 targetScale = scale; // this is the full scale before shrinking;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            yield return null;
        }
        startScale = targetScale;
        rb.isKinematic = false;
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        if (GameManager.isSecondPhase)
        {
            playerStartPosition = playerStartPosition2;
        }
        if ( GameManager.isSecondPhase&&GameManager.isThirdPhase)
        {
            playerStartPosition = playerStartPosition3;
        }

        if (Input.GetKeyDown(KeyCode.Space) && stamina >= 20f && jumpsLeft > 0)
        {
            Debug.Log("Jumping");
            canJump = true;
            GameManager.Instance.canOpenMenu = false;

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

        if (!GameManager.Instance.canOpenMenu) return;

        if (canMove && Input.GetKeyDown(KeyCode.Tab) && GameManager.Instance.canOpenMenu)
        {
            mainMenu.SetActive(true);
            playerUIGO.SetActive(false);
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
            isSecondGroundHit = true;
        }
        canJump = false;
    }

    private void Jump()
    {
        GameObject spawnedParticle = Instantiate(
        particlesPrefab,
        particleSpawnPosition.transform.position,
        Quaternion.identity
        );
        

        Destroy(spawnedParticle, 1);

        int randomJumpForce = UnityEngine.Random.Range(6, 16);
        float staminaCost = 0;
     switch (randomJumpForce)
    {
        case 6: staminaCost = 5; break;
        case 7: staminaCost = 10; break;
        case 8: staminaCost = 15; break;
        case 9: staminaCost = 20; break;
        case 10: staminaCost = 25; break;
        case 11: staminaCost = 30; break;
        case 12: staminaCost = 35; break;
        case 13: staminaCost = 40; break;
        case 14: staminaCost = 45; break;
        case 15: staminaCost = 50; break;
        default: staminaCost = 15; break; 
    }
    //JUMPING SOUNDS.
    if (jumpClips.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, jumpClips.Length);
            AudioManager.Instance.PlaySFX(jumpClips[randomIndex]);
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
        float randomRotateForce = UnityEngine.Random.Range(.5f, 1f);
        rb.AddTorque(new Vector3(randomRotateForce, 0, 0), ForceMode.Impulse);
    }
    private void AddForwardForce()
    {
        float randomForwardForce = UnityEngine.Random.Range(6, 9);
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
