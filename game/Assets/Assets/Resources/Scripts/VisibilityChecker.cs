using UnityEngine;
using System.Collections;

public class VisibilityChecker : MonoBehaviour {

	Director _director;

	public float ChunkWidth;
	public bool TriggersSpawn;
	float _movedDistance = 0f;
	bool _spawned;

	// Use this for initialization
	void Start () {
		_director = GameObject.Find("Director").GetComponent<Director>();

	}
	
	// Update is called once per frame
	void Update () {
		if(TriggersSpawn && !_spawned && _movedDistance >= ChunkWidth) {
			_director.SpawnSegment();
			_spawned = true;
		}

		if(transform.position.x <= 0 - ChunkWidth - 20) {
			Destroy(this.gameObject);
		}
	}

	public void Move(float moveDistance) {
		_movedDistance += moveDistance;


	}
}
