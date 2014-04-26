using UnityEngine;
using System.Collections;

public class StartSceneController : MonoBehaviour {

    public int SceneIndex;
    public int GameStartScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneIndex++;
        }

        if (SceneIndex >= GameStartScene)
        {
            Application.LoadLevel(1);
        }
	}
}
