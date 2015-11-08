using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpForce;

	public Sprite leftBody, rightBody;

	public GameObject myBean, leftFoot, rightFoot, body;
	private SpriteRenderer beanRenderer;

	public float duration, magnitude;

    public float attackDuration;
    public GameObject holdingSword, swordLeft, swordRight;


    public Transform feet;
    public Transform hands;


	private bool grounded = false;

	private Rigidbody2D rb;
	private Transform groundCheck;
    private bool m_FacingRight = true;
    private GameObject sword;
    private bool isAttacking, stunned;

    private SpriteRenderer leftFootSprender, rightFootSprender, bodySprender;

    void Start(){
		rb = GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("groundCheck");

		leftFootSprender = leftFoot.GetComponent<SpriteRenderer>();
		rightFootSprender = rightFoot.GetComponent<SpriteRenderer>();
		bodySprender = body.GetComponent<SpriteRenderer>();
		beanRenderer = myBean.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!stunned){
			grounded = Physics2D.Linecast(gameObject.transform.position, groundCheck.position, (1 << LayerMask.NameToLayer("floor")) | (1<<LayerMask.NameToLayer("wall"))); 

			bool left = Input.GetKey("left");
			bool right = Input.GetKey("right");
			bool jump = Input.GetKeyDown(KeyCode.Z);
			bool attack = Input.GetKeyDown(KeyCode.X);


			if (left){
				rb.velocity = new Vector3(-speed, rb.velocity.y, 0f);
				leftFootSprender.sortingOrder = -1;
				rightFootSprender.sortingOrder = 2;
				bodySprender.sprite = leftBody;
	            if (m_FacingRight)
	            {
	                Flip();
	            }
				m_FacingRight = false;
			}

			if (right){
				rb.velocity = new Vector3(speed, rb.velocity.y, 0f);
				bodySprender.sprite = rightBody;
				leftFootSprender.sortingOrder = 2;
				rightFootSprender.sortingOrder = -1;
	            if (!m_FacingRight)
	            {
	                Flip();
	            }
	            m_FacingRight = true;
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
	            sword.SetActive(false);
	            holdingSword.SetActive(true);
	            isAttacking = false;
	        }

			if (attack){
	            Attack();
			}
		}
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "bean") {
            BeanController bean = other.gameObject.GetComponent<BeanController>();
            bean.Destroy();
            beanRenderer.enabled = true;
        }

        if (other.tag == "goal") {
            if (beanRenderer.enabled) {
                Rigidbody2D rb = myBean.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
                rb.gravityScale = 3f;
            }
        }
        if (other.tag == "enemy")
        {
        	if(beanRenderer.enabled){
        		Rigidbody2D beanrb = myBean.GetComponent<Rigidbody2D>();
        		beanrb.isKinematic = true;
        		beanRenderer.enabled = false;
        		BeanSpawner.SpawnNewBean();
        	}
            other.GetComponentInParent<Animator>().SetTrigger("Strike");
            StartCoroutine(Shake());
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        SideSwitch(hands);
    }

    private void SideSwitch(Transform feet)
    {
        Vector3 facing = feet.localScale;
        facing.x *= -1;
        feet.localScale = facing;
    }


    private float lastAttack;
    private void Attack()
    {
        if(!isAttacking)
        {
            holdingSword.SetActive(false);
            if (m_FacingRight)
            {
                swordRight.SetActive(true);
                sword = swordRight;
            }
            else
            {
                swordLeft.SetActive(true);
                sword = swordLeft;
            }
            isAttacking = true;
            lastAttack = Time.time;
        }
    }

    IEnumerator Shake() {

    	MusicMiddleware.playSound("Bad Sound");

    	stunned = true;
    	rb.velocity = Vector3.zero;
	        
	    float elapsed = 0.0f;
	    
	    Vector3 originalCamPos = Camera.main.transform.position;
	    
	    while (elapsed < duration) {
	        
	        elapsed += Time.deltaTime;          
	        
	        float percentComplete = elapsed / duration;         
	        float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
	        
	        // map value to [-1, 1]
	        float x = Random.value * 2.0f - 1.0f;
	        float y = Random.value * 2.0f - 1.0f;
	        x *= magnitude * damper;
	        y *= magnitude * damper;

	        x += originalCamPos.x;
	        y += originalCamPos.y;
	        
	        Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
	            
	        yield return null;
	    }
	    
	    stunned = false;

	    Camera.main.transform.position = originalCamPos;
	}
}
