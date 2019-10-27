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

    public static String getLevelScene(Level level) {
        switch(level) {
            case Level.BRAIN: return "RectalCancerScene";
            case Level.BONE: return "RectalCancerScene";
            case Level.RECTUM: return "RectalCancerScene";
            case Level.LEUCHEMIA:  return "RectalCancerScene";
            case Level.LYMPHOMA: return "RectalCancerScene";
            default: return "";
        }
    }

    public enum Level {
        BRAIN, LYMPHOMA, LEUCHEMIA, BONE, RECTUM, NONE
    }
}
