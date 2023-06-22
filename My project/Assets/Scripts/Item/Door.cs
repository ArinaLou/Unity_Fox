using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    public GameObject player;
	public GameObject info;
	public Collider2D DisColl;
	bool isLocked = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {	

	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (isLocked)
			{
				if (PlayerMovement.hasKey)
				{
					// 如果玩家拥有钥匙，则解锁门
					isLocked = false;
					anim.SetTrigger("Open");
					DisColl.enabled = false;
				}
				else
				{
					info.SetActive(true);
					DisColl.enabled = true;
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			info.SetActive(false);
		}
	}

}
