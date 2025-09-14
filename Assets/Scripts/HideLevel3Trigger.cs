using System.Collections;
using UnityEngine;

public class HideLevel3Trigger : MonoBehaviour
{
    [SerializeField] private GameObject mosaicGameObject;

    [SerializeField] private Material mosaicMaterial;

    [SerializeField] private PlayerController player;
    [SerializeField] private Rigidbody playerRb;

    [SerializeField] private GameObject endGamePanel;

     private bool canHideLevel;

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController player) && canHideLevel)
        {
            canHideLevel = false;
            GameManager.isThirdPhase = true;
            StartCoroutine(ShowAndHideMosaic());
            playerRb.isKinematic= true;
            player.enabled = false;
            endGamePanel.gameObject.SetActive(true);
        }
    }
     private IEnumerator ShowAndHideMosaic()
    {
        mosaicGameObject.SetActive(true);

        float elapsedTime = 0f;
        float duration = .1f;
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

        elapsedTime = 0f;
        duration = .07f;
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
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canHideLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
