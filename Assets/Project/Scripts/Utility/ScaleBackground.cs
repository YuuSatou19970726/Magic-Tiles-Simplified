using UnityEngine;

public class ScaleBackground : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1, 1, 1);

        float height = spriteRenderer.sprite.bounds.size.y;
        float width = spriteRenderer.sprite.bounds.size.x;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 transformLocalScale = transform.localScale;
        transformLocalScale.y = worldScreenHeight / height + 0.1f;
        transformLocalScale.x = worldScreenWidth / width + 0.1f;

        transform.localScale = transformLocalScale;
    }
}
