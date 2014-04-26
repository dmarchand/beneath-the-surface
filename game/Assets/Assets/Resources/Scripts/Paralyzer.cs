using UnityEngine;
using System.Collections;

public class Paralyzer : MonoBehaviour {

	public float Duration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {

		PlatformerCharacter2D player = coll.GetComponent<PlatformerCharacter2D>();

		if(player != null) {
			player.Paralyze(Duration);
			Destroy(this.gameObject);
		}
	}
}
