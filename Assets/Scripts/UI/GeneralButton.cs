using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralButton : MonoBehaviour
{
	[SerializeField] private AudioClip buttonPressedSound; //Sound the button will make when pressed.

	protected virtual void Awake()
	{
		GetComponent<Button>().onClick.AddListener(buttonPressed);
	}

	protected virtual void buttonPressed()
	{
		AudioManager.Instance.PlayAudio(buttonPressedSound);
	}
}
