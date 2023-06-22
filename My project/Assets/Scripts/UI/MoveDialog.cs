using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDialog : MonoBehaviour
{
	public GameObject moveDailog;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			moveDailog.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			moveDailog.SetActive(false);
		}
	}
}
