using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Athlete : Clickable {
	public Player player;
	public RectTransform bubble;
	public Transform healer;
	public Transform healerPosition;
	public Transform god;
	public Transform godPosition;
	public Image godBubble;
	public Animator animator;
	public CanvasGroup choice;
	private Vector3 bubbleOffset = new Vector3(0.0f, 2.0f, 0.0f);
	private int state = 0;
	public AudioHandler stranger;
	public AudioHandler athlete;
	public AudioHandler you;

	public override void click() {
		player.setUIActive(true);
		player.setDestination(transform.position);
		bubble.GetComponent<Image>().enabled = false;
		switch (state) {
			case 0:
				player.setArrivalAction(a00);
				break;
			case 1:
				player.setArrivalAction(a10);
				break;
			case 2:
				player.setArrivalAction(a20);
				break;
			default: break;
		}
	}

	private void a00() {
		healer.position = healerPosition.position;
		say("There's a stranger standing outside your cottage, looking like they're waiting for someone. You wonder what they want.", a01);
	}

	private void a01() {
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
		say("Hello? Can I help you?", a02, you);
	}

	private void a02() {
		say("Oh, hi there! I heard the witch living here makes the best potions around these parts.", a03, stranger);
		animator.SetTrigger("talk2");
	}

	private void a03() {
		say("Have you seen them lately?", a04, stranger);
		animator.SetTrigger("talk");
	}

	private void a04() {
		say("No, not lately, seeing as I don't own a mirror.", a05, you);
	}

	private void a05() {
		say("What?", a06, stranger);
		animator.SetTrigger("talk");
	}

	private void a06() {
		say("...", a07, you);
	}

	private void a07() {
		say("It's me. I'm the witch living here.", a08, you);
	}

	private void a08() {
		say("...", a09, stranger);
	}

	private void a09() {
		say("You don't look like a witch.", a010, stranger);
		animator.SetTrigger("talk");
	}

	private void a010() {
		say("And yet I am one.", a011, you);
	}

	private void a011() {
		say("Shouldn't you at least have a pointy hat or something?", a012, stranger);
		animator.SetTrigger("talk2");
	}

	private void a012() {
		say("I am not defined by my appearance!", a013, you);
	}

	private void a013() {
		say("...", a014, stranger);
	}

	private void a014() {
		say("...", a015, you);
	}

	private void a015() {
		say("Did you want something?", a016, you);
	}

	private void a016() {
		say("Ah, yes. I was hoping you could sell me a speed potion.", a017, stranger);
		animator.SetTrigger("talk2");
	}

	private void a017() {
		say("Can't you buy one from the potion shop in town? Distribution isn't really my job.", a018, you);
	}

	private void a018() {
		say("Well, um, they won't sell me one.", a019, stranger);
		animator.SetTrigger("talk");
	}

	private void a019() {
		say("... Why not?", a020, you);
	}

	private void a020() {
		say("Um... you see... it may be the case... that I'm a... professional athlete...", a021, stranger);
		animator.SetTrigger("talk");
	}

	private void a021() {
		say("And they didn't want to be part of you doping yourself?", a022, you);
	}

	private void a022() {
		say("Exactly!", a023, athlete);
		animator.SetTrigger("talk2");
	}

	private void a023() {
		say("Yea, me neither.", a024, you);
	}

	private void a024() {
		say("Please! I'll pay you 20 moneys for it!", a025, athlete);
		animator.SetTrigger("pray");
	}

	private void a025() {
		Dialogue.dialogue.hide();
		choice.alpha = 1.0f;
		choice.interactable = true;
		choice.blocksRaycasts = true;
	}

	public void a0y25() {
		choice.alpha = 0.0f;
		choice.interactable = false;
		choice.blocksRaycasts = false;
		say("Ugh, fine...", a0y26, you);
		Dialogue.skipClick = true;
	}

	private void a0y26() {
		say("Wait here!", a0y27, you);
	}

	private void a0y27() {
		say("Thank you so much!", close, athlete);
		animator.SetTrigger("pray");
		text = "Talk to athlete";
		Dialogue.dialogue.setObjective("Current objective: Brew a speed potion for the athlete");
		state = 1;
	}

	public void a0n25() {
		choice.alpha = 0.0f;
		choice.interactable = false;
		choice.blocksRaycasts = false;
		say("No! You have to compete on the same terms as everyone else!", a0n26, you);
		Dialogue.skipClick = true;
	}

	private void a0n26() {
		say("You don't understand, I wouldn't need it normally.", a0n27, athlete);
		animator.SetTrigger("talk");
	}

	private void a0n27() {
		say("But the days before big competitions I get really nervous, and I'm having trouble trying to sleep.", a0n28, athlete);
		animator.SetTrigger("talk2");
	}

	private void a0n28() {
		say("I'm counting sheep, but running out.", a0n29, athlete);
		animator.SetTrigger("talk");
	}

	private void a0n29() {
		say("Well, I can get you a sleep potion instead.", a0n30, you);
	}

	private void a0n30() {
		say("Oh, thank you!", close, athlete);
		animator.SetTrigger("talk");
		text = "Talk to athlete";
		Dialogue.dialogue.setObjective("Current objective: Brew a sleep potion for the athlete");
		state = 2;
	}

	private void a10() {
		if (player.GetComponent<Inventory>().speed >= 1) {
			a1y0();
		} else {
			a1n0();
		}
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void a1y0() {
		say("Here's your speed potion.", a1y1, you);
		player.GetComponent<Inventory>().speed--;
	}

	private void a1y1() {
		say("Thanks again!", a1y2, athlete);
		animator.SetTrigger("talk2");
	}

	private void a1y2() {
		say("They hand you 20 moneys.", a1y3);
		player.GetComponent<Inventory>().moneys += 20;
	}

	private void a1y3() {
		say("I better not get in trouble for this.", close, you);
		GetComponent<Collider>().enabled = false;
		Dialogue.dialogue.setObjective("Brew some potions");
		god.position = godPosition.position;
		godBubble.enabled = true;
	}

	private void a1n0() {
		say("Do you have my speed potion?", a1n1, athlete);
	}

	private void a1n1() {
		say("I'll get it to you when I have time.", close, you);
	}

	private void a20() {
		if (player.GetComponent<Inventory>().sleep >= 1) {
			a2y0();
		} else {
			a2n0();
		}
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void a2y0() {
		say("Here's your sleep potion.", a2y1, you);
		player.GetComponent<Inventory>().sleep--;
	}

	private void a2y1() {
		say("Thanks again!", a2y2, athlete);
		animator.SetTrigger("talk2");
	}

	private void a2y2() {
		say("They hand you 20 moneys.", a2y3);
		player.GetComponent<Inventory>().moneys += 20;
	}

	private void a2y3() {
		say("Much better than cheating, isn't it?", close, you);
		GetComponent<Collider>().enabled = false;
		Dialogue.dialogue.setObjective("Brew some potions");
		god.position = godPosition.position;
		godBubble.enabled = true;
	}

	private void a2n0() {
		say("Do you have my sleep potion?", a1n1, athlete);
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
}
