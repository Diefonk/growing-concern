using UnityEngine;

public class House : MonoBehaviour {
	public Player player;
    public Renderer[] renderers;

	private void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == player.name) {
			foreach (Renderer renderer in renderers) {
				renderer.enabled = false;
			}
		}
	}

	private void OnTriggerExit(Collider collider) {
		if (collider.gameObject.name == player.name) {
			foreach (Renderer renderer in renderers) {
				renderer.enabled = true;
			}
		}
	}
}
