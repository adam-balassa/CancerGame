using UnityEngine;
using UnityEngine.Events;
using System;

public class LevelManager : Manager<LevelManager> {

    public float width;
    public float height;
    public int seed;
    public int maxTries;

    public GameObject[] cellPrefabs;
    public Collider2D[] regions;

    public int cancerousCellType = 0;

    public short pointsFor5 = 8;
    public short pointsFor4 = 12;
    public short pointsFor3 = 20;
    public short pointsFor2 = 25;
    public GameObject starsPrefab;

    int cellMask;
    int numberOfCancerousCells = 0;
    short firedRays = 0;
    bool gameWon = false;

	void Awake() {
        cellMask = LayerMask.GetMask("Cell");
        UnityEngine.Random.InitState(seed);
        CellManager manager = CellManager.Instance;

        manager.OnCellAddedEvent.AddListener((CellBehaviour cell) => {
            if (cell.type == cancerousCellType) numberOfCancerousCells++;
        });

        manager.OnCellRemovedEvent.AddListener((CellBehaviour cell) => {
            if (cell.type == cancerousCellType) numberOfCancerousCells--;

            if (numberOfCancerousCells == 0 && !gameWon) {
                gameWon = true;
                Debug.Log(firedRays);
                StarManager.Instance.CalculateResult(new short[]{
                    pointsFor5, pointsFor4, pointsFor3, pointsFor2
                }, firedRays);
                Instantiate(starsPrefab);
            }
        });

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
                    UnityEngine.Random.Range(0, width),
                    UnityEngine.Random.Range(0, height)
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

    public void RayFired() {
        firedRays++;
        Debug.Log("Rays fired");
    }
}
