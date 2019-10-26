using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class CellBehaviour : MonoBehaviour {

    public int type = 0;
    public Relation[] relations = new Relation[0];
    public float multiplicationRate = 1;
    public float multiplicationDistance = 0.2f;
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

		cellManager.AddCell(this);
	}

	void OnDestroy() {
		cellManager.RemoveCell(this);
	}

    void FixedUpdate() {
        UpdateRelations();
        Multiply();
    }

    void UpdateRelations() {
        float checkRadius = collider.radius;
        foreach (Relation relation in relations) {
            if (relation.repulsionDistance + collider.radius > checkRadius) checkRadius = relation.repulsionDistance + collider.radius;
            if (relation.cohesionDistance + collider.radius > checkRadius) checkRadius = relation.cohesionDistance + collider.radius;
        }

		foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, checkRadius, mask)) {
			if (collider == this.collider) continue;

            CellBehaviour cell = collider.GetComponent<CellBehaviour>();

			Vector2 direction = cell.transform.position - transform.position;
			float distance = direction.magnitude;
			direction /= distance;
            distance -= this.collider.radius + cell.collider.radius;

            if (cell.type < relations.Length) {
                Relation relation = relations[cell.type];
                if (distance < relation.repulsionDistance) {
                    float repulsion = (relation.repulsionDistance - distance) / relation.repulsionDistance;
                    rigidbody.AddForce(-direction * repulsion * relation.repulsionStrength);
                } else if (distance < relation.cohesionDistance) {
                    float cohesion = (distance - relation.repulsionDistance) / (relation.cohesionDistance - relation.repulsionDistance);
                    rigidbody.AddForce(direction * cohesion * relation.cohesionStrength);
                }
            }
		}
    }

    void Multiply() {
        float remaining = multiplicationRate;
        while (remaining > 0 && (remaining >= 1 || remaining > Random.Range(0f, 1f))) {
            float directionAngle = Random.Range(0f, 2 * Mathf.PI);
            Vector2 position = (Vector2) transform.position + new Vector2(
                Mathf.Cos(directionAngle) * (collider.radius + collider.radius),
                Mathf.Sin(directionAngle) * (collider.radius + collider.radius)
            );

            if (position.x - collider.radius < 0) continue;
            if (position.x + collider.radius > levelManager.width) continue;
            if (position.y - collider.radius < 0) continue;
            if (position.y + collider.radius > levelManager.height) continue;

            if (!Physics2D.OverlapCircle(position, collider.radius, mask)) {
                Instantiate(gameObject, (Vector2) transform.position + new Vector2(
                    Mathf.Cos(directionAngle) * (collider.radius * (1 + multiplicationDistance)),
                    Mathf.Sin(directionAngle) * (collider.radius * (1 + multiplicationDistance))
                ), new Quaternion());
                break;
            }

            remaining -= 1;
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
