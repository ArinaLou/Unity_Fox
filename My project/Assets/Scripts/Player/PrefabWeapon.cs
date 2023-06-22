using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour {

	public Transform firePoint;
	public GameObject bulletPrefab;
	PlayerMovement player;


	void Start()
	{
		GameObject playerObject = GameObject.FindWithTag("Player"); // 使用标签查找对象
		player = playerObject.GetComponent<PlayerMovement>(); // 获取PlayerMovement组件的引用
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
