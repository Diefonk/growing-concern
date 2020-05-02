using UnityEngine;

public abstract class Clickable : MonoBehaviour {
    public string text;

	public virtual void click() {}
	public virtual void hoverEnter() {}
	public virtual void hoverExit() {}
}
