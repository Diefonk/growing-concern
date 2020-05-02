using UnityEngine;

public class Plant : MonoBehaviour {
	public bool dayPlant;
	public float growTime;
	public float normalScale;
	private float growCountdown;

	private void Awake() {
		normalScale = transform.localScale.x;
	}

	public void plant() {
		GetComponent<Renderer>().enabled = true;
		transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		growCountdown = growTime;
	}

	public float grow(float time) {
		growCountdown -= time;
		if (growCountdown <= 0.0f) {
			transform.localScale = new Vector3(normalScale, normalScale, normalScale);
			return 0.0f;
		}
		float scale = Mathf.Lerp(0.1f, normalScale, (growTime - growCountdown) / growTime);
		transform.localScale = new Vector3(scale, scale, scale);
		return growCountdown;
	}
}
