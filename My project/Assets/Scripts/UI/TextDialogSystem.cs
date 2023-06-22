using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;

    bool textFinished;
    bool cancelTyping;

    List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        GetTextFormFile(textFile);
    }
	private void OnEnable()
	{
        textFinished = true;
		StartCoroutine(SetTextUI());
	}

	// Update is called once per frame
	void Update()
    {
        if(index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }else if (!textFinished) 
            { 
                cancelTyping = !cancelTyping;
            } 
        }

    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var listData = file.text.Split('\n');

        foreach (var line in listData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";
        int letter = 0;
        while(!cancelTyping && letter < textList[index].Length - 1)
        {
			textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(0.1f); ;
		}
        textLabel.text = textList[index];
        cancelTyping = false;
		textFinished = true;
        index++;
    }
}
