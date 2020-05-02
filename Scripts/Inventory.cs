using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	public Text moneyText;
	public Text seedText;
	public Text plantText;
	public Text potionText;
	public int moneys;
	public int concern;
	public int concernSeeds;
	public int interest;
	public int interestSeeds;
	public int restless;
	public int restlessSeeds;
	public int tired;
	public int tiredSeeds;
	public int pain;
	public int painSeeds;
	public int resentment;
	public int resentmentSeeds;
	public int admiration;
	public int admirationSeeds;
	public int threat;
	public int threatSeeds;
	public int health;
	public int mana;
	public int strength;
	public int speed;
	public int sleep;
	public int hate;
	public int love;
	public int strongest;

	public void updateMenu() {
		moneyText.text = "Wallet: " + moneys + (moneys == 1 ? " money" : " moneys");
		seedText.text = "Seeds:\n" +
			concernSeeds + " concern\n" +
			interestSeeds + " interest\n" +
			restlessSeeds + " restless\n" +
			tiredSeeds + " tired\n" +
			painSeeds + " pain\n" +
			resentmentSeeds + " resentment\n" +
			admirationSeeds + " admiration\n" +
			threatSeeds + " threat";
		plantText.text = "Plants:\n" +
			concern + " concern\n" +
			interest + " interest\n" +
			restless + " restless\n" +
			tired + " tired\n" +
			pain + " pain\n" +
			resentment + " resentment\n" +
			admiration + " admiration\n" +
			threat + " threat";
		potionText.text = "Potions:\n" +
			health + " health\n" +
			mana + " mana\n" +
			strength + " strength\n" +
			speed + " speed\n" +
			sleep + " sleep\n" +
			hate + " hate\n" +
			love + " love\n" +
			strongest + " strongest";
	}
}
