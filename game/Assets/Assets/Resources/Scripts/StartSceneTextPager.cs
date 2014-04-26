using UnityEngine;
using System.Collections;

public class StartSceneTextPager : MonoBehaviour {
    
    public int DisplayOnIndex;

    StartSceneController _sceneController;

	// Use this for initialization
	void Start () {
        _sceneController = GameObject.Find("Main Camera").GetComponent<StartSceneController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_sceneController.SceneIndex == DisplayOnIndex)
        {
            this.guiText.enabled = true;
        }
        else
        {
            this.guiText.enabled = false;
        }
	}
}
