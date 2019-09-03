using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class DataRetriever : MonoBehaviour
{
	[SerializeField] private GameConfiguration gameConfiguration;
	[SerializeField] private string endpointURL;

	private event Action OnFinishDataRetrieving;

	/// <summary>
	/// Retrieve data for Game Configuration.
	/// </summary>
	public void RetrieveData()
	{
		StartCoroutine(RetrieveDataCoroutine());
	}

	IEnumerator RetrieveDataCoroutine()
	{
		UnityWebRequest www = UnityWebRequest.Get(endpointURL);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			//Error. Default values in GameConfiguration Scriptable Object stay there.
			Debug.LogWarning(www.error);
		}
		else
		{
			//Parse results to GameConfiguration SO
			ParseData(www.downloadHandler.text);
		}

		//Trigger OnFinishData event
		OnFinishDataRetrieving?.Invoke();
	}

	void ParseData(string jsonText)
	{
		//Parse results to GameConfiguration Scriptable Object
		JsonUtility.FromJsonOverwrite(jsonText, gameConfiguration);
	}

	public void AddFinishDataListener(Action listener)
	{
		OnFinishDataRetrieving += listener;
	}
}
