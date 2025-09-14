using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI backText;
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
            backText.color = newColor;
            AudioManager.Instance.PlaySFX(onHoverClip);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backText.color = Color.white;
    }
     public void SetColorToWhite()
    {
        if (backText != null)
        {
            backText.color = Color.white;
        }
    }

}
