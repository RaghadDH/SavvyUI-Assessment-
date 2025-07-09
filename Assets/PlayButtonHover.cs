using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButtonHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Color Settings")]
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(0.9f, 0.9f, 0.9f);
    public Color pressedColor = new Color(0.7f, 0.7f, 0.7f);

    [Header("Hand Settings")]
    public RectTransform handIcon;
    public Vector2 handOffset = new Vector2(0f, 130f);
    public float handAppearSpeed = 10f;

    private Image buttonImage;
    private bool isHovering = false;
    private RectTransform buttonRectTransform;
    private Canvas parentCanvas;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.color = normalColor;

        buttonRectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();

        if (handIcon != null)
        {
            handIcon.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        buttonImage.color = hoverColor;

        if (handIcon != null)
        {
            handIcon.gameObject.SetActive(true);
            UpdateHandPosition();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        buttonImage.color = normalColor;

        if (handIcon != null)
        {
            handIcon.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.color = isHovering ? hoverColor : normalColor;
    }

    void Update()
    {
        if (isHovering && handIcon != null)
        {
            UpdateHandPositionSmooth();
        }
    }

    private void UpdateHandPosition()
    {
        Vector2 localPoint;
       
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            RectTransformUtility.WorldToScreenPoint(null, buttonRectTransform.position),
            parentCanvas.worldCamera,
            out localPoint);

        handIcon.anchoredPosition = localPoint + handOffset;
    }

    private void UpdateHandPositionSmooth()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            RectTransformUtility.WorldToScreenPoint(null, buttonRectTransform.position),
            parentCanvas.worldCamera,
            out localPoint);

        Vector2 targetPos = localPoint + handOffset;
        handIcon.anchoredPosition = Vector2.Lerp(handIcon.anchoredPosition, targetPos, handAppearSpeed * Time.deltaTime);
    }
}
