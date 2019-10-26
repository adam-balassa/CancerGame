using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IllnessBehaviour : MonoBehaviour, IPointerClickHandler
{
    CanvasRenderer canvas;
    public GameObject bubble;
    const float minScale = 0.05f;
    const float maxScale = 1.0f;
    const float deltaScale = 1.4f;
    float currentScale = 1.0f;

    void Awake() {
        canvas = GetComponent<CanvasRenderer>();
        ChooseLevelManager manager = ChooseLevelManager.Instance;
        manager.OnChooseLevel.AddListener((ChooseLevelManager.Level _) => {
            Destroy(gameObject);
        });
    }
    void Start()
    {
        canvas.SetAlpha(0.0f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(bubble);
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(currentScale, currentScale, 0);
        canvas.SetAlpha(1 - currentScale * currentScale);
    }

    void FixedUpdate()
    {
        float scale = currentScale + Time.fixedDeltaTime * deltaScale;
        if (scale > maxScale) currentScale = minScale; 
        else currentScale = scale;
    }
}
