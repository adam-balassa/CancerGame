using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChooseLevelManager : Manager<ChooseLevelManager>
{    private bool levelChosen = false;
    public bool LevelChosen { 
        get { return levelChosen; }
        set {
            levelChosen = value;
            OnChooseLevel.Invoke(levelSelected);
        } 
    }

    private Level levelSelected = Level.NONE;
    public Level LevelSelected { 
        get { return levelSelected; }
        set {
            levelSelected = value;
            OnSelectLevel.Invoke(levelSelected);
        } 
    }
    public OnChooseLevelEvent OnChooseLevel;
    public OnSelectLevelEvent OnSelectLevel;

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
    [Serializable]
    public class OnSelectLevelEvent: UnityEvent<Level> {}

    public enum Level {
        BARIN, LYMPHOMA, LEUCHEMIA, BONE, RECTUM, NONE
    }
}
