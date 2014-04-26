using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool IsReflected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.GetComponent<PlayerController>() != null)
		{
			Debug.Log("Dead");
			Application.LoadLevel(0);
		}
	}
}
