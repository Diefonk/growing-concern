using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour {
	public Image background;
	public Text text;
	public Image speakerBackground;
	public Text speakerText;
	private string toSay = "";
	public float charTime;
	private float charCountdown;
	private UnityAction nextAction;
	public Player player;
	public static Dialogue dialogue;
	public Text objective;
	public static bool skipClick = false;
	private AudioHandler currentSpeaker;

	public void say(string line, UnityAction action, AudioHandler speaker = null) {
		background.enabled = true;
		text.enabled = true;
		text.text = "";
		toSay = line;
		charCountdown = 0.0f;
		nextAction = action;
		if (speaker != null) {
			speakerBackground.enabled = true;
			speakerText.enabled = true;
			speakerText.text = speaker.name;
		} else {
			speakerBackground.enabled = false;
			speakerText.enabled = false;
		}
		currentSpeaker = speaker;
	}

	public void hide() {
		background.enabled = false;
		text.enabled = false;
		speakerBackground.enabled = false;
		speakerText.enabled = false;
	}

	public void setObjective(string newObjective) {
		objective.text = newObjective;
	}

	private void Update() {
		if (!background.enabled) {
			return;
		}

		if (toSay.Length > 0) {
			if (Input.GetMouseButtonUp(0) && !player.getMenuActive() && !skipClick) {
				text.text += toSay;
				toSay = "";
				return;
			}
			charCountdown -= Time.deltaTime;
			if (charCountdown <= 0.0f) {
				text.text += toSay[0];
				toSay = toSay.Substring(1);
				charCountdown = charTime;
				if (currentSpeaker) {
					currentSpeaker.play();
				}
			}
		} else if (Input.GetMouseButtonUp(0) && !player.getMenuActive() && !skipClick) {
			nextAction.Invoke();
		}
		skipClick = false;
	}

	private void Awake() {
		dialogue = this;

		player.setUIActive(true);
		say("Welcome to this game about growing plants and brewing potions.\n\n(click to continue)", tutorial2);
		setObjective("Current objective: Click through the introduction");
	}

	private void tutorial2() {
		say("Use the LEFT MOUSE BUTTON to move around and interact with things, and the RIGHT MOUSE BUTTON to view your inventory. Press ESCAPE to quit the game.", tutorial3);
	}

	private void tutorial3() {
		say("To get started, try planting a seed by clicking on a patch of dirt in your garden.", closeTutorial);
		setObjective("Current objective: Grow a plant");
	}

	private void closeTutorial() {
		Dialogue.dialogue.hide();
		player.setUIActive(false);
	}
}
