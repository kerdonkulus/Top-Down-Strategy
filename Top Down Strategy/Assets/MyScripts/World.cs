using UnityEngine;
using System.Collections;

public struct Point {
	public int x;
	public int y;

	public static Point zero = new Point (0, 0);

	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static Point operator +(Point A, Point B) {
		return new Point (A.x + B.x, A.y + B.y);
	}
	
	public static Point operator -(Point A, Point B) {
		return new Point (A.x - B.x, A.y - B.y);
	}

	public override string ToString ()
	{
		return "(" + x.ToString () + ", " + y.ToString () + ")";
	}
}

public class World : MonoBehaviour {

	public Material grassMaterial;
	public Material dirtMaterial;
	public Material rockMaterial;

	public Point mapSize = new Point(50,50);

	private Tile[,] map;

	public GameObject tile;

	// Use this for initialization
	void Awake () {
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GenerateMap() {
		map = new Tile[mapSize.x,mapSize.y];
		for(int x = 0; x<= mapSize.x-1; x++) {

			for(int y = 0; y<= mapSize.y-1; y++) {
				GameObject clone = Instantiate(tile, new Vector3(x,0,y),Quaternion.identity) as GameObject;
                map[x, y] = clone.GetComponent<Tile>();
				int temp = Random.Range(0,3);
				if (temp ==0) {
					map[x,y].SetMaterial(grassMaterial);
				}
				else if (temp == 1) {
					map[x,y].SetMaterial(dirtMaterial);
				}
				else {
					map[x,y].SetMaterial(rockMaterial);
				}
			}
		}
	}
}
