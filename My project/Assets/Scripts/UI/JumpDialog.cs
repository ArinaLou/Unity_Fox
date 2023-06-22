using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDialog : MonoBehaviour
{
	public GameObject jumpDailog;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			jumpDailog.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			jumpDailog.SetActive(false);
		}
	}
}
