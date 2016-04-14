using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Navigator : MonoBehaviour {

	public static int Abs(int a) {
		return (a < 0? -a : a);
	}

	public static int Sign(int a) {
		return (a < 0 ? -1 : 1);
	}

	public List<Point> ComputePath(Point from, Point to) {
		List<Point> result = new List<Point>();
        Point current = from;
        
        while (current.x != to.x || current.y != to.y)
        {
            Point seperation = to - current;
            if (Abs(seperation.x) > Abs(seperation.y))
                current.x += Sign(seperation.x);
            else
                current.y += Sign(seperation.y);
            result.Add(current);
        }



        return result;
	}
}
