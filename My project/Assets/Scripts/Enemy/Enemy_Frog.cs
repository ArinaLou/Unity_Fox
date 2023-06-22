using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D Coll;
    public LayerMask Ground;
    public Transform leftpoint, rightpoint;
	public float Speed, JumpForce;
    private float leftx, rightx;

	public bool Faceleft = true;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnim();
	}

    void Movement()
    {
        if (Faceleft)
        {
            if(Coll.IsTouchingLayers(Ground))
            {
				animator.SetBool("jumping", true);
				rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if(transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1,1,1);
                Faceleft = false;
            }
        }
        else
        {
			if (Coll.IsTouchingLayers(Ground))
			{
				animator.SetBool("jumping", true);
				rb.velocity = new Vector2(Speed, JumpForce);
			}
			if (transform.position.x > rightx)
			{
				transform.localScale = new Vector3(1, 1, 1);
				Faceleft = true;
			}
		}
    }

    void SwitchAnim()
    {
        if (animator.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground) && animator.GetBool("falling"))
        {
            animator.SetBool("falling", false);
        }
    }

	
}
