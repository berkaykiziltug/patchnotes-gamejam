using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Playbutton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private AudioClip onHoverClip;
    [SerializeField] private AudioClip onClickClip;
    [SerializeField] private RectTransform[] buttons;
     [SerializeField] private float slideDuration = 1f;
    [SerializeField] private float buttonDelay = 0.2f;

    private Vector2[] originalPositions;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString("#FF9C00", out Color newColor))
        {
            playText.color = newColor;
            AudioManager.Instance.PlaySFX(onHoverClip);
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

    void OnEnable()
    {
        playText.color = Color.white;
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < buttons.Length; i++)
        {
            // Start offscreen to the left
            buttons[i].anchoredPosition = new Vector2(-Screen.width, originalPositions[i].y);

            // Add each button's tween to the sequence with a delay
            seq.Append(buttons[i].DOAnchorPos(originalPositions[i], slideDuration)
                                  .SetEase(Ease.OutCubic));
            seq.AppendInterval(buttonDelay); // wait before next button
        }
    }

    void Awake()
    {
        // Cache the original positions of all buttons
        originalPositions = new Vector2[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalPositions[i] = buttons[i].anchoredPosition;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(onClickClip);
    }
}
