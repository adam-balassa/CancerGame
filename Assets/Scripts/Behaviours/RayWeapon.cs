using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RayWeapon : MonoBehaviour {

    public float radius = 0;
    public string fireButton = "Fire";

    CellManager cellManager;
    new Camera camera;

	void Awake() {
        cellManager = CellManager.Instance;
        camera = GetComponent<Camera>();
	}

    void Update() {
        if (Input.GetButton(fireButton)) {
            Vector2 firePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            foreach (CellBehaviour cell in cellManager.GetCells()) {
                Vector2 difference = (Vector2) cell.transform.position - firePosition;
                if ((difference / cell.deathDistance).sqrMagnitude < 1) Destroy(cell.gameObject);
            }
        }
    }
}
