using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour {
	public float dayTime;
	public Transform sun;
	public Image dayImage;
	public Image nightImage;
	public Animator music;
    private FarmPlot[] plots;
	private bool day = true;
	private float dayCountdown;

	private void Awake() {
		plots = GetComponentsInChildren<FarmPlot>();
		dayCountdown = dayTime;
	}

	private void Update() {
		if (Time.deltaTime >= dayCountdown) {
			updateTime(dayCountdown / dayTime);
			day = !day;
			updateTime((Time.deltaTime - dayCountdown) / dayTime);
			dayCountdown = dayTime;
			if (day) {
				dayImage.enabled = true;
				nightImage.enabled = false;
				dayImage.fillAmount = 1.0f;
				music.SetTrigger("day");
			} else {
				dayImage.enabled = false;
				nightImage.enabled = true;
				nightImage.fillAmount = 1.0f;
				music.SetTrigger("night");
			}
		} else {
			updateTime(Time.deltaTime / dayTime);
			dayCountdown -= Time.deltaTime;
			if (day) {
				dayImage.fillAmount = dayCountdown / dayTime;
			} else {
				nightImage.fillAmount = dayCountdown / dayTime;
			}
		}
	}

	private void updateTime(float time) {
		foreach (FarmPlot plot in plots) {
			plot.grow(time, day);
		}
		sun.Rotate(time * 180.0f, 0.0f, 0.0f);
	}

	public bool getIsDay() {
		return day;
	}

	public void sleep() {
		updateTime(dayCountdown / dayTime);
		day = !day;
		dayCountdown = dayTime;
		if (day) {
			dayImage.enabled = true;
			nightImage.enabled = false;
			dayImage.fillAmount = 1.0f;
			music.SetTrigger("day");
		} else {
			dayImage.enabled = false;
			nightImage.enabled = true;
			nightImage.fillAmount = 1.0f;
			music.SetTrigger("night");
		}
	}
}
