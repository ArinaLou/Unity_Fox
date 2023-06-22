using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
	public AudioMixer audioMixer;
	public  GameObject PauseMenu;

	public void Pasue()
	{
		PauseMenu.SetActive(true);
		Time.timeScale = 0;
	}

	public void ReturnGame()
	{
		PauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
	public void SetVolume(float value)
	{
		audioMixer.SetFloat("MainVolume", value);
	}


	public void TryAgain()
	{
		Input.ResetInputAxes();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1;

		GameObject player = GameObject.FindWithTag("Player");
		if (player != null)
		{
			PlayerController playerController = player.GetComponent<PlayerController>();
			if (playerController != null)
			{

				playerController.ResetPlayer();
			}
		}
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1;
	}

}
