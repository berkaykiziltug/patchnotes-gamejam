using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI settingsText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString("#FF9C00", out Color newColor))
        {
            settingsText.color = newColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        settingsText.color = Color.white;
    }

}
