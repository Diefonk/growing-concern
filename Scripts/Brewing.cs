using UnityEngine;
using UnityEngine.UI;

public class Brewing : Clickable {
	public Player player;
	public Inventory inventory;
	public CanvasGroup selection;
	public Button selectionClose;
	public Text health;
	public Text mana;
	public Text strength;
	public Text speed;
	public Text sleep;
	public Text hate;
	public Text love;
	public Text strongest;
	public AudioHandler brewAudio;
	private bool doTutorial = true;
	private bool madePotion = false;

	private void Awake() {
		health.GetComponentInParent<Button>().onClick.AddListener(() => brew(0));
		mana.GetComponentInParent<Button>().onClick.AddListener(() => brew(1));
		strength.GetComponentInParent<Button>().onClick.AddListener(() => brew(2));
		speed.GetComponentInParent<Button>().onClick.AddListener(() => brew(3));
		sleep.GetComponentInParent<Button>().onClick.AddListener(() => brew(4));
		hate.GetComponentInParent<Button>().onClick.AddListener(() => brew(5));
		love.GetComponentInParent<Button>().onClick.AddListener(() => brew(6));
		strongest.GetComponentInParent<Button>().onClick.AddListener(() => brew(7));
		selectionClose.onClick.AddListener(closeSelection);
	}

	public override void click() {
		player.setUIActive(true);
		player.setDestination(transform.position);
		player.setArrivalAction(openSelection);
	}

	private void openSelection() {
		updateButtons();
		selection.alpha = 1.0f;
		selection.interactable = true;
		selection.blocksRaycasts = true;
	}

	private void closeSelection() {
		selection.alpha = 0.0f;
		selection.interactable = false;
		selection.blocksRaycasts = false;
		player.setUIActive(false);
		if (doTutorial && madePotion) {
			player.setUIActive(true);
			Dialogue.dialogue.say("You can go in to the town and sell your potions. You'll find the town if you follow the road upwards.", closeTutorial);
			Dialogue.dialogue.setObjective("Current objective: Sell a potion");
			Dialogue.skipClick = true;
			doTutorial = false;
		}
	}

	private void brew(int type) {
		switch (type) {
			case 0:
				inventory.health++;
				inventory.concern--;
				break;
			case 1:
				inventory.mana++;
				inventory.concern--;
				inventory.interest--;
				break;
			case 2:
				inventory.strength++;
				inventory.concern--;
				inventory.restless--;
				break;
			case 3:
				inventory.speed++;
				inventory.interest--;
				inventory.restless--;
				break;
			case 4:
				inventory.sleep++;
				inventory.concern--;
				inventory.tired--;
				break;
			case 5:
				inventory.hate++;
				inventory.pain--;
				inventory.resentment--;
				break;
			case 6:
				inventory.love++;
				inventory.pain--;
				inventory.admiration--;
				break;
			case 7:
				inventory.strongest++;
				inventory.concern--;
				inventory.interest--;
				inventory.restless--;
				inventory.tired--;
				inventory.pain--;
				inventory.resentment--;
				inventory.admiration--;
				inventory.threat--;
				break;
			default: break;
		}
		brewAudio.play();
		updateButtons();
		madePotion = true;
	}

	private void updateButtons() {
		health.text = "[" + inventory.health + "]";
		health.GetComponentInParent<Button>().interactable = inventory.concern > 0;
		mana.text = "[" + inventory.mana + "]";
		mana.GetComponentInParent<Button>().interactable = inventory.concern > 0 &&
			inventory.interest > 0;
		strength.text = "[" + inventory.strength + "]";
		strength.GetComponentInParent<Button>().interactable = inventory.concern > 0 &&
			inventory.restless > 0;
		speed.text = "[" + inventory.speed + "]";
		speed.GetComponentInParent<Button>().interactable = inventory.interest > 0 &&
			inventory.restless > 0;
		sleep.text = "[" + inventory.sleep + "]";
		sleep.GetComponentInParent<Button>().interactable = inventory.concern > 0 &&
			inventory.tired > 0;
		hate.text = "[" + inventory.hate + "]";
		hate.GetComponentInParent<Button>().interactable = inventory.pain > 0 &&
			inventory.resentment > 0;
		love.text = "[" + inventory.love + "]";
		love.GetComponentInParent<Button>().interactable = inventory.pain > 0 &&
			inventory.admiration > 0;
		strongest.text = "[" + inventory.strongest + "]";
		strongest.GetComponentInParent<Button>().interactable = inventory.concern > 0 &&
			inventory.interest > 0 && inventory.restless > 0 && inventory.tired > 0 &&
			inventory.pain > 0 && inventory.resentment > 0 && inventory.admiration > 0 &&
			inventory.threat > 0;
	}

	private void closeTutorial() {
		Dialogue.dialogue.hide();
		player.setUIActive(false);
	}
}
