using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Healer : Clickable {
	public Player player;
	public RectTransform bubble;
	public Transform stranger;
	public Transform strangerPosition;
	public Image strangerBubble;
	public Animator animator;
	private Vector3 bubbleOffset = new Vector3(0.0f, 2.0f, 0.0f);
	private int state = 0;
	public AudioHandler healer;
	public AudioHandler you;

	public override void click() {
		player.setUIActive(true);
		player.setDestination(transform.position);
		bubble.GetComponent<Image>().enabled = false;
		switch (state) {
			case 0:
				player.setArrivalAction(healer00);
				break;
			case 1:
				player.setArrivalAction(healer10);
				break;
			case 2:
				player.setArrivalAction(healer20);
				break;
			default: break;
		}
	}

	private void Awake() {
		animator.gameObject.SetActive(false);
	}

	private void LateUpdate() {
		bubble.position = Camera.main.WorldToScreenPoint(transform.position + bubbleOffset);
	}

	private void healer00() {
		say("A healer is standing outside the potion shop.", healer01);
	}

	private void healer01() {
		say("Hi! You're the witch making the potions, right?", healer02, healer);
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void healer02() {
		say("Yep, that's me. What can I do for you?", healer03, you);
	}

	private void healer03() {
		say("You may have heard of the new illness spreading lately.", healer04, healer);
		animator.SetTrigger("talk");
	}

	private void healer04() {
		say("I work at the hospital here in town, and we're in need of a lot of health potions right now.", healer05, healer);
		animator.SetTrigger("talk2");
	}

	private void healer05() {
		say("Well, you're standing outside the potion shop, so I'm sure you could manage to get some there.", healer06, you);
	}

	private void healer06() {
		say("They're also running low on their supply.", healer07, healer);
		animator.SetTrigger("talk");
	}

	private void healer07() {
		say("Could you maybe consider increasing your production for a while?", healer08, healer);
		animator.SetTrigger("pray");
	}

	private void healer08() {
		say("I can pay you more than the usual amount to make up for it.", healer09, healer);
		animator.SetTrigger("talk2");
	}

	private void healer09() {
		say("Sure, but I'd need some money right away to get more seeds.", healer010, you);
	}

	private void healer010() {
		say("Of course. Can you make me 10 health potions?", healer011, healer);
		animator.SetTrigger("talk");
	}

	private void healer011() {
		say("I'll give you half the money right now.", healer012, healer);
		animator.SetTrigger("talk2");
	}

	private void healer012() {
		say("The healer gives you 15 moneys.", healer013);
		player.GetComponent<Inventory>().moneys += 15;
	}

	private void healer013() {
		say("Hang tight, I'll get you those potions as soon as possible.", close, you);
		Dialogue.dialogue.setObjective("Current objective: Brew 10 health potions for the healer");
		state = 1;
	}

	private void healer10() {
		if (player.GetComponent<Inventory>().health >= 10) {
			healer1y0();
		} else {
			healer1n0();
		}
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void healer1y0() {
		say("Here are the potions you needed.", healer1y1, you);
		player.GetComponent<Inventory>().health -= 10;
	}

	private void healer1y1() {
		say("Thank you so much!", healer1y2, healer);
		animator.SetTrigger("talk2");
	}

	private void healer1y2() {
		say("They hand you the rest of the money.", healer1y3);
		player.GetComponent<Inventory>().moneys += 15;
	}

	private void healer1y3() {
		say("A couple of local mages have decided to help us with the few healing spells they have.", healer1y4, healer);
		animator.SetTrigger("talk");
	}

	private void healer1y4() {
		say("But they need some mana potions to keep it up.", healer1y5, healer);
		animator.SetTrigger("talk2");
	}

	private void healer1y5() {
		say("Could you get me 10 mana potions? I'll pay you extra for this as well.", healer1y6, healer);
		animator.SetTrigger("pray");
	}

	private void healer1y6() {
		say("No problem, I'll get right on that.", close, you);
		Dialogue.dialogue.setObjective("Current objective: Brew 10 mana potions for the healer");
		state = 2;
	}

	private void healer1n0() {
		say("Do you have the health potions?", healer1n1, healer);
	}

	private void healer1n1() {
		say("Not yet.", healer1n2, you);
	}

	private void healer1n2() {
		say("Please hurry!", close, healer);
		animator.SetTrigger("pray");
	}

	private void healer20() {
		if (player.GetComponent<Inventory>().mana >= 10) {
			healer2y0();
		} else {
			healer2n0();
		}
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void healer2y0() {
		say("Here are those mana potions.", healer2y1, you);
		player.GetComponent<Inventory>().mana -= 10;
	}

	private void healer2y1() {
		say("Thanks again!", healer2y2, healer);
		animator.SetTrigger("pray");
	}

	private void healer2y2() {
		say("You've helped us a lot along the path of defeating this illness.", healer2y3, healer);
		animator.SetTrigger("talk2");
	}

	private void healer2y3() {
		say("They hand you 50 moneys.", healer2y4);
		player.GetComponent<Inventory>().moneys += 50;
	}

	private void healer2y4() {
		say("I'm happy I could help.", close, you);
		Dialogue.dialogue.setObjective("Grow some plants");
		GetComponent<Collider>().enabled = false;
		stranger.position = strangerPosition.position;
		strangerBubble.enabled = true;
	}

	private void healer2n0() {
		say("Do you have the mana potions?", healer1n1, healer);
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
