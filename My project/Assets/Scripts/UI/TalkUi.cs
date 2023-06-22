using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkUi : MonoBehaviour
{

	public GameObject textDialog;
	public GameObject textimage;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			textDialog.SetActive(true);
			textimage.SetActive(false);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			textDialog.SetActive(false);
		}
	}
}
