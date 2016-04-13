using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour
{

	public PlayerListDisplay playerListDisplay;
	public UnitListDisplay playerFeetDisplay;
	public UnitListDisplay buildableUnitsDisplay;

	private Player selectedPlayer;


	void Awake ()
	{
		playerListDisplay.onListItemClick += OnPlayerSelect;
		buildableUnitsDisplay.onListItemClick += AddUnittoPlayerFleet;
	
	}

	void OnDestroy ()
	{
		playerListDisplay.onListItemClick -= OnPlayerSelect;
		buildableUnitsDisplay.onListItemClick -= AddUnittoPlayerFleet;

	}

	void OnPlayerSelect (Player _Player)
	{
		selectedPlayer = _Player;
		buildableUnitsDisplay.Prime (_Player.getFaction ().FactionUnits);
		playerFeetDisplay.Prime (selectedPlayer.fleet);
	}

	void AddUnittoPlayerFleet (Unit _unit)
	{
		selectedPlayer.fleet.Add (_unit);
		playerFeetDisplay.Prime (selectedPlayer.fleet);

	}

	public void addPlayer ()
	{
		Game.Manager.addPlayer ();
		playerListDisplay.Prime (Game.Manager.Players);
	}
}
