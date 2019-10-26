using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager<T> : MonoBehaviour where T : MonoBehaviour {

	// The currently active instance of the manager
	static T currentManager;

	// The scene of the current manager
	static Scene currentScene;

	// An instance of the manager in the active scene
	public static T Instance {
		get {
			// Get the active svene
			Scene activeScene = SceneManager.GetActiveScene();

			// If the current manager's scene isn't the active scene
			// replace it with the active scene and find the currently active manager
			if (currentScene != activeScene) {
				currentScene = activeScene; 
				currentManager = FindObjectOfType<T>();
			}

			// Return the currently active manager
			return currentManager;
		}
	}
}