using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Text highScoreText;
	[SerializeField] private GameConfiguration gameConfiguration;

	[SerializeField] private Text lastScoreText;
	[SerializeField] private Text lastScoreTitleText;
	[SerializeField] private LastScore lastScoreConfiguration;

	private void Awake()
	{
		highScoreText.text = gameConfiguration.default_high_score.ToString();

		if (!lastScoreConfiguration.IsFirstTime)
		{
			lastScoreText.gameObject.SetActive(true);
			lastScoreTitleText.gameObject.SetActive(true);
			lastScoreText.text = lastScoreConfiguration.LastScorePoints.ToString();
		}

	}
	public void PlayGame()
	{
		SceneLoader.Instance.LoadScene(2);
	}
}
