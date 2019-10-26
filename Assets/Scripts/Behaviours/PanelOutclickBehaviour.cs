using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelOutclickBehaviour : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChooseLevelManager.Instance.LevelSelected = ChooseLevelManager.Level.NONE;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
