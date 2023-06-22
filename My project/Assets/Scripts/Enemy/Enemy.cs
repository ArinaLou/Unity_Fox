using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	protected Animator animator;
	protected AudioSource deathAudio;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
		deathAudio = GetComponent<AudioSource>();
    }

	public void death()
	{
		animator.SetTrigger("death");
		deathAudio.Play();
	}
	public void JumpOn()
	{
		GetComponent<Collider2D>().enabled = false;
		Destroy(gameObject);
	}

}
