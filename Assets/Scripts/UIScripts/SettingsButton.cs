using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI settingsText;
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
            settingsText.color = newColor;
            AudioManager.Instance.PlaySFX(onHoverClip);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        settingsText.color = Color.white;
    }

      public void SetColorToWhite()
    {
        if (settingsText != null)
        {
            settingsText.color = Color.white;
        }
    }

}
