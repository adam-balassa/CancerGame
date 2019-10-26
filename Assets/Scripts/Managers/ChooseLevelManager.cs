using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChooseLevelManager : Manager<ChooseLevelManager>
{
    public Level OpenLevel { get; set; }
    private bool levelChosen = false;
    public bool LevelChosen { 
        get { return levelChosen; }
        set {
            levelChosen = value;
            OnChooseLevel.Invoke(OpenLevel);
        } 
    }
    public OnChooseLevelEvent OnChooseLevel;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public class OnChooseLevelEvent: UnityEvent<Level> {}

    public enum Level {
        BARIN, LYMPHOMA, LEUCHEMIA, BONE, RECTUM, NONE
    }
}
