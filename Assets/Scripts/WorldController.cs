﻿using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	// Define general dimensions for the world in blocks. Actual block count will vary with size of blocks.
	public int worldWidth;
	public int worldHeight;

	// Used as the base for blocks
	public Block blockPrefab;

	// Base block scale.
	private int blockSize;

	// ((worldWidth * worldHeight) / blockSize) cache
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
		blockCount = Mathf.FloorToInt((worldWidth * worldHeight) / blockSize);
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
	
	}
}