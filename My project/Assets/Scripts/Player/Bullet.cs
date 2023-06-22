using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 40f;
	public int damage = 20;
	public Rigidbody2D rb;
	public GameObject impactEffect;

	private PlayerMovement player;
	void Start () {
		player = FindObjectOfType<PlayerMovement>();
		// ��������泯������������䷽��
		if (player != null)
		{
			if (player.transform.localScale.x > 0)
			{
				rb.velocity = transform.right * speed;
				transform.localScale = new Vector3(1f, 1f, 1f); // ��������Ϊ������С
			}
			else
			{
				rb.velocity = -transform.right * speed;
				transform.localScale = new Vector3(-1f, 1f, 1f); // ��������Ϊ����
			}
		}
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		BossHealth enemy = hitInfo.GetComponent<BossHealth>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}

		Instantiate(impactEffect, transform.position, transform.rotation);

		//Debug.Log(hitInfo.name);
		//Destroy(gameObject);
	}

}
