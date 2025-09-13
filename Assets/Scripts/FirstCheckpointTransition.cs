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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            StartCoroutine(TeleportPlayer());
           
        }
    }


    private IEnumerator TeleportPlayer()
    {
        // var col = ps.colorOverLifetime;
        // Gradient grad = col.color.gradient;

        //     // copy existing keys
        // GradientColorKey[] colorKeys = grad.colorKeys;
        // GradientAlphaKey[] alphaKeys = grad.alphaKeys;

        //     // change ONE color key (e.g. the last one)
        // colorKeys[colorKeys.Length - 1].color = Color.green;

        //     // create a new gradient with the modified keys
        // Gradient newGrad = new Gradient();
        // newGrad.SetKeys(colorKeys, alphaKeys);

        //     // assign back
        // col.color = new ParticleSystem.MinMaxGradient(newGrad);

         // get all particle systems on this object and children
        ParticleSystem[] systems = parentPs.GetComponentsInChildren<ParticleSystem>();

        foreach (var ps in systems)
        {
            var col = ps.colorOverLifetime;
            Gradient grad = col.color.gradient;

            // copy existing keys
            GradientColorKey[] colorKeys = grad.colorKeys;
            GradientAlphaKey[] alphaKeys = grad.alphaKeys;

            // clamp index so we don't go out of bounds
            int index = Mathf.Clamp(keyIndexToChange, 0, colorKeys.Length - 1);
            colorKeys[index].color = newColorGreen;

            // create a new gradient with the modified keys
            Gradient newGrad = new Gradient();
            newGrad.SetKeys(colorKeys, alphaKeys);

            // assign back
            col.color = new ParticleSystem.MinMaxGradient(newGrad);
        }
        yield return new WaitForSeconds(.2f);
        parentPs.gameObject.SetActive(false);
        
        mosaicGameObject.SetActive(true);

        float elapsedTime = 0f;
        float duration = 1;
        float startValue = 1080;
        float endValue = 10;
         while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Smooth interpolation
            float currentValue = Mathf.Lerp(startValue, endValue, t);

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
        yield return null;

        player.transform.position = playerStartPosition2.transform.position;
        player.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(1f);

        elapsedTime = 0f;
        duration = 1;
        startValue = 10;
        endValue = 1080;
         while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Smooth interpolation
            float currentValue = Mathf.Lerp(startValue, endValue, t);

            // Apply to shader
            mosaicMaterial.SetFloat("_Resolution", currentValue);

            yield return null;
        }
        mosaicMaterial.SetFloat("_Resolution", 1080);
        yield return null;
        mosaicGameObject.SetActive(false);

        playerRigidbody.isKinematic = false;
        yield return null;
    }
}
