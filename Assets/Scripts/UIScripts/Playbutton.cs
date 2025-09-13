using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Playbutton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI playText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString("#FF9C00", out Color newColor))
        {
            playText.color = newColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playText.color = Color.white;
    }

    public void SetColorToWhite()
    {
        if (playText != null)
        {
            playText.color = Color.white;
        }
    }

}
