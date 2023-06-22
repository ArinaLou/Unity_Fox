using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	protected Rigidbody2D rb;
	private Animator anim;
	public  Collider2D coll, DisColl;
	public Transform CellingCheck,GroundCheck;
	public AudioSource JumpAudio, HurtAudio, CherryAudio, HeartAudio;
	public LayerMask ground;

	[Header("移动参数")]
	public float speed;
	public float JumpForce;
	public float jumpHoldForce;
	public float jumpHoldDuration;

	float jumpTime;

	[Header("物品参数")]
	public int Cherry;
	public int Heart = 3;
	public Text CherryNum, HeartNum;

	[Header("状态")]
	private bool isHurt, isGround;
	public bool isJump;
	//public static bool hasKey;
	//public bool hasJian;

	//按键设置
	bool jumpPressed;
	bool jumpHeld;
	bool crouchHeld;

	public GameObject DeadMenu;
	//public GameObject Key;
	//public GameObject Jian;
	protected int extraJump;
	private Vector3 startPosition;

	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		startPosition = transform.position;
	}

    void FixedUpdate()
    {
		if (!isHurt)
		{
			Movement();
		}
		SwitchAnim();
		isGround = Physics2D.OverlapCircle(GroundCheck.position,0.1f,ground);
	}

	private void Update()
	{
		jumpPressed = Input.GetButtonDown("Jump");
		jumpHeld = Input.GetButton("Jump");
		crouchHeld = Input.GetButton("Crouch");

		Crouch();
		Jump();
		CherryNum.text = Cherry.ToString();
		CheckHeathy();
	}

	//角色移动
	void Movement()
	{
		float horizontalMove = Input.GetAxis("Horizontal");
		float faceDircetion = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
		anim.SetFloat("running", Mathf.Abs(horizontalMove));
		if (faceDircetion != 0)
		{
			transform.localScale = new Vector3(faceDircetion, 1, 1);
		}

	}
	//切换动画
	void SwitchAnim()
	{
		//anim.SetBool("idle", false);

		if(rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
		{
			anim.SetBool("falling", true) ;
		}

		if (anim.GetBool("jumping"))
		{
			if(rb.velocity.y < 0)
			{
				anim.SetBool("jumping", false);
				anim.SetBool("falling", true);
			}
		}
		else if (isHurt)
		{
			anim.SetBool("hurt", true);
			anim.SetFloat("running", 0);
			if (Mathf.Abs(rb.velocity.x) < 0.1f)
			{
				anim.SetBool("hurt", false);
				//anim.SetBool("idle", true);
				isHurt = false;
			}
		}
		else if (coll.IsTouchingLayers(ground)){
			anim.SetBool("falling", false);
			//anim.SetBool("idle", true);
		}
	}
	//碰撞物体 [收集物品,死亡，拿到钥匙]
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//樱桃
		if (collision.tag == "Cherry")
		{
			CherryAudio.Play();
			collision.GetComponent<Animator>().Play("IsGot");
			CherryNum.text = Cherry.ToString();
		}
		//心
		if (collision.tag == "Heart")
		{
			HeartAudio.Play();
			Destroy(collision.gameObject);
			Heart += 1;
			HeartNum.text = Heart.ToString();
		}

		//死亡重启
		if (collision.tag == "DeadLine")
		{
			Stop();
		}


/*		if (collision.tag == "Key")
		{
			// 设置玩家拿到钥匙的状态
			hasKey = true;

			Key.SetActive(false);
		}*/
	}
	//消灭敌人
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();

			if (anim.GetBool("falling"))
			{
				enemy.death();
				rb.velocity = new Vector2(rb.velocity.x, JumpForce * Time.deltaTime);
				anim.SetBool("jumping", true);
			}
			//受伤动画
			else if (transform.position.x < collision.gameObject.transform.position.x)
			{
				rb.velocity = new Vector2(-5, rb.velocity.y);
				HurtAudio.Play();
				isHurt = true;
				Heart--;
				HeartNum.text = Heart.ToString();
			}
			else if (transform.position.x > collision.gameObject.transform.position.x)
			{
				rb.velocity = new Vector2(5, rb.velocity.y);
				HurtAudio.Play();
				isHurt = true;
				Heart--;
				HeartNum.text = Heart.ToString();
			}
		}

	}
	//跳跃[二段跳]
	void Jump()
	{
		if(isGround)
		{
			extraJump = 2;
		}
		if(jumpPressed && extraJump > 0)
		{
			isGround = false;
			isJump = true;

			jumpTime = Time.time + jumpHoldDuration;

			JumpAudio.Play();
			rb.velocity = new Vector2(rb.velocity.x, JumpForce);
			extraJump--;
			anim.SetBool("jumping", true);
		}
		if (jumpPressed && extraJump == 0 && isGround)
		{
			isGround = false;
			isJump = true;
			JumpAudio.Play();
			rb.velocity = new Vector2(rb.velocity.x, JumpForce);
			anim.SetBool("jumping", true);
		}

		else if (isJump)
		{
			if (jumpHeld)
			{
			rb.AddForce(new Vector2(rb.velocity.x, jumpHoldForce), ForceMode2D.Impulse);
			}
			if (jumpTime < Time.time) isJump = false;
		}
	}
	//蹲下
	void Crouch()
	{
		if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))
		{
			if (crouchHeld)
			{
				anim.SetBool("crouching", true);
				DisColl.enabled = false;
			}
			else
			{
				anim.SetBool("crouching", false);
				DisColl.enabled = true;
			}
		}
		
	}
	//检查血量
	void CheckHeathy()
	{
		if(Heart <= 0)
		{
			Stop();
		}
	}
	//停止游戏，并显示死亡画面
	public void Stop()
	{
		GetComponent<AudioSource>().enabled = false;
		DeadMenu.SetActive(true);
		Time.timeScale = 0;
	}
	//重置玩家的状态
	public void ResetPlayer()
	{
		transform.position = startPosition; // 设置为初始位置
		rb.velocity = Vector2.zero; // 设置刚体速度为零
		Heart = 3;//问题关键 不能删
		HeartNum.text = Heart.ToString();
		isHurt = false;
		coll.enabled = true;
	}
	public void CherryCount()
	{
		Cherry += 1;
	}

}
