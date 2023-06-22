using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject Jian;
	public GameObject FireDialog;

	public PlayerMovement player;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if (collision.tag == "Player")
		{
			player.hasJian = true;
			Jian.SetActive(true);
			FireDialog.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			Destroy(gameObject);
			FireDialog.SetActive(false);
		}
	}

}
