using UnityEngine;

public class UILinear : MonoBehaviour
{
    [SerializeField] private GameObject lineLeft;
    [SerializeField] private GameObject lineRight;

    void Start()
    {
        RectTransform leftRectTransform = this.lineLeft.GetComponent<RectTransform>();
        leftRectTransform.anchoredPosition = this.ChangePosXRectTransform(leftRectTransform, false);

        RectTransform rightRectTransform = this.lineRight.GetComponent<RectTransform>();
        rightRectTransform.anchoredPosition = this.ChangePosXRectTransform(rightRectTransform, true);
    }

    private Vector2 ChangePosXRectTransform(RectTransform rectTransform, bool isRight)
    {
        Vector2 pos = Vector2.zero;

        pos = rectTransform.anchoredPosition;

        float changePosX = Screen.width / 4;

        if (isRight)
            pos.x = changePosX;
        else
            pos.x = -changePosX;

        return pos;
    }
}
