using UnityEngine;
using DG.Tweening;

public class MenuUIAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform[] uiElements; // assign slider + icon
    [SerializeField] private float slideDuration = .2f;
    [SerializeField] private float delayBetween = 0.2f;

    private Vector2[] originalPositions;

    void Awake()
    {
        // Cache original positions
        originalPositions = new Vector2[uiElements.Length];
        for (int i = 0; i < uiElements.Length; i++)
        {
            originalPositions[i] = uiElements[i].anchoredPosition;
        }
    }

    void OnEnable()
    {
        AnimateIn();
    }

    private void AnimateIn()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < uiElements.Length; i++)
        {
            // Put each element offscreen bottom-left
            uiElements[i].anchoredPosition = new Vector2(-Screen.width, -Screen.height);

            // Slide to original
            seq.Append(uiElements[i].DOAnchorPos(originalPositions[i], slideDuration)
                                   .SetEase(Ease.OutCubic));
            seq.AppendInterval(delayBetween);
        }
    }
}