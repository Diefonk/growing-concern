using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour {
	public Transform goal;
	public Image hover;
	public Text hoverText;
	public Animator menu;
	public AudioHandler footsteps;
	private NavMeshAgent agent;
	private Camera mainCamera;
	private Vector3 cameraOffset;
	private Quaternion cameraRotation;
	private Animator animator;
	private Clickable oldClickable = null;
	private bool oldWalking = false;
	private bool uiActive = false;
	private bool skipUpdate = false;
	private UnityAction arrivalAction = null;
	private bool menuActive = false;
	private float previousWalkTime = 0.0f;

	private void Awake() {
		agent = GetComponent<NavMeshAgent>();
		mainCamera = GetComponentInChildren<Camera>();
		cameraOffset = mainCamera.transform.localPosition;
		cameraRotation = mainCamera.transform.localRotation;
		animator = GetComponent<Animator>();
		goal.GetComponent<Renderer>().enabled = false;
	}

    private void Update() {
		mainCamera.transform.position = transform.position + cameraOffset;
		mainCamera.transform.rotation = cameraRotation;

		bool walking = Vector3.Distance(agent.destination, transform.position) - 0.01f > agent.stoppingDistance;
		animator.SetBool("walking", walking);
		if (!walking && oldWalking) {
			goal.GetComponent<Renderer>().enabled = false;
			if (arrivalAction != null) {
				arrivalAction.Invoke();
				arrivalAction = null;
			}
			previousWalkTime = 0.0f;
		}
		oldWalking = walking;
		if (walking) {
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			float walkTime = stateInfo.normalizedTime % 1.0f;
			if (stateInfo.IsName("Walking") &&
				((previousWalkTime < 0.36f && walkTime >= 0.36f) ||
				(previousWalkTime < 0.86f && walkTime >= 0.86f))) {
				footsteps.play();
			}
			previousWalkTime = walkTime;
		}

		if (Input.GetMouseButtonUp(1)) {
			menuActive = !menuActive;
			if (menuActive) {
				menu.SetTrigger("show");
				hover.enabled = false;
				hoverText.enabled = false;
				GetComponent<Inventory>().updateMenu();
			} else {
				menu.SetTrigger("hide");
				skipUpdate = true;
				Dialogue.skipClick = true;
			}
			return;
		}

		if (uiActive || menuActive) {
			hover.enabled = false;
			hoverText.enabled = false;
			return;
		}
		if (skipUpdate) {
			skipUpdate = false;
			return;
		}

		Clickable clickable = null;
		bool hoveringGround = false;
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
			clickable = hit.transform.GetComponent<Clickable>();
			if (clickable) {
				hoverText.text = clickable.text;
			} else if (hit.transform.CompareTag("Ground")) {
				hoveringGround = true;
				hoverText.text = "Walk";
			}
		}

		if (oldClickable && oldClickable != clickable) {
			oldClickable.hoverExit();
		}
		if (clickable && oldClickable != clickable) {
			clickable.hoverEnter();
		}
		oldClickable = clickable;
		hover.enabled = hoveringGround || clickable;
		hoverText.enabled = hoveringGround || clickable;

		if (Input.GetMouseButtonUp(0)) {
			if (clickable) {
				clickable.click();
			} else if (hoveringGround) {
				setDestination(hit.point, true);
			}
		}
    }

	public void setDestination(Vector3 destination, bool setGoal = false) {
		agent.destination = destination;
		if (setGoal) {
			goal.position = destination;
			goal.GetComponent<Renderer>().enabled = true;
		} else {
			goal.GetComponent<Renderer>().enabled = false;
		}
		oldWalking = true;
	}

	public void setUIActive(bool active) {
		uiActive = active;
		hover.enabled = false;
		hoverText.enabled = false;
		skipUpdate = true;
	}

	public void setArrivalAction(UnityAction action) {
		arrivalAction = action;
	}

	public void removeOldClickable() {
		oldClickable = null;
	}

	public void hideMenu() {
		menuActive = false;
		menu.SetTrigger("hide");
		skipUpdate = true;
		Dialogue.skipClick = true;
	}

	public bool getMenuActive() {
		return menuActive;
	}
}
