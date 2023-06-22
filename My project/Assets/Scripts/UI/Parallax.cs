using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float xMoveRate, yMoveRate;
    private float startPointX,startPointY;
    public bool lockY;

    // Start is called before the first frame update
    void Start()
    {
        startPointX = transform.position.x;    
    }

    // Update is called once per frame
    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX + Cam.position.x * xMoveRate, transform.position.y);
        }
        else
        {
			transform.position = new Vector2(startPointX + Cam.position.x * xMoveRate, startPointY + Cam.position.y * yMoveRate);
		}
    }
}
