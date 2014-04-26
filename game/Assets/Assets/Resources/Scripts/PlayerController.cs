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
			Application.LoadLevel(0);
		}

		if(!IsReflected && this.transform.position.y < -30)
		{
			print ("Regular char fell");
			Application.LoadLevel(0);
		}

		if(this.transform.position.x < -15)
		{
			print ("Fell behind");
			Application.LoadLevel(0);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.GetComponent<PlayerController>() != null)
		{
			print ("Hit other player");
			Application.LoadLevel(0);
		}
	}
}
