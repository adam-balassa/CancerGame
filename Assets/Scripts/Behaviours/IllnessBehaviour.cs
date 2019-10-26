using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IllnessBehaviour : MonoBehaviour, IPointerClickHandler
{
    public GameObject startPanel;
    CanvasRenderer canvas;
    const float minScale = 0.05f;
    const float maxScale = 1.0f;
    const float deltaScale = 1.4f;
    float currentScale = 1.0f;
    bool isVisible = true;

    
    public ChooseLevelManager.Level level;
    ChooseLevelManager manager;
    void Awake() {
        manager = ChooseLevelManager.Instance;
        canvas = GetComponent<CanvasRenderer>();
        manager.OnChooseLevel.AddListener((ChooseLevelManager.Level _) => {
            Destroy(gameObject);
        });
        manager.OnSelectLevel.AddListener((ChooseLevelManager.Level level) => {
            isVisible = level == this.level || level == ChooseLevelManager.Level.NONE;
        });
    }
    void Start()
    {
        canvas.SetAlpha(0.0f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(startPanel, transform.parent.parent);
        manager.LevelSelected = level;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isVisible) {
            canvas.SetAlpha(0);
            return;
        }
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
