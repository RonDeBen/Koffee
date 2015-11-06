using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;
	private Transform leftCheck, rightCheck;
	bool isMovingRight, rightWall, leftWall;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		leftCheck = transform.Find("leftCheck");
		rightCheck = transform.Find("rightCheck");

		rb.velocity = isMovingRight ? Vector3.right * speed : Vector3.left * speed;
	}
	
	// Update is called once per frame
	void Update () {
		rightWall = Physics2D.Linecast(gameObject.transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("wall"));
		leftWall = Physics2D.Linecast(gameObject.transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("wall"));

		if (rightWall && isMovingRight){
			isMovingRight = false;
			rb.velocity = Vector3.left * speed;
		}

		if (leftWall && !isMovingRight){
			isMovingRight = true;
			rb.velocity = Vector3.right * speed;
		}

	}

	public void SetDirection(bool isMovingRight){
		this.isMovingRight = isMovingRight;
	}
}
