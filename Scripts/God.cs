using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class God : Clickable {
	public Player player;
	public RectTransform bubble;
	public Transform athlete;
	public Transform athletePosition;
	public Transform traveller;
	public Transform travellerPosition;
	public Image travellerBubble;
	public Animator animator;
	public Renderer bottle;
	private Vector3 bubbleOffset = new Vector3(0.0f, 2.0f, 0.0f);
	private int state = 0;
	public AudioHandler god;
	public AudioHandler you;

	public override void click() {
		player.setUIActive(true);
		player.setDestination(transform.position);
		bubble.GetComponent<Image>().enabled = false;
		switch (state) {
			case 0:
				player.setArrivalAction(g00);
				break;
			case 1:
				player.setArrivalAction(g10);
				break;
			default: break;
		}
	}

	private void g00() {
		athlete.position = athletePosition.position;
		say("In front of you is the most beautiful person you've ever seen. They are literally glowing.", g01);
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void g01() {
		say("You're sure they must be a god or something.", g02);
	}

	private void g02() {
		say("slfghraelgbrsljgh", g03, you);
	}

	private void g03() {
		say("Oh no, I don't understand this language!", g05, god);
		animator.SetTrigger("talk");
	}

	private void g05() {
		say("Sorry! I'm just... I... you...", g06, you);
	}

	private void g06() {
		say("You're so beautiful! How are you so beautiful?", g07, you);
	}

	private void g07() {
		say("They blush, which is impressive considering their already rosy complexion.", g08);
	}

	private void g08() {
		say("You think I'm beautiful?", g09, god);
		animator.SetTrigger("talk");
	}

	private void g09() {
		say("Thank you! I've been feeling a bit off ever since...", g010, god);
		animator.SetTrigger("talk2");
	}

	private void g010() {
		say("...", g011, god);
	}

	private void g011() {
		say("...", g012, you);
	}

	private void g012() {
		say("I don't even remember what happened, but I'm sure I must have been transported to a different world or something...", g013, god);
		animator.SetTrigger("talk");
	}

	private void g013() {
		say("To this world, that is.", g014, god);
		animator.SetTrigger("talk2");
	}

	private void g014() {
		say("You're from a different world?", g015, you);
	}

	private void g015() {
		say("That would explain your appearance, I guess.", g016, you);
	}

	private void g016() {
		say("I actually thought you might be a god or something, haha!", g017, you);
	}

	private void g017() {
		say("No, I am a god.", g018, god);
		animator.SetTrigger("talk");
	}

	private void g018() {
		say("And from a different world.", g019, god);
		animator.SetTrigger("talk2");
	}

	private void g019() {
		say("... Huh?", g020, you);
	}

	private void g020() {
		say("I am the god of love.", g021, god);
		animator.SetTrigger("talk2");
	}

	private void g021() {
		say("But like I said, I'm feeling a bit off.", g022, god);
		animator.SetTrigger("talk");
	}

	private void g022() {
		say("Hmm, maybe I can try to find some kind of potion that would make me feel more like myself again.", g023, god);
		animator.SetTrigger("talk");
	}

	private void g023() {
		say("Like what? A love potion?", g024, you);
	}

	private void g024() {
		say("Oooh, yes, that sounds perfect!", g025, god);
		animator.SetTrigger("talk");
	}

	private void g025() {
		say("Do you know where I can find one?", g026, god);
		animator.SetTrigger("talk2");
	}

	private void g026() {
		say("Um, yea, I actually make potions for a living.", g027, you);
	}

	private void g027() {
		say("But I was kidding, a love potion wouldn't have that effect.", g028, you);
	}

	private void g028() {
		say("How do you know what effect it would have on a god?", g029, god);
		animator.SetTrigger("talk2");
	}

	private void g029() {
		say("I... hmm... I guess we can try?", g030, you);
	}

	private void g030() {
		say("Wait here.", close, you);
		text = "Talk to god";
		Dialogue.dialogue.setObjective("Current objective: Brew a love potion for the god");
		state = 1;
	}

	private void g10() {
		if (player.GetComponent<Inventory>().love >= 1) {
			g1y0();
		} else {
			g1n0();
		}
		animator.gameObject.SetActive(true);
		animator.SetBool("idle2", true);
	}

	private void g1y0() {
		say("Here's that love potion.", g1y1, you);
		player.GetComponent<Inventory>().love--;
	}

	private void g1y1() {
		say("I'm not sure this will work, but worst case scenario you'll fall in love with me.", g1y2, you);
	}

	private void g1y2() {
		say("Which... I wouldn't mind that too much...", g1y3, you);
	}

	private void g1y3() {
		say("Haha, thanks!", g1y4, god);
		animator.SetTrigger("talk");
	}

	private void g1y4() {
		say("They take the love potion and drink all of it.", g1y5);
		animator.SetTrigger("drink");
		bottle.enabled = true;
	}

	private void g1y5() {
		say("How do you feel?", g1y6, you);
		bottle.enabled = false;
	}

	private void g1y6() {
		say("Much better! That was just what I needed.", g1y7, god);
		animator.SetTrigger("talk");
	}

	private void g1y7() {
		say("Thank you so much!", g1y8, god);
		animator.SetTrigger("pray");
	}

	private void g1y8() {
		say("Oh no, I don't have any money to pay you.", g1y9, god);
		animator.SetTrigger("talk2");
	}

	private void g1y9() {
		say("Oh, that's fine.", g1y10, you);
	}

	private void g1y10() {
		say("But maybe you want to hang out again some other time?", g1y11, you);
	}

	private void g1y11() {
		say("Only if you want to, of course!", g1y12, you);
	}

	private void g1y12() {
		say("Sure! I do want to try to get back to my own world, but that might take a long time, so I'll probably be around for a while.", g1y13, god);
		animator.SetTrigger("talk");
	}

	private void g1y13() {
		say("Cool, yeah, see you later then!", close, you);
		GetComponent<Collider>().enabled = false;
		Dialogue.dialogue.setObjective("Do some stuff");
		traveller.position = travellerPosition.position;
		travellerBubble.enabled = true;
	}

	private void g1n0() {
		say("I'm still not sure about this whole love potion thing.", g1n1, you);
	}

	private void g1n1() {
		say("It'll be fine!", close, god);
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
}
