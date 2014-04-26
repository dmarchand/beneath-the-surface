using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

    public string[] Tips;

	// Use this for initialization
	void Start () {

        bool newHighScore = false;
        if (Director.GameStats.Score > Director.GameStats.HighScore)
        {
            Director.GameStats.HighScore = Director.GameStats.Score;
            newHighScore = true;
        }

        GameObject.Find("ScoreText").guiText.text = "Score: " + Director.GameStats.Score;
        GameObject.Find("HighScoreText").guiText.text = "High Score: " + Director.GameStats.Score;

        if (newHighScore)
        {
            GameObject.Find("HighScoreText").guiText.text += "  (New High Score!)";
        }

        GameObject.Find("DeathReasonText").guiText.text = Director.GameStats.DeathText;

        int tipIndex = Random.Range(0, Tips.Length);
        GameObject.Find("TipText").guiText.text = "Tip: " + Tips[tipIndex];

	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.anyKeyDown)
        {
            Application.LoadLevel(1);
        }
	}
}
