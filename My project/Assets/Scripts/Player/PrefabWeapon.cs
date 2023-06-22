using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;
	PlayerMovement player;


	void Start()
	{
		GameObject playerObject = GameObject.FindWithTag("Player"); // ʹ�ñ�ǩ���Ҷ���
		player = playerObject.GetComponent<PlayerMovement>(); // ��ȡPlayerMovement���������
	}

	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			if (player.hasJian)
			{
				Shoot();
			}
		}

        
	}

	void Shoot ()
	{
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}
}
