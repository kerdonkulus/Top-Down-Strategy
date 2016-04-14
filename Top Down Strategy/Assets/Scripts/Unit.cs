using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public Tile currentTile;

	public string unitName = "Unit";
	public int hp = 10;
	public int moveStat = 3;

    private Vector3 lastPosition;
    private float interpolationParam;
    internal static Point to;

    private List<Point> navPoints = new List<Point>();


    // Use this for initialization
    public virtual void Start () {
		currentTile = World.Instance ().GetTileFromPosition (transform.position);
		World.Instance ().WarpUnit (this, GetCoords ());
        lastPosition = transform.position;
	}

	public Point GetCoords() {
		if(currentTile == null)
			return new Point(-1, -1);

		return currentTile.coords;
	}

    public void NavigateTo(Point coords)
    {
        navPoints = GetComponent<Navigator>().ComputePath(GetCoords(), coords);
    }

    void OnMouseUpAsButton(){
        if (Input.GetMouseButtonUp(0))
            World.Instance().Select(GetCoords());
        if (Input.GetMouseButtonUp(1))
            World.Instance().moveTo(GetCoords());
    }

    public void BeginInterpolatedMove() {
        

        lastPosition = transform.position;
        interpolationParam = 0.0f;
    }

    public bool IsMoving() {
        return navPoints.Count > 0;
    }

    public float NLerp(float from, float to, float t){
        //4 * (x - .5) ^ 3 + .5
        //t = 0.36f * Mathf.Atan(10f * (t-.05f)) + 0.5f;
        return from + (to - from) * t;  //both this code and line below are the same
        //return from * (1.0f - t) + to * t;
    }

    public Vector3 VectorInterpolate(Vector3 from, Vector3 to, float t){
        return new Vector3(NLerp(from.x, to.x, t), NLerp(from.y, to.y, t), NLerp(from.z, to.z, t));
    }

    // Update is called once per frame
    void Update () { 
        if (interpolationParam <= 1.0f && navPoints.Count > 0)
        {
            Point nextPoint = navPoints[0];
            //Use that element as destination
            Vector3 destination = World.Instance().GetPositionFromCoords(nextPoint);
            transform.position = VectorInterpolate(lastPosition, destination, interpolationParam);
            interpolationParam += Time.deltaTime * 2;

            //if interpolation parameter is over 1 its time to go to the next navPoint element
            if(interpolationParam >= 1.0f) {
                lastPosition = World.Instance().GetPositionFromCoords(nextPoint);
                navPoints.RemoveAt(0);
                interpolationParam = 0.0f;
            }
        }
	}

	public virtual void ActivateSpecial() {

	}
}
