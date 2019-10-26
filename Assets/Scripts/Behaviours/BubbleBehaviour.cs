using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleBehaviour : MonoBehaviour, IPointerClickHandler
{
    public ChooseLevelManager.Level level;
    ChooseLevelManager manager;
    void Awake() {
        manager = ChooseLevelManager.Instance;
        manager.OpenLevel = level;
        manager.OnChooseLevel.AddListener((ChooseLevelManager.Level _) => {
            Destroy(gameObject);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
        manager.OpenLevel = ChooseLevelManager.Level.NONE;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
