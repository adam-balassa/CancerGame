using UnityEngine; 
using UnityEngine.UI;

public class PlayButtonBehaviour : MonoBehaviour
{
    Button button;
    void Awake() {
        button = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => {
            ChooseLevelManager manager = ChooseLevelManager.Instance;
            manager.LevelChosen = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
