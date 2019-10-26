using UnityEngine;

public class LevelManager : Manager<LevelManager> {

    public float width;
    public float height;

    public int seed;
    public int maxTries;
    public int healthyCount;
    public int cancerCount;

    public GameObject healthyPrefab;
    public GameObject cancerPrefab;

    public Collider2D cancerRegion;

    int cellMask;
    GameObject bounds;

	void Awake() {
        cellMask = LayerMask.GetMask("Cell");

        Random.InitState(seed);
        CreateBounds();
        PopulateLevel();
    }

    void CreateBounds() {
        bounds = new GameObject();
        bounds.AddComponent<EdgeCollider2D>().points = new Vector2[]{
            new Vector2(0, 0),
            new Vector2(width, 0),
            new Vector2(width, height),
            new Vector2(0, height),
            new Vector2(0, 0),
        };
    }

    void PopulateLevel() {
        float cancerRadius = cancerPrefab.GetComponent<CircleCollider2D>().radius;
        int cancerCount = 0;
        int cancerTries = 0;
        while (cancerCount < this.cancerCount && cancerTries < maxTries) {
            Vector2 position = new Vector2(
                Random.Range(0, width),
                Random.Range(0, height)
            );

            if (cancerRegion != null && cancerRegion.OverlapPoint(position)) {
                if (!Physics2D.OverlapCircle(position, cancerRadius, cellMask)) {
                    Instantiate(cancerPrefab, position, new Quaternion());
                    cancerCount++;
                    cancerTries = 0;
                    continue;
                }
            }

            cancerTries++;
        }

        float healthyRadius = healthyPrefab.GetComponent<CircleCollider2D>().radius;
        int healthyCount = 0;
        int healthyTries = 0;
        while (healthyCount < this.healthyCount && healthyTries < maxTries) {
            Vector2 position = new Vector2(
                Random.Range(0, width),
                Random.Range(0, height)
            );

            if (cancerRegion == null || !cancerRegion.OverlapPoint(position)) {
                if (!Physics2D.OverlapCircle(position, healthyRadius, cellMask)) {
                    Instantiate(healthyPrefab, position, new Quaternion());
                    healthyCount++;
                    healthyTries = 0;
                    continue;
                }
            }

            healthyTries++;
        }
    }
}
