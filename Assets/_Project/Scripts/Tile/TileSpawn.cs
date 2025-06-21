using System.Linq;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    public GameObject tilePrefab;

    private bool isReadySpawm = false;

    [SerializeField] private Transform[] spawmPoints;

    void OnEnable()
    {
        BeatController.OnBeat += this.SpawnTile;
    }

    void OnDisable()
    {
        BeatController.OnBeat -= this.SpawnTile;
    }

    void Start()
    {
        this.SetupPositionSpawmPoints();
    }

    private void SpawnTile()
    {
        if (!this.isReadySpawm) return;

        int randomIndex = Random.Range(0, spawmPoints.Count());

        ObjectPool.Instance.GetObject(this.tilePrefab, this.spawmPoints[randomIndex]);
    }

    private void SetupPositionSpawmPoints()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float spacing = worldScreenWidth / this.spawmPoints.Count();

        for (int i = 0; i < this.spawmPoints.Count(); i++)
        {
            float x = -worldScreenWidth / 2f + spacing / 2f + i * spacing;
            float y = worldScreenHeight / 2f;

            this.spawmPoints[i].position = new Vector3(x, y + 2f, 0);
        }

        this.isReadySpawm = true;
    }
}
