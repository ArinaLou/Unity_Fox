﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void UIEnable()
	{
		GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
	}

	public void About()
	{
		SceneManager.LoadScene("About");
	}
	public void Back()
	{
		SceneManager.LoadScene("Menu");
	}
}
