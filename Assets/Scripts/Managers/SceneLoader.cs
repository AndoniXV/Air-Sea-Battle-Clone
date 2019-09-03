using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	//Singleton
	public static SceneLoader Instance = null;

	[SerializeField] private DataRetriever dataRetriever;
	[SerializeField] private LastScore lastScoreConfiguration;
	[SerializeField] private GameConfiguration gameConfiguration;

	private bool firstTime = true;
	private int sceneToLoad = 1;

	private void Awake()
	{
		//Singleton pattern
		if (Instance == null)
		{
			Instance = this;
			SceneManager.sceneLoaded += OnSceneLoaded;
			DontDestroyOnLoad(this.gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		if (firstTime && SceneManager.GetActiveScene().buildIndex == 0)
		{
			gameConfiguration.RestartToDefault();				 //Restart game configuration to default in case data cannot be retrieved (this is only needed in editor as it's a SO).
			dataRetriever.AddFinishDataListener(LoadFirstScene); //Load first scene when data is finished retrieving (or fails to retrieve)
			dataRetriever.RetrieveData();						 //Retrive data
		}
	}

	//Is called when a new scene is loaded
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex == 0 && !firstTime)
		{
			StartCoroutine(LoadSceneCoroutine(sceneToLoad));
		}
	}

	private void LoadFirstScene()
	{
		StartCoroutine(LoadSceneCoroutine(1));
		firstTime = false;

		//Only for editor purposes (in build would work anyway)
		lastScoreConfiguration.IsFirstTime = true;
	}

	/// <summary>
	/// Load given scene.
	/// </summary>
	/// <param name="sceneToLoad"></param>
	public void LoadScene(int sceneToLoad)
	{
		this.sceneToLoad = sceneToLoad;

		if(SceneManager.GetActiveScene().buildIndex != 0)
		{
			SceneManager.LoadScene(0);
		}
	}

	public IEnumerator LoadSceneCoroutine(int sceneToLoad)
	{
		//Wait some seconds to make Loading Scene longer as for this game loading times are really low
		yield return new WaitForSeconds(1.5f);

		AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

		//Wait until scene is loaded to finish
		while (!async.isDone)
		{
			yield return null;
		}
	}
}
