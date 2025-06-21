using UnityEngine;
using UnityEngine.EventSystems;

public class TileController : MonoBehaviour
{
    [SerializeField] private TileType tileType;

    public float fallSpeed = 5f;

    private Vector3 currentPosition;
    public bool isActive = false;

    public TileVisual visual { get; private set; } // read-only

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.visual = GetComponent<TileVisual>();
        this.ScaleTile();
        this.currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActive)
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y <= -7.5f)
        {
            this.ReturnTile();
            GameManager.Instance.GameOver();
        }
    }

    public void ReturnTile()
    {
        this.isActive = false;
        transform.position = this.currentPosition;
        ObjectPool.Instance.ReturnObject(transform.gameObject);
    }

    [ContextMenu("Scale Tile")]
    private void ScaleTile()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1, 1, 1);

        float height = spriteRenderer.sprite.bounds.size.y;
        float width = spriteRenderer.sprite.bounds.size.x;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 transformLocalScale = transform.localScale;
        transformLocalScale.y = worldScreenHeight / (height * 6f);
        transformLocalScale.x = worldScreenWidth / (width * 4f) - 0.1f;

        transform.localScale = transformLocalScale;
    }
}
