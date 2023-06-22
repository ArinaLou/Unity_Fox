using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cion : MonoBehaviour
{
	public AudioSource audioSourcel;
	Collider2D Coll;


	private void Start()
	{
		Coll = GetComponent<Collider2D>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			audioSourcel.Play();
			Destroy(Coll.gameObject);
		}
	}

}
