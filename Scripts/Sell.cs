using UnityEngine;
using UnityEngine.UI;

public class Sell : Clickable {
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
	public Transform merchant;
	public AudioHandler moneyAudio;
	private bool doTutorial = true;
	private bool soldPotion = false;

	private void Awake() {
		health.GetComponentInParent<Button>().onClick.AddListener(() => sell(0));
		mana.GetComponentInParent<Button>().onClick.AddListener(() => sell(1));
		strength.GetComponentInParent<Button>().onClick.AddListener(() => sell(2));
		speed.GetComponentInParent<Button>().onClick.AddListener(() => sell(3));
		sleep.GetComponentInParent<Button>().onClick.AddListener(() => sell(4));
		hate.GetComponentInParent<Button>().onClick.AddListener(() => sell(5));
		love.GetComponentInParent<Button>().onClick.AddListener(() => sell(6));
		strongest.GetComponentInParent<Button>().onClick.AddListener(() => sell(7));
		selectionClose.onClick.AddListener(closeSelection);
	}

	public override void click() {
		player.setUIActive(true);
		player.setDestination(merchant.position);
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
		if (doTutorial && soldPotion) {
			player.setUIActive(true);
			Dialogue.dialogue.say("Now that you have some money you can buy new seeds to plant. You'll find the seed merchant slightly farther into town.", closeTutorial);
			Dialogue.dialogue.setObjective("Current objective: Buy seeds");
			Dialogue.skipClick = true;
			doTutorial = false;
		}
	}

	private void sell(int type) {
		switch (type) {
			case 0:
				inventory.health--;
				inventory.moneys += 2;
				break;
			case 1:
				inventory.mana--;
				inventory.moneys += 4;
				break;
			case 2:
				inventory.strength--;
				inventory.moneys += 9;
				break;
			case 3:
				inventory.speed--;
				inventory.moneys += 9;
				break;
			case 4:
				inventory.sleep--;
				inventory.moneys += 12;
				break;
			case 5:
				inventory.hate--;
				inventory.moneys += 24;
				break;
			case 6:
				inventory.love--;
				inventory.moneys += 30;
				break;
			case 7:
				inventory.strongest--;
				inventory.moneys += 80;
				break;
			default: break;
		}
		moneyAudio.play();
		updateButtons();
		soldPotion = true;
	}

	private void updateButtons() {
		health.text = "[" + inventory.health + "]";
		health.GetComponentInParent<Button>().interactable = inventory.health > 0;
		mana.text = "[" + inventory.mana + "]";
		mana.GetComponentInParent<Button>().interactable = inventory.mana > 0;
		strength.text = "[" + inventory.strength + "]";
		strength.GetComponentInParent<Button>().interactable = inventory.strength > 0;
		speed.text = "[" + inventory.speed + "]";
		speed.GetComponentInParent<Button>().interactable = inventory.speed > 0;
		sleep.text = "[" + inventory.sleep + "]";
		sleep.GetComponentInParent<Button>().interactable = inventory.sleep > 0;
		hate.text = "[" + inventory.hate + "]";
		hate.GetComponentInParent<Button>().interactable = inventory.hate > 0;
		love.text = "[" + inventory.love + "]";
		love.GetComponentInParent<Button>().interactable = inventory.love > 0;
		strongest.text = "[" + inventory.strongest + "]";
		strongest.GetComponentInParent<Button>().interactable = inventory.strongest > 0;
	}

	private void closeTutorial() {
		Dialogue.dialogue.hide();
		player.setUIActive(false);
	}
}
