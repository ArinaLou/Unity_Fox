using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Coin : MonoBehaviour
{

	public GameObject Coin1;
	public GameObject Coin2;
	public GameObject Coin3;

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.tag == "Player")
		{
			Coin1.SetActive(true);
			Coin2.SetActive(true);
			Coin3.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			Destroy(gameObject);
		}
	}



}
