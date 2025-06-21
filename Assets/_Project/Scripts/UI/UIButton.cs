using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Mouse hover setting")]
    public float scaleSpeed = 1f;
    public float scaleRate = 1.2f;

    private Vector3 defaultScale;
    private Vector3 targetScale;

    private Image imageButton;
    private TextMeshProUGUI textButton;

    [Header("Audio")]
    [SerializeField] private AudioSource pointerEnterSFX;
    [SerializeField] private AudioSource pointerDownSFX;

    protected virtual void Start()
    {
        this.defaultScale = transform.localScale;
        this.targetScale = this.defaultScale;

        this.MappingComponent();
    }

    protected virtual void Update()
    {
        if (Mathf.Abs(transform.lossyScale.x - this.targetScale.x) > 0.01f)
        {
            float scaleValue =
                Mathf.Lerp(transform.localScale.x, this.targetScale.x, Time.deltaTime * this.scaleSpeed);

            transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
    }

    private void MappingComponent()
    {
        this.imageButton = GetComponent<Button>().image;
        this.textButton = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void ReturnDefaultLook()
    {
        this.targetScale = this.defaultScale;

        if (this.imageButton != null)
            this.imageButton.color = Color.white;
        if (this.textButton != null)
            this.textButton.color = Color.white;
    }

    public void AssignAudioSource()
    {
        this.pointerEnterSFX = GameObject.Find(GameObjectTags.UI_POINTER_ENTER).GetComponent<AudioSource>();
        this.pointerDownSFX = GameObject.Find(GameObjectTags.UI_POINTER_DOWN).GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.targetScale = this.defaultScale * this.scaleRate;

        if (this.pointerEnterSFX != null)
            this.pointerEnterSFX.Play();

        if (this.imageButton != null)
            this.imageButton.color = Color.yellow;

        if (this.textButton != null)
            this.textButton.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.ReturnDefaultLook();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.pointerDownSFX != null)
            this.pointerDownSFX.Play();

        this.ReturnDefaultLook();
    }
}
