using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RayWeapon : MonoBehaviour {

    public float chargeTime = 1;
    public string fireButton = "Fire";

    public GameObject fireAnimation;

    CellManager cellManager;
    LevelManager levelManager;
    new Camera camera;

    Vector2 firePosition = new Vector2();
    bool charging = false;
    float chargeElapsed = 0;

	void Awake() {
        cellManager = CellManager.Instance;
        levelManager = LevelManager.Instance;
        camera = GetComponent<Camera>();
	}

    void Update() {
        if (!charging && Input.GetButtonDown(fireButton)) {
            firePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            charging = true;

            Instantiate(fireAnimation, firePosition, new Quaternion());
        }

        if (charging) {
            chargeElapsed += Time.deltaTime;
            if (chargeElapsed > chargeTime) {
                charging = false;
                chargeElapsed = 0;
                foreach (CellBehaviour cell in cellManager.GetCells()) {
                    Vector2 difference = (Vector2) cell.transform.position - firePosition;
                    if ((difference / cell.deathDistance).sqrMagnitude < 1) Destroy(cell.gameObject);
                }
                levelManager.RayFired();
            }
        }
    }
}
