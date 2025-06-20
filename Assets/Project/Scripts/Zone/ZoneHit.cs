using System.Collections;
using UnityEngine;

public class ZoneHit : MonoBehaviour
{
    [SerializeField] GameObject hitZone;

    private void Start()
    {
        SpriteRenderer spriteRenderer = hitZone.GetComponent<SpriteRenderer>();
        hitZone.transform.localScale = new Vector3(1, 1, 1);

        float height = spriteRenderer.sprite.bounds.size.y;
        float width = spriteRenderer.sprite.bounds.size.x;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 transformLocalScale = transform.localScale;
        transformLocalScale.y = worldScreenHeight / height * 0.007f;
        transformLocalScale.x = worldScreenWidth / width + 0.1f;

        hitZone.transform.localScale = transformLocalScale;

        float y = worldScreenHeight / 2f;
        hitZone.transform.position = new Vector3(0, -y + 2f, 0);
    }
}
