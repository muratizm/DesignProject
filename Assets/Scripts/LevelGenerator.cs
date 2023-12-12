using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour {

	public Tilemap tilemap;
	public bool[,] tileArray;
	public static int Level = 0;
	public GameObject floorPrefab;
	public GameObject wallPrefab;
	public GameObject ceilingPrefab;

	public GameObject characterController;

	public GameObject floorParent;
	public GameObject wallsParent;

	public int holeCounter = 0;

	// allows us to see the maze generation from the scene view
	public bool generateRoof = true;

	// number of times we want to "dig" in our maze
	public int tilesToRemove = 50;

	public int mazeSize;

	// spawns at the end of the maze generation
	public GameObject pickup;

	// this will determine whether we've placed the character controller
	private bool characterPlaced = false;

	// 2D array representing the map
	private bool[,] mapData;

	// we use these to dig through our maze and to spawn the pickup at the end
	private int mazeX = 4, mazeY = 1;

	// Use this for initialization
	void Start () {
		ConvertTilemapToArray();
		
		// initialize map 2D array
		mapData = GenerateMazeData();
		// create actual maze blocks from maze boolean data
		for (int z = 0; z < mazeSize; z++) {
			for (int x = 0; x < mazeSize; x++) {
				if (mapData[z, x]) {
					CreateChildPrefab(wallPrefab, wallsParent, x, 1, z);
					CreateChildPrefab(wallPrefab, wallsParent, x, 2, z);
					CreateChildPrefab(wallPrefab, wallsParent, x, 3, z);
				} else if (!characterPlaced) {
					
					// place the character controller on the first empty wall we generate
					characterController.transform.SetPositionAndRotation(
						new Vector3(x, 1, z), Quaternion.identity
					);

					// flag as placed so we never consider placing again
					characterPlaced = true;
				}

				
				if (Random.Range(1, 31) == 1 && holeCounter < 12) // max(holeCounter) is 12 because most of the holes are at under wall level, the player does not come across them
				{ // dont create floor
					holeCounter += 1;
				}
				else // create floor 
				{
					CreateChildPrefab(floorPrefab, floorParent, x, 0, z);
				}

				/*
				if (generateRoof) { // create ceiling
					CreateChildPrefab(ceilingPrefab, wallsParent, x, 4, z);
				}
				*/
			}
		}


		Debug.Log(holeCounter);

		// spawn the pickup at the end
		var myPickup = Instantiate(pickup, new Vector3(mazeX, 1, mazeY), Quaternion.identity);
		myPickup.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
	}

	    void ConvertTilemapToArray()
    {
        BoundsInt bounds = tilemap.cellBounds;
        tileArray = new bool[bounds.size.x, bounds.size.y];

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(bounds.x + x, bounds.y + y, 0);
                TileBase tile = tilemap.GetTile(cellPosition);

                // If a tile is present, set the corresponding array element to true, otherwise false.
                tileArray[x, y] = (tile != null);
            }
        }

        // Output the 2D array (for testing purposes)
        PrintArray(tileArray);
    }

    void PrintArray(bool[,] array)
    {
        string output = "";

        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                output += array[x, y] + " ";
            }

            output += "\n";
        }

        Debug.Log(output);
    }

	// generates the booleans determining the maze, which will be used to construct the cubes
	// actually making up the maze
	bool[,] GenerateMazeData() {
		bool[,] data = new bool[mazeSize, mazeSize];

		// initialize all walls to true
		for (int y = 0; y < mazeSize; y++) {
			for (int x = 0; x < mazeSize; x++) {
				data[y, x] = true;
			}
		}

		// counter to ensure we consume a minimum number of tiles
		int tilesConsumed = 0;

		// iterate our random crawler, clearing out walls and straying from edges
		while (tilesConsumed < tilesToRemove) {
			
			// directions we will be moving along each axis; one must always be 0
			// to avoid diagonal lines
			int xDirection = 0, yDirection = 0;

			if (Random.value < 0.5) {
				xDirection = Random.value < 0.5 ? 1 : -1;
			} else {
				yDirection = Random.value < 0.5 ? 1 : -1;
			}

			// random number of spaces to move in this line
			int numSpacesMove = (int)(Random.Range(1, mazeSize - 1));

			// move the number of spaces we just calculated, clearing tiles along the way
			for (int i = 0; i < numSpacesMove; i++) {
				mazeX = Mathf.Clamp(mazeX + xDirection, 1, mazeSize - 2);
				mazeY = Mathf.Clamp(mazeY + yDirection, 1, mazeSize - 2);

				if (data[mazeY, mazeX]) {
					data[mazeY, mazeX] = false;
					tilesConsumed++;
				}
			}
		}

		return data;
	}

	// allow us to instantiate something and immediately make it the child of this game object's
	// transform, so we can containerize everything. also allows us to avoid writing Quaternion.
	// identity all over the place, since we never spawn anything with rotation
	void CreateChildPrefab(GameObject prefab, GameObject parent, int x, int y, int z) {
		var myPrefab = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
		myPrefab.transform.parent = parent.transform;
	}
}
