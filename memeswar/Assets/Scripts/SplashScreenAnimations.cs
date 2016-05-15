using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenAnimations : MonoBehaviour 
{
	const float FADE_DURATION = 1f;

	const float IFRN_FADEIN = 2f;
	const float IFRN_FADEOUT = IFRN_FADEIN + FADE_DURATION*2 + 2f;

	const float MACAMBIRA_FADEIN = IFRN_FADEOUT + 1f;
	const float MACAMBIRA_FADEOUT = MACAMBIRA_FADEIN + FADE_DURATION * 2 + 2f;

	public Image IFRN;

	public Image Macambira;

	public AudioSource BackgroundAudio;

	void Start ()
	{
		Color color = this.IFRN.color;
		color.a = 0;

		this.IFRN.color = color;
		this.Macambira.color = color;
	}

	private float alphaAnimate(float tin, float tout, float fduration)
	{
		if ((Time.timeSinceLevelLoad < tin) || (Time.timeSinceLevelLoad > tout))
			return 0f;
		else if (Time.timeSinceLevelLoad < tin + fduration)
			return ((Time.timeSinceLevelLoad - tin) / fduration);
		else if (Time.timeSinceLevelLoad > tout - fduration)
			return ((tout - Time.timeSinceLevelLoad) / fduration);
		else
			return 1f;
	}

	void Update ()
	{
		Color color = this.IFRN.color;
		color.a = this.alphaAnimate(IFRN_FADEIN, IFRN_FADEOUT, FADE_DURATION);
		this.IFRN.color = color;

		color = this.Macambira.color;
		color.a = this.alphaAnimate(MACAMBIRA_FADEIN, MACAMBIRA_FADEOUT, FADE_DURATION);
		this.Macambira.color = color;

		if (Time.timeSinceLevelLoad > MACAMBIRA_FADEOUT - FADE_DURATION)
			this.BackgroundAudio.volume = this.alphaAnimate(MACAMBIRA_FADEIN, MACAMBIRA_FADEOUT, FADE_DURATION);

		if (Time.timeSinceLevelLoad > MACAMBIRA_FADEOUT + 1f)
		{
			Debug.Log("Aqui!!!");
			SceneManager.LoadScene("MainMenu");
		}
	}
}
