using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Point coords = Point.zero;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMaterial(Material mat) {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }

        rend.material = mat;
    }
}
