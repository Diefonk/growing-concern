using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;

public class FarmPlot : Clickable {
	public Player player;
	public Inventory inventory;
	public CanvasGroup selection;
	public Button selectionClose;
	public Text concernAmount;
	public Text interestAmount;
	public Text restlessAmount;
	public Text tiredAmount;
	public Text painAmount;
	public Text resentmentAmount;
	public Text admirationAmount;
	public Text threatAmount;
	public Renderer hover;
	public ParticleSystem particles;
	public Plant concern;
	public Plant interest;
	public Plant restless;
	public Plant tired;
	public Plant pain;
	public Plant resentment;
	public Plant admiration;
	public Plant threat;
	public AudioHandler dirtAudio;
	private Plant currentPlant = null;
	private bool mature = false;
	private static bool doTutorial = true;
	private static bool doTutorial2 = true;

	private void Awake() {
		hover.enabled = false;
		particles.Stop();
	}

	public override void click() {
		if (!currentPlant) {
			prepareButton(concernAmount, inventory.concernSeeds, concern);
			prepareButton(interestAmount, inventory.interestSeeds, interest);
			prepareButton(restlessAmount, inventory.restlessSeeds, restless);
			prepareButton(tiredAmount, inventory.tiredSeeds, tired);
			prepareButton(painAmount, inventory.painSeeds, pain);
			prepareButton(resentmentAmount, inventory.resentmentSeeds, resentment);
			prepareButton(admirationAmount, inventory.admirationSeeds, admiration);
			prepareButton(threatAmount, inventory.threatSeeds, threat);
			selectionClose.onClick.AddListener(closeSelection);
			selection.alpha = 1.0f;
			selection.interactable = true;
			selection.blocksRaycasts = true;
			player.setUIActive(true);
		} else if (currentPlant && mature) {
			player.setUIActive(true);
			player.setDestination(transform.position);
			player.setArrivalAction(harvest);
			hoverExit();
			player.removeOldClickable();
		}
	}

	public override void hoverEnter() {
		hover.enabled = true;
	}

	public override void hoverExit() {
		hover.enabled = false;
	}

	public void grow(float time, bool day) {
		if (currentPlant && currentPlant.dayPlant == day && !mature) {
			float timeLeft = currentPlant.grow(time);
			if (timeLeft <= 0.0f) {
				mature = true;
				particles.Play();
				text = "Harvest " + currentPlant.name.ToLower();
			} else {
				text = "Growing " + currentPlant.name.ToLower() + " ~ " + Mathf.Floor(timeLeft * 1000.0f) / 1000.0f + (currentPlant.dayPlant ? " days left" : " nights left");
			}
		}
	}

	private void plantSeed(Plant seed) {
		closeSelection();
		player.setUIActive(true);
		player.setDestination(transform.position);
		player.setArrivalAction(() => plant(seed));
		hoverExit();
		player.removeOldClickable();
	}

	private void plant(Plant seed) {
		StartCoroutine(plantCont(seed));
	}

	private IEnumerator plantCont(Plant seed) {
		player.GetComponent<Animator>().SetBool("kneeling", true);
		float timeout = Time.time + 1.0f;
		while (Time.time < timeout) {
			yield return null;
		}
		player.GetComponent<NavMeshAgent>().enabled = false;
		player.GetComponent<Animator>().SetTrigger("plant");
		dirtAudio.play();
		currentPlant = seed;
		mature = false;
		currentPlant.plant();
		text = "Growing " + currentPlant.name.ToLower();
		if (currentPlant == concern) {
			inventory.concernSeeds--;
		} else if (currentPlant == interest) {
			inventory.interestSeeds--;
		} else if (currentPlant == restless) {
			inventory.restlessSeeds--;
		} else if (currentPlant == tired) {
			inventory.tiredSeeds--;
		} else if (currentPlant == pain) {
			inventory.painSeeds--;
		} else if (currentPlant == resentment) {
			inventory.resentmentSeeds--;
		} else if (currentPlant == admiration) {
			inventory.admirationSeeds--;
		} else if (currentPlant == threat) {
			inventory.threatSeeds--;
		}
		inventory.updateMenu();
		timeout = Time.time + 6.0f;
		while (Time.time < timeout) {
			yield return null;
		}
		player.GetComponent<NavMeshAgent>().enabled = true;
		player.GetComponent<Animator>().SetBool("kneeling", false);
		player.setUIActive(false);
		if (doTutorial) {
			player.setUIActive(true);
			Dialogue.dialogue.say("It'll take some time for that to grow, but you can just go and sleep in your bed to avoid waiting. You'll find the bed inside your cottage.", closeTutorial);
			doTutorial = false;
		}
	}

