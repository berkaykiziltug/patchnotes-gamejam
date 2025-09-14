using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CreditsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI creditsText;
    [SerializeField] private AudioClip onHoverClip;
 
    [SerializeField] private AudioClip onClickClip;


    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(onClickClip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString("#FF9C00", out Color newColor))
        {
            creditsText.color = newColor;
            AudioManager.Instance.PlaySFX(onHoverClip);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        creditsText.color = Color.white;
    }

      public void SetColorToWhite()
    {
        if (creditsText != null)
        {
            creditsText.color = Color.white;
        }
    }

}
