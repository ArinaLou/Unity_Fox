﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
public class Boss2 : MonoBehaviour
{
	[Header("Pathfinding")]
	public Transform target;
	public float activateDistance = 50f;
	public float pathUpdateSeconds = 0.5f;

	[Header("Physics")]
	public float speed = 200f;
	public float nextWaypointDistance = 3f;
	public float jumpNodeHeightRequirement = 0.8f;
	public float jumpModifier = 0.3f;
	public float jumpCheckOffset = 0.1f;

	[Header("Custom Behavior")]
	public bool followEnabled = true;
	public bool jumpEnabled = true;
	public bool directionLookEnabled = true;

	[Header("状态")]
	public int health;
	public Slider healthBar;
	public bool isDead;

	[Header("预设")]
	public GameObject keyObject;
	public float deathDelay = 2f; // 死亡延迟时间

	Animator anim;
	private Path path;
	private int currentWaypoint = 0;
	bool isGrounded = false;
	Seeker seeker;
	Rigidbody2D rb;

	public void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		seeker = GetComponent<Seeker>();
		anim = GetComponent<Animator>();

		InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
	}

	private void FixedUpdate()
	{
		if (TargetInDistance() && followEnabled)
		{
			PathFollow();
		}
		death();
		healthBar.value = health;
	}


	private void UpdatePath()
	{
		if (followEnabled && TargetInDistance() && seeker.IsDone())
		{
			seeker.StartPath(rb.position, target.position, OnPathComplete);
		}
	}

	private void PathFollow()
	{
		if (path == null) return;

		if (currentWaypoint >= path.vectorPath.Count)
		{
			return;
		}
		isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);

		Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

		Vector2 force = direction * speed * Time.deltaTime;

		if (jumpEnabled && isGrounded)
		{
			if (direction.y > jumpNodeHeightRequirement)
			{
				rb.AddForce(Vector2.up * speed * jumpModifier);
			}
		}
		rb.AddForce(force);

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

		if (distance < nextWaypointDistance)
		{
			currentWaypoint++;
		}

		if (directionLookEnabled)
		{
			if (rb.velocity.x < -0.05f)
			{
				transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
			else if (rb.velocity.x > 0.05f)
			{
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
		}
	}

	private bool TargetInDistance()
	{
		return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
	}

	private void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}

	public void death()
	{
		if(health <= 0)
		{
			// 播放死亡动画或其他逻辑
			anim.SetTrigger("death");

			isDead = true;
			StartCoroutine(DisappearAfterDelay());
		}
	}

/*	public void Attack()
	{
		PlayerController player = new PlayerController();
		if(health > 0 && player.hasJian) 
		{

		}
	}*/

	private IEnumerator DisappearAfterDelay()
	{
		yield return new WaitForSeconds(deathDelay);
		gameObject.SetActive(false);
		// 在Boss死亡后生成Key对象
		keyObject.SetActive(true);
	}
}