	private void prepareButton(Text text, int amount, Plant seed) {
		text.text = "[" + amount + "]";
		if (amount > 0) {
			text.GetComponentInParent<Button>().interactable = true;
			text.GetComponentInParent<Button>().onClick.AddListener(() => plantSeed(seed));
		} else {
			text.GetComponentInParent<Button>().interactable = false;
		}
	}

	private void closeSelection() {
		concernAmount.GetComponentInParent<Button>().interactable = false;
		concernAmount.GetComponentInParent<Button>().onClick.RemoveAllListeners();
		interestAmount.GetComponentInParent<Button>().interactable = false;
		interestAmount.GetComponentInParent<Button>().onClick.RemoveAllListeners();
		restlessAmount.GetComponentInParent<Button>().interactable = false;
		restlessAmount.GetComponentInParent<Button>().onClick.RemoveAllListeners();
		tiredAmount.GetComponentInParent<Button>().interactable = false;
		tiredAmount.GetComponentInParent<Button>().onClick.RemoveAllListeners();
		painAmount.GetComponentInParent<Button>().interactable = false;
		painAmount.GetComponentInParent<Button>().onClick.RemoveAllListeners();
		resentmentAmount.GetComponentInParent<Button>().interactable = false;
		resentmentAmount.GetComponentInParent<Button>().onClick.RemoveAllListeners();
		admirationAmount.GetComponentInParent<Button>().interactable = false;
		admirationAmount.GetComponentInParent<Button>().onClick.RemoveAllListeners();
		threatAmount.GetComponentInParent<Button>().interactable = false;
		threatAmount.GetComponentInParent<Button>().onClick.RemoveAllListeners();
		selectionClose.onClick.RemoveAllListeners();
		selection.alpha = 0.0f;
		selection.interactable = false;
		selection.blocksRaycasts = false;
		player.setUIActive(false);
	}

	private void harvest() {
		StartCoroutine(harvestCont());
	}

	private IEnumerator harvestCont() {
		player.GetComponent<Animator>().SetBool("kneeling", true);
		float timeout = Time.time + 1.0f;
		while (Time.time < timeout) {
			yield return null;
		}
		player.GetComponent<NavMeshAgent>().enabled = false;
		player.GetComponent<Animator>().SetTrigger("harvest");
		dirtAudio.play();
		timeout = Time.time + 3.0f;
		while (Time.time < timeout) {
			yield return null;
		}
		if (currentPlant == concern) {
			inventory.concern++;
		} else if (currentPlant == interest) {
			inventory.interest++;
		} else if (currentPlant == restless) {
			inventory.restless++;
		} else if (currentPlant == tired) {
			inventory.tired++;
		} else if (currentPlant == pain) {
			inventory.pain++;
		} else if (currentPlant == resentment) {
			inventory.resentment++;
		} else if (currentPlant == admiration) {
			inventory.admiration++;
		} else if (currentPlant == threat) {
			inventory.threat++;
		}
		inventory.updateMenu();
		currentPlant.GetComponent<Renderer>().enabled = false;
		currentPlant = null;
		particles.Stop();
		text = "Plant seed";
		timeout = Time.time + 2.0f;
		while (Time.time < timeout) {
			yield return null;
		}
		player.GetComponent<NavMeshAgent>().enabled = true;
		player.GetComponent<Animator>().SetBool("kneeling", false);
		player.setUIActive(false);
		if (doTutorial2) {
			player.setUIActive(true);
			Dialogue.dialogue.say("Now you can use the plants you've grown to brew potions. You'll find the brewing station inside your cottage.", closeTutorial);
			Dialogue.dialogue.setObjective("Current objective: Brew a potion");
			doTutorial2 = false;
		}
	}

	private void closeTutorial() {
		Dialogue.dialogue.hide();
		player.setUIActive(false);
	}
}
