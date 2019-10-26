using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class CellBehaviour : MonoBehaviour {

    public float size;
    public int type = 0;
    public Relation[] relations = new Relation[0];
    public float regionStrength = 5;
    public float multiplicationRate = 1;
    public float deathDistance = 4;

	CellManager cellManager;
    LevelManager levelManager;
    new CircleCollider2D collider;
    new Rigidbody2D rigidbody;
    int mask;

	void Awake() {
		cellManager = CellManager.Instance;
        levelManager = LevelManager.Instance;
        collider = GetComponent<CircleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Cell");

        UpdateComponents();

		cellManager.AddCell(this);
	}

	void OnDestroy() {
		cellManager.RemoveCell(this);
	}

    void FixedUpdate() {
        UpdateComponents();
        UpdateRelations();
        Multiply();
    }

    void UpdateComponents() {
        collider.radius = size;
    }

    void UpdateRelations() {
        float checkRadius = size;
        foreach (Relation relation in relations) {
            float repulsionDistance = relation.repulsionDistance;
            float cohesionDistance = relation.cohesionDistance;
            if (repulsionDistance + size > checkRadius) checkRadius = repulsionDistance + size;
            if (cohesionDistance + size > checkRadius) checkRadius = cohesionDistance + size;
        }

		foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, checkRadius, mask)) {
			if (collider == this.collider) continue;
            CellBehaviour cell = collider.GetComponent<CellBehaviour>();

			Vector2 direction = cell.transform.position - transform.position;
			float distance = direction.magnitude;

            if (distance > 0) {
                direction /= distance;
                distance -= cell.size + size;

                if (cell.type < relations.Length) {
                    Relation relation = relations[cell.type];
                    float repulsionDistance = relation.repulsionDistance;
                    float repulsionStrength = relation.repulsionStrength;
                    float cohesionDistance = relation.cohesionDistance;
                    float cohesionStrength = relation.cohesionStrength;

                    if (distance < repulsionDistance) {
                        float repulsion = (repulsionDistance - distance) / repulsionDistance;
                        rigidbody.AddForce(-direction * repulsion * repulsionStrength);
                    } else if (distance < cohesionDistance) {
                        float cohesion = (distance - repulsionDistance) / (cohesionDistance - repulsionDistance);
                        rigidbody.AddForce(direction * cohesion * cohesionStrength);
                    }
                }
            }
		}

        if (type < levelManager.regions.Length) {
            Collider2D region = levelManager.regions[type];

            bool inside = region.OverlapPoint(transform.position);
            Vector2 direction = region.ClosestPoint(transform.position) - (Vector2) transform.position;
            float distance = direction.magnitude;

            if (distance > 0) {
                direction /= distance;

                if (!inside) {
                    rigidbody.AddForce(direction * regionStrength);
                } else {
                    rigidbody.AddForce(-direction * Mathf.Min(regionStrength, Mathf.Pow(1 / distance, 2)));
                }
            }
        }
    }

    void Multiply() {
        if (multiplicationRate < Random.Range(0f, 1f)) return;
        if (Physics2D.OverlapCircleAll(transform.position, size * 0.9f, mask).Length > 1) return;
        
        Collider2D region = type < levelManager.regions.Length ? levelManager.regions[type] : null;
        if (region != null && !region.OverlapPoint(transform.position)) return;

        float surrounding = 0;
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, size * 2.3f, mask)) {
            surrounding += Mathf.Pow(
                Mathf.Min(Mathf.Min(collider.bounds.size.x, collider.bounds.size.y) / size / 2, 1), 2
            );
        }
        if (surrounding > 5f) return;
        
        int samples = 12;
        int inside = 0;

        float angleStart = Random.Range(0, Mathf.PI);
        float angleEnd = 2 * Mathf.PI + angleStart;
        float deltaAngle = 2 * Mathf.PI / samples;

        for (float directionAngle = angleStart; directionAngle < angleEnd; directionAngle += deltaAngle) {
            Vector2 direction = new Vector2(Mathf.Cos(directionAngle), Mathf.Sin(directionAngle));
            Vector2 position = (Vector2) transform.position + direction * (size + size);
            if (region != null && region.OverlapPoint(position)) {
                inside++;
            }
        }

        if (surrounding < 5f * inside / samples) {
            Instantiate(gameObject, (Vector2) transform.position, new Quaternion()).name = name;
        }
    }

    [System.Serializable]
    public class Relation {

        public float repulsionDistance;
        public float repulsionStrength;
        public float cohesionDistance;
        public float cohesionStrength;
    }
}
