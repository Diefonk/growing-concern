using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Traveller : Clickable {
	public Player player;
	public RectTransform bubble;
	public Animator animator;
	public Button close1;
	public Button close2;
	public Button close3;
	public Transform position2;
	private Vector3 bubbleOffset = new Vector3(0.0f, 2.0f, 0.0f);
	private int state = 0;
	public AudioHandler trav;
	public AudioHandler you;

	public override void click() {
		player.setUIActive(true);
		player.setDestination(transform.position);
		bubble.GetComponent<Image>().enabled = false;
		switch (state) {
			case 0:
				player.setArrivalAction(t00);
				break;
			case 1:
				player.setArrivalAction(t10);
				break;
			default: break;
		}
	}

	private void t00() {
		say("A lone traveller greets you as you approach them.", t01);
	}

	private void t01() {
		say("Hello, witch!", t02, trav);
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void t02() {
		say("I am going into battle, and I want your strongest potions.", t03, trav);
		animator.SetTrigger("talk");
	}

	private void t03() {
		say("They're too strong for you, traveller.", t04, you);
	}

	private void t04() {
		say("That's what the potion seller said, which is why I came directly to you instead.", t05, trav);
		animator.SetTrigger("talk2");
	}

	private void t05() {
		say("Nevertheless, you can't handle my strongest potions.", t06, you);
	}

	private void t06() {
		say("Look, I just had a whole conversation like this with the potion seller, and I'm not in the mood for repeating it.", t07, trav);
		animator.SetTrigger("talk");
	}

	private void t07() {
		say("I'm going into battle and I want 5 of your strongest potions.", t08, trav);
		animator.SetTrigger("talk2");
	}

	private void t08() {
		say("I'll pay you twice the amount you get from the potion seller, and some of it right now.", t09, trav);
		animator.SetTrigger("talk");
	}

	private void t09() {
		say("Please!", t010, trav);
		animator.SetTrigger("pray");
	}

	private void t010() {
		say("...", t011, you);
	}

	private void t011() {
		say("Fine.", t012, you);
	}

	private void t012() {
		say("Thank you!", t013, trav);
		animator.SetTrigger("talk2");
	}

	private void t013() {
		say("They hand you 300 moneys.", t014);
		player.GetComponent<Inventory>().moneys += 300;
	}

	private void t014() {
		say("Wait here while I go prepare them.", close, you);
		Dialogue.dialogue.setObjective("Current objective: Brew 5 strongest potions for the traveller");
		state = 1;
	}

	private void t10() {
		if (player.GetComponent<Inventory>().strongest >= 5) {
			t1y0();
		} else {
			t1n0();
		}
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void t1y0() {
		say("Here are your potions.", t1y1, you);
		player.GetComponent<Inventory>().strongest -= 5;
	}

	private void t1y1() {
		say("But I still advise you not to use them, since they are too strong and will kill you.", t1y2, you);
	}

	private void t1y2() {
		say("And I tell you that I can handle them.", t1y3, trav);
		animator.SetTrigger("talk2");
	}

	private void t1y3() {
		say("They hand you the rest of the money.", t1y4);
		player.GetComponent<Inventory>().moneys += 500;
	}

	private void t1y4() {
		say("Well, I tried to warn you and I got my money. Our business is done.", t1y5, you);
	}

	private void t1y5() {
		say("That's all there is to this game, I hope you enjoyed it! You can keep farming and stuff if you want to, but at some point you'll have to return to reality.", close);
		GetComponent<Collider>().enabled = false;
		Dialogue.dialogue.setObjective("Thanks for playing!");
		animator.gameObject.SetActive(false);
		close1.onClick.AddListener(leave);
		close2.onClick.AddListener(leave);
		close3.onClick.AddListener(leave);
	}

	private void t1n0() {
		say("Do you have my potions?", t1n1, trav);
	}

	private void t1n1() {
		say("Not yet.", t1n2, you);
	}

	private void t1n2() {
		say("I'll wait.", close, trav);
		animator.SetTrigger("talk");
	}

	private void Awake() {
		animator.gameObject.SetActive(false);
	}

	private void LateUpdate() {
		bubble.position = Camera.main.WorldToScreenPoint(transform.position + bubbleOffset);
	}

	private void close() {
		Dialogue.dialogue.hide();
		player.setUIActive(false);
		bubble.GetComponent<Image>().enabled = GetComponent<Collider>().enabled;
		animator.gameObject.SetActive(false);
	}

	private void say(string line, UnityAction action, AudioHandler speaker = null) {
		Dialogue.dialogue.say(line, action, speaker);
	}

	private void leave() {
		close1.onClick.RemoveListener(leave);
		close2.onClick.RemoveListener(leave);
		close3.onClick.RemoveListener(leave);
		transform.position = position2.position;
	}
}
