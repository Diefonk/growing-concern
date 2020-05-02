using UnityEngine;

public class FollowCursor : MonoBehaviour {
	public float offset;
	private RectTransform rectTransform;

	private void Awake() {
		rectTransform = transform as RectTransform;
	}

    private void Update() {
		rectTransform.position = Input.mousePosition;
		Vector2 pivot = new Vector2();
		if (rectTransform.localPosition.x > 0.0f) {
			pivot.x = 1.0f + offset / (rectTransform.rect.width / rectTransform.rect.height);
		} else {
			pivot.x = 0.0f - offset / (rectTransform.rect.width / rectTransform.rect.height);
		}
		if (rectTransform.localPosition.y < 0.0f) {
			pivot.y = 0.0f - offset;
		} else {
			pivot.y = 1.0f + offset;
		}
		rectTransform.pivot = pivot;
	}
}
