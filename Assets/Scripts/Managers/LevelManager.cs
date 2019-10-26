using UnityEngine;

public class LevelManager : Manager<LevelManager> {

    public float width;
    public float height;

    public int seed;
    public int maxTries;

    public GameObject[] cellPrefabs;
    public Collider2D[] regions;

    int cellMask;

	void Awake() {
        cellMask = LayerMask.GetMask("Cell");
        Random.InitState(seed);
        
        PopulateLevel();
    }

    void PopulateLevel() {
        foreach (GameObject cellPrefab in cellPrefabs) {
            CellBehaviour cell = cellPrefab.GetComponent<CellBehaviour>();
            Collider2D cellRegion = cell.type < regions.Length ? regions[cell.type] : null;
            float size = cell.size;

            int cellCount = 0;
            int tries = 0;
            while (tries < maxTries) {

                Vector2 position = new Vector2(
                    Random.Range(0, width),
                    Random.Range(0, height)
                );

                if (cellRegion == null || cellRegion.OverlapPoint(position)) {
                    if (!Physics2D.OverlapCircle(position, size, cellMask)) {
                        Instantiate(cellPrefab, position, new Quaternion());
                        cellCount++;
                        tries = 0;
                        continue;
                    }
                }

                tries++;
            }

            Debug.Log(cellCount);
        }
    }
}
