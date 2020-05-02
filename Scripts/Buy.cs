using UnityEngine;
using UnityEngine.UI;

public class Buy : Clickable {
	public Player player;
	public Inventory inventory;
	public CanvasGroup selection;
	public Button selectionClose;
	public Text concern;
	public Text interest;
	public Text restless;
	public Text tired;
	public Text pain;
	public Text resentment;
	public Text admiration;
	public Text threat;
	public Transform merchant;
	public Transform healer;
	public Transform healerPosition;
	public Image healerBubble;
	public AudioHandler moneyAudio;
	private bool doTutorial = true;
	private bool boughtSeed = false;

	private void Awake() {
		concern.GetComponentInParent<Button>().onClick.AddListener(() => buy(0));
		interest.GetComponentInParent<Button>().onClick.AddListener(() => buy(1));
		restless.GetComponentInParent<Button>().onClick.AddListener(() => buy(2));
		tired.GetComponentInParent<Button>().onClick.AddListener(() => buy(3));
		pain.GetComponentInParent<Button>().onClick.AddListener(() => buy(4));
		resentment.GetComponentInParent<Button>().onClick.AddListener(() => buy(5));
		admiration.GetComponentInParent<Button>().onClick.AddListener(() => buy(6));
		threat.GetComponentInParent<Button>().onClick.AddListener(() => buy(7));
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
		if (doTutorial && boughtSeed) {
			player.setUIActive(true);
			Dialogue.dialogue.say("That's basically all there is to this game, but I think there's someone back at the potion seller who wants to talk to you.", closeTutorial);
			Dialogue.dialogue.setObjective("Current objective: Talk to healer");
			healer.position = healerPosition.position;
			healerBubble.enabled = true;
			Dialogue.skipClick = true;
			doTutorial = false;
		}
	}

	private void buy(int type) {
		switch (type) {
			case 0:
				inventory.concernSeeds++;
				inventory.moneys--;
				break;
			case 1:
				inventory.interestSeeds++;
				inventory.moneys--;
				break;
			case 2:
				inventory.restlessSeeds++;
				inventory.moneys -= 5;
				break;
			case 3:
				inventory.tiredSeeds++;
				inventory.moneys -= 7;
				break;
			case 4:
				inventory.painSeeds++;
				inventory.moneys -= 9;
				break;
			case 5:
				inventory.resentmentSeeds++;
				inventory.moneys -= 10;
				break;
			case 6:
				inventory.admirationSeeds++;
				inventory.moneys -= 12;
				break;
			case 7:
				inventory.threatSeeds++;
				inventory.moneys -= 15;
				break;
			default: break;
		}
		moneyAudio.play();
		updateButtons();
		boughtSeed = true;
	}

	private void updateButtons() {
		concern.text = "[" + inventory.concernSeeds + "]";
		concern.GetComponentInParent<Button>().interactable = inventory.moneys >= 1;
		interest.text = "[" + inventory.interestSeeds + "]";
		interest.GetComponentInParent<Button>().interactable = inventory.moneys >= 1;
		restless.text = "[" + inventory.restlessSeeds + "]";
		restless.GetComponentInParent<Button>().interactable = inventory.moneys >= 5;
		tired.text = "[" + inventory.tiredSeeds + "]";
		tired.GetComponentInParent<Button>().interactable = inventory.moneys >= 7;
		pain.text = "[" + inventory.painSeeds + "]";
		pain.GetComponentInParent<Button>().interactable = inventory.moneys >= 9;
		resentment.text = "[" + inventory.resentmentSeeds + "]";
		resentment.GetComponentInParent<Button>().interactable = inventory.moneys >= 10;
		admiration.text = "[" + inventory.admirationSeeds + "]";
		admiration.GetComponentInParent<Button>().interactable = inventory.moneys >= 12;
		threat.text = "[" + inventory.threatSeeds + "]";
		threat.GetComponentInParent<Button>().interactable = inventory.moneys >= 15;
	}

	private void closeTutorial() {
		Dialogue.dialogue.hide();
		player.setUIActive(false);
	}
}
