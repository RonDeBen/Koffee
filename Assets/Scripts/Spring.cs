using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {

	public enum Direction {Left, Right};

	public float force = 5000f;
	public Direction direction;
	

	public float GetForce(){
		return force;
	}

	public bool GoingRight(){
		return (direction == Direction.Right);
	}
}
