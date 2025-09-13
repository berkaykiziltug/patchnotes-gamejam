using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CreditsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI creditsText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString("#FF9C00", out Color newColor))
        {
            creditsText.color = newColor;
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
