using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class genText : MonoBehaviour
{

	public int letterPause = 1;
	Text guiText;
	string message;

	// Use this for initialization
	void Start()
	{
		guiText = gameObject.GetComponent<Text>();
		message = guiText.text;
		guiText.text = "";
		TypeText();
	}

	void TypeText()
	{
		foreach (char letter in message.ToCharArray())
		{
			guiText.text += letter;
			System.Threading.Thread.Sleep(letterPause);


		}
	}
}