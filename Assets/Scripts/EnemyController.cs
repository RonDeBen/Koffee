using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed;
    public GameObject looks;
	private Rigidbody2D rb;
	private Transform leftCheck, rightCheck;
	bool isMovingRight, rightWall, leftWall;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		leftCheck = transform.Find("leftCheck");
		rightCheck = transform.Find("rightCheck");

		rb.velocity = isMovingRight ? Vector3.right * speed : Vector3.left * speed;
        if (!isMovingRight)
            Flip();
	}
	
	// Update is called once per frame
	void Update () {
		rightWall = Physics2D.Linecast(gameObject.transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("wall"));
		leftWall = Physics2D.Linecast(gameObject.transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("wall"));

		if (rightWall && isMovingRight){
			isMovingRight = false;
			speed = -speed;
            Flip();
		}

		if (leftWall && !isMovingRight){
			isMovingRight = true;
			speed = -speed;
            Flip();
		}

		Vector3 startVel = rb.velocity;
		rb.velocity = new Vector3(speed, startVel.y, startVel.z);

	}

	public void SetDirection(bool isMovingRight){
		this.isMovingRight = isMovingRight;
		if (!isMovingRight){
			speed = -speed;
		}
	}

    private void Die()
    {
    	Score.AddPoints(50);
        GameObject.Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("sword"))
        {
            Die();
        }

        if (other.tag == "spring"){
    		Spring spr = other.GetComponent<Spring>();
    		if(isMovingRight == spr.GoingRight()){
    			rb.velocity = Vector2.zero;
    			rb.AddForce(Vector2.up * spr.GetForce());
    		}
    	}

    }

    void Flip()
    {
        Vector3 toFlip = looks.transform.localScale;
        toFlip.x *= -1;
        looks.transform.localScale = toFlip;
    }
}
