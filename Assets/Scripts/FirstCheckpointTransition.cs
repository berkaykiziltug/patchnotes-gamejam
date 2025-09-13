using System.Collections;
using UnityEngine;

public class FirstCheckpointTransition : MonoBehaviour
{
    [SerializeField] PlayerController player;

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private GameObject playerStartPosition2;
    [SerializeField] private GameObject mosaicGameObject;

    [SerializeField] private Material mosaicMaterial;

    [SerializeField] private ParticleSystem parentPs;

    private Color newColorGreen = Color.green;
    private int keyIndexToChange = 0;

    private bool canTriggerNextLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canTriggerNextLevel = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController) && canTriggerNextLevel)
        {
            canTriggerNextLevel = false;
            StartCoroutine(TeleportPlayer());
        }
    }


    private IEnumerator TeleportPlayer()
    {
        player.canMove = false;
        
        ParticleSystem[] systems = parentPs.GetComponentsInChildren<ParticleSystem>();

        foreach (var ps in systems)
        {
            var col = ps.colorOverLifetime;
            Gradient grad = col.color.gradient;

            // copy existing keys
            GradientColorKey[] colorKeys = grad.colorKeys;
            GradientAlphaKey[] alphaKeys = grad.alphaKeys;

            
            int index = Mathf.Clamp(keyIndexToChange, 0, colorKeys.Length - 1);
            colorKeys[index].color = newColorGreen;

            
            Gradient newGrad = new Gradient();
            newGrad.SetKeys(colorKeys, alphaKeys);

            
            col.color = new ParticleSystem.MinMaxGradient(newGrad);
        }
        yield return new WaitForSeconds(.8f);
        parentPs.gameObject.SetActive(false);
        mosaicGameObject.SetActive(true);

        float elapsedTime = 0f;
        float duration = .8f;
        float startValue = 1080;
        float endValue = 10;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            //Interpolate here
            float currentValue = Mathf.Lerp(startValue, endValue, Mathf.SmoothStep(0f, 1f, t));

            // Apply to shader
            mosaicMaterial.SetFloat("_Resolution", currentValue);

            yield return null;
        }
        mosaicMaterial.SetFloat("_Resolution", 10);


        yield return new WaitForSeconds(1f);

        playerRigidbody.linearVelocity = Vector3.zero;
        yield return null;
        playerRigidbody.angularVelocity = Vector3.zero;
        yield return null;
        playerRigidbody.isKinematic = true;
        yield return new WaitForSeconds(1);


        player.transform.position = playerStartPosition2.transform.position;
        player.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(2f);

        elapsedTime = 0f;
        duration = .8f;
        startValue = 10;
        endValue = 1080;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            float currentValue = Mathf.Lerp(startValue, endValue, Mathf.SmoothStep(0f, 1f, t));

            mosaicMaterial.SetFloat("_Resolution", currentValue);

            yield return null;
        }
        mosaicMaterial.SetFloat("_Resolution", 1080);
        yield return null;
        mosaicGameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        player.canMove = true;
        playerRigidbody.isKinematic = false;
        GameManager.isSecondPhase = true;
    }
}
