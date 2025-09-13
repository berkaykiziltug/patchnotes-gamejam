using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI backText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString("#FF9C00", out Color newColor))
        {
            backText.color = newColor;
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
