using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchDia : MonoBehaviour
{
	public GameObject crouchDailog;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			crouchDailog.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			crouchDailog.SetActive(false);
		}
	}
}
