using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Bed : Clickable {
	public Farm farm;
	public Animator blackout;
	public Player player;
	public Transform walkGoal;

	public override void click() {
		StartCoroutine(sleep());
	}

	private IEnumerator sleep() {
		player.setUIActive(true);
		player.setDestination(walkGoal.position);
		player.removeOldClickable();
		blackout.SetTrigger("fadeOut");
		float timeout = Time.time + 1.0f;
		while (Time.time < timeout) {
			yield return null;
		}
		player.GetComponent<NavMeshAgent>().Warp(walkGoal.position);
		farm.sleep();
		blackout.SetTrigger("fadeIn");
		player.setUIActive(false);
	}

	public override void hoverEnter() {
		text = farm.getIsDay() ? "Sleep until dusk" : "Sleep until dawn";
	}
}
