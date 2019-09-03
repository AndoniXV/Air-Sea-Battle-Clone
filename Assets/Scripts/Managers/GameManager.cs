using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that handles the time, the points and the flow of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
	//Singleton
	public static GameManager instance = null;

	[Header("References")]
	[SerializeField] private GameConfiguration gameConfiguration;
	[SerializeField] private PlayerController player;

	[Header("Top texts")]
	[SerializeField] private Text currentScoreText;
	[SerializeField] private Text highScoreText;
	[SerializeField] private Text timeText;

	[Header("Pause Menu")]
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private Button resumeButton;
	[SerializeField] private Button exitButton;

	[Header("Game Over Menu")]
	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private GameObject newScoreHolder;
	[SerializeField] private Text newScoreText;
	[SerializeField] private Button exitButton2;
	[SerializeField] private LastScore lastScoreConfiguration;


	private int points;
	private float timer;
	private bool isGamePaused;
	private bool isGameEnded;

	public bool IsGamePaused
	{
		get { return isGamePaused; }
		set { isGamePaused = value; }
	}

    void Awake()
    {
		//Singleton pattern
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		//Set scores and time texts
		currentScoreText.text = "0";
		points = 0;
		highScoreText.text = gameConfiguration.default_high_score.ToString();

		timer = gameConfiguration.time_limit;
		timeText.text = ((int)timer).ToString();

		//Set buttons events
		resumeButton.onClick.AddListener(ResumeGame);
		exitButton.onClick.AddListener(ExitGame);
		exitButton2.onClick.AddListener(ExitGame);
	}

    void Update()
    {
		if(timer - Time.deltaTime <= 0 && !isGameEnded)
		{
			isGameEnded = true;
			EndGame();
		}

		timer -= Time.deltaTime;
		timeText.text = ((int)timer).ToString();
	}

	/// <summary>
	/// Add the corresponding points to the score for a killed plane.
	/// </summary>
	public void PlaneKilled()
	{
		points += gameConfiguration.points_per_plane;
		currentScoreText.text = points.ToString();
	}

	private void EndGame()
	{
		if(points > gameConfiguration.default_high_score)
		{
			gameConfiguration.default_high_score = points;
			newScoreHolder.SetActive(true);
			newScoreText.text = "NEW HIGH SCORE: " + points;
		}
		else
		{
			newScoreHolder.SetActive(false);
		}

		lastScoreConfiguration.LastScorePoints = points;
		lastScoreConfiguration.IsFirstTime = false;
		gameOverMenu.SetActive(true);
		Time.timeScale = 0;
	}

	public void PauseGame()
	{
		isGamePaused = true;
		pauseMenu.SetActive(true);
		Time.timeScale = 0;		//Pause time
		player.IsLocked = true; //Stop player input
	}

	public void ResumeGame()
	{
		isGamePaused = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1;		//Play time again
		player.IsLocked = false;//Resume player input
	}

	public void ExitGame()
	{
		SceneLoader.Instance.LoadScene(1);
		Time.timeScale = 1;
	}
}
