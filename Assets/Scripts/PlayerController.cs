using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpForce;

	public GameObject myBean;

    public float attackDuration;
    public Transform t_RightSword;
    public Transform t_LeftSword;
    public GameObject p_RightSword;
    public GameObject p_LeftSword;

    public Transform feet;


	private bool grounded = false;

	private Rigidbody2D rb;
	private Transform groundCheck;
    private bool m_FacingRight = true;
    private GameObject sword;

    void Start(){
		rb = GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("groundCheck");
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(gameObject.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("floor")); 

		bool left = Input.GetKey("left");
		bool right = Input.GetKey("right");
		bool jump = Input.GetKeyDown(KeyCode.Z);
		bool attack = Input.GetKeyDown(KeyCode.X);


		if (left){
			rb.velocity = new Vector3(-speed, rb.velocity.y, 0f);
            if (m_FacingRight)
                Flip();
		}

		if (right){
			rb.velocity = new Vector3(speed, rb.velocity.y, 0f);
            if (!m_FacingRight)
                Flip();
        }

		if (!(right || left)){
			Vector3 playerVelocity = rb.velocity;
			playerVelocity.x = 0.0f;
			rb.velocity = playerVelocity;
		}

		if (jump && grounded){
			grounded = false;
			rb.AddForce(Vector3.up * jumpForce);
		}	

        if(sword != null && (Time.time > lastAttack + attackDuration))
        {
            GameObject.Destroy(sword);
        }

		if (attack){
            Attack();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "bean"){
			BeanController bean = other.gameObject.GetComponent<BeanController>();
			bean.Destroy();
			SpriteRenderer sprender = myBean.GetComponent<SpriteRenderer>();
			sprender.enabled = true;
		}

		if (other.tag == "goal"){
			SpriteRenderer sprender = myBean.GetComponent<SpriteRenderer>();
			if(sprender.enabled){
				Rigidbody2D rb = myBean.GetComponent<Rigidbody2D>();
				rb.isKinematic = false;
				rb.gravityScale = 3f;
			}
		}
	}

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 facing = feet.localScale;
        facing.x *= -1;
        feet.localScale = facing;
    }

    private float lastAttack;
    private void Attack()
    {
        if(sword == null)
        {
            
            if (m_FacingRight)
            {
                sword = GameObject.Instantiate(p_RightSword);
                sword.transform.SetParent(t_RightSword);
                sword.transform.localPosition = Vector3.zero;
                sword.transform.localScale = new Vector3(0.04783243f, 0.04783245f, 0.04783245f);
            }
            else
            {
                sword = GameObject.Instantiate(p_LeftSword);
                sword.transform.SetParent(t_LeftSword);
                sword.transform.localPosition = Vector3.zero;
                sword.transform.localScale = new Vector3(-0.04783243f, 0.04783245f, 0.04783245f);
            }
            lastAttack = Time.time;
        }
    }
}
