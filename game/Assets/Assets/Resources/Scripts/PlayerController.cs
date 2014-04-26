using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool IsReflected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(IsReflected && this.transform.position.y > 30)
		{
			print ("Reflected char fell");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
			Application.LoadLevel(2);
		}

		if(!IsReflected && this.transform.position.y < -30)
		{
			print ("Regular char fell");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
			Application.LoadLevel(2);
		}

		if(this.transform.position.x < -15)
		{
			print ("Fell behind");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
			Application.LoadLevel(2);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.GetComponent<PlayerController>() != null)
		{
			print ("Hit other player");
            Director.GameStats.DeathText = "Your drones collided, violating the dimensional anomaly's physical laws\n and caused it to collapse!";
			Application.LoadLevel(2);
		}
	}
}
