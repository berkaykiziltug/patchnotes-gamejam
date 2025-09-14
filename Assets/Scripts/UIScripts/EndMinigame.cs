using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class EndMinigame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;

    [SerializeField] private RectTransform[] uiElements;

    [SerializeField] private GameObject applicationNotResponding;

    [SerializeField] private Button clickButton;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip applicationNotRespondingClip;
    [SerializeField] private RectTransform theEndTMP;
    [SerializeField] private RectTransform goodCone;

    private int counter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 50)
        {
            StartCoroutine(ApplicationNotResponding());
        }
    }

    public void IncreaseNumber()
    {
        counter++;
        numberText.text = counter.ToString();
        ShakeUIElements();
    }
    private void ShakeUIElements()
    {
        foreach (RectTransform element in uiElements)
        {
            if (element == null) continue;

            element.DOShakeAnchorPos(
                duration: 0.1f,
                strength: new Vector2(30, 20),
                vibrato: 20,
                randomness: 200,
                snapping: false,
                fadeOut: true
            );
        }
    }

    private IEnumerator ApplicationNotResponding()
    {
        AudioManager.Instance.PlaySFX(applicationNotRespondingClip);
        clickButton.enabled = false;
        applicationNotResponding.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        // clickButton.enabled = true;
        // applicationNotResponding.gameObject.SetActive(false);
        foreach (RectTransform element in uiElements)
        {
            if (element != null)
                element.gameObject.SetActive(false);
        }
        applicationNotResponding.gameObject.SetActive(false);
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.StopSFX();
        yield return new WaitForSeconds(2);
         // Animate theEndTMP and goodCone from off-screen below
        RectTransform[] elementsToAnimate = { theEndTMP, goodCone };
        foreach (RectTransform rt in elementsToAnimate)
    {
        if (rt == null) continue;

        Vector2 originalPos = rt.anchoredPosition;                  // store original position
        rt.anchoredPosition = new Vector2(originalPos.x, -Screen.height); // start below screen

        rt.gameObject.SetActive(true);

        rt.DOAnchorPos(originalPos, 1f).SetEase(Ease.OutBack);      // animate to original position
    }

    }

    public void PlaySFXPitch()
    {
        AudioManager.Instance.PlaySFX(clickSound, 0.5f, 1);
    }
}
