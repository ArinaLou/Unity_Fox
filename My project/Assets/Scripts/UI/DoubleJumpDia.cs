using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpDia : MonoBehaviour
{
	public GameObject doubleJumpDailog;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			doubleJumpDailog.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			doubleJumpDailog.SetActive(false);
		}
	}
}
