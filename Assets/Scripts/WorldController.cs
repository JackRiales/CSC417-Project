using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	// Map array of blocks defining the world
	private Block[][] _world;

	// Define general dimensions for the world in blocks. Actual block count will vary with size of blocks.
	public int worldWidth;
	public int worldHeight;

	// Block parent transform
	public Transform blockParent;

	// Used as the base for blocks
	public GameObject blockPrefab;

	// Used as the destruction particle
	public GameObject particlePrefab;

	// For right now the occlusion collider
	public Collider player;

	// Base block scale.
	private int blockSize;

	// (worldWidth * worldHeight) cache
	private int blockCount;

	/*
	===================================
	 */

	void Awake() {
		// Safety check sizes
		if (worldWidth < 1) {
			Debug.LogWarning("World width given less than 1. Setting to 1.");
			worldWidth = 1;
		}

		if (worldHeight < 1) {
			Debug.LogWarning("World height given less than 1. Setting to 1.");
			worldHeight = 1;
		}

		// Check if the blockPrefab is a block (so to speak) by checking if its scale factor is all equal.
		if (blockPrefab.transform.localScale.x != blockPrefab.transform.localScale.y) {
			Debug.LogWarning("Block prefab doesn't have equal scale factors. Might want to correct this.");
		}

		// Set up block size from scale
		blockSize = Mathf.FloorToInt((blockPrefab.transform.localScale.x + blockPrefab.transform.localScale.y) / 2);

		// Set up block count total
		blockCount = (worldWidth * worldHeight);

		// Debug
		Debug.Log ("World generated with " + blockCount + " blocks.");
	}

	void Start () {
		generateWorld ();
	}
	
	void Update () {
	
	}

	/*
	===================================
	 */

	private void generateWorld() {
		// Columns 
		for (int y = 0; y < worldHeight; y++) {

			// Rows
			for (int x = 0; x < worldWidth; x++) {

				// Generate a block
				GameObject newBlock = (GameObject) Instantiate (
					blockPrefab, 
					new Vector3(transform.position.x + x + blockSize, -(transform.position.y + y + blockSize), transform.position.z), 
					Quaternion.identity);

				// Set name
				newBlock.name = "Block " + x + ", " + y;

				// Set parent
				if (blockParent)
					newBlock.transform.parent = blockParent;
				else
					newBlock.transform.parent = this.transform;

				// Add block component (change later for specifics)
				Block blockComp = newBlock.AddComponent<Block>();
				if (particlePrefab)
					blockComp.destructionBlocks = particlePrefab;
				blockComp._xindex = x;
				blockComp._yindex = y;
			}

		}
	}

}
