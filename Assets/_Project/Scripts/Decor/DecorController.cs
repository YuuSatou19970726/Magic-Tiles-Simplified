using UnityEngine;

public class DecorController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.ScaleDecor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ScaleDecor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1, 1, 1);

        float height = spriteRenderer.sprite.bounds.size.y * 2f;
        float width = spriteRenderer.sprite.bounds.size.x;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 transformLocalScale = transform.localScale;
        transformLocalScale.y = worldScreenHeight / height;
        transformLocalScale.x = worldScreenWidth / width + 0.95f;

        transform.localScale = transformLocalScale;

        float topOffsetPercent = 0.35f;
        Vector3 screenPosition = new Vector3(
            Screen.width / 2f,
            Screen.height * (1f - topOffsetPercent),
            Camera.main.nearClipPlane + 1f
        );

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(transform.position.x, worldPosition.y, transform.position.z);
    }
}
