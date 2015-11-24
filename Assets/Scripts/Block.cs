using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	// World array coordinates
	[HideInInspector]
	public int _xindex = 0, _yindex = 0;

	// How many hits will the block need 
	public int hitsToDestroy = 5;
	public int spawnLayer = 0;
	public int treasureCount = 0;
	public Material material;
	public GameObject destructionBlocks;

	static int treasureMax = 10;
	static int destructionBlockCount = 6;
	static int destructionBlockTTL = 3;

	/*
	===================================
	 */

	void Awake() {
		// These must always be on the "block" layer
		gameObject.layer = 9;
	}

	void Start() {
		// If no material is set, use the standard diffuse
		if (!material)
			material = new Material (Shader.Find ("Standard"));

		if (material)
			GetComponent<Renderer> ().material = material;
	}

	void Update() {
		// Destroy when hp is 0 or lower
		if (hitsToDestroy <= 0) {
			Destroy (gameObject);
		}
	}

	void OnDestroy() {
		// Push to log
		// Debug.Log("Block located at " + _xindex + ", " + _yindex + " was destroyed.");

		// Create the fun destructive blocks, only if they exist in the inspector AND have a rigidbody
		if (destructionBlocks && destructionBlocks.GetComponent<Rigidbody> ()) {
			for(int i = 0; i < Block.destructionBlockCount; i++) {
				// Create the block
				GameObject db = (GameObject) Instantiate(destructionBlocks, this.transform.position, Quaternion.identity);

				/* do some physics */

				// Flag for delete in [TTL] seconds
				Destroy (db, Block.destructionBlockTTL);
			}
		}
	}
}
