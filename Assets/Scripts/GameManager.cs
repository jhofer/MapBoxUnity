using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEngine;
using Mapbox.Unity.Map;
using System.Linq;

public class GameManager : MonoBehaviour {

	public Guid currentPlayer;//TODO: set real player 
	
	public Dictionary<Guid, HashSet<EntityData>> playerEntities = new Dictionary<Guid, HashSet<EntityData>>();
    public Dictionary<Guid, PlayerData> players = new Dictionary<Guid, PlayerData>();

    public MouseManager mouseManager = new MouseManager();

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public HashSet<Vector2d> buildingSelection = new HashSet<Vector2d>();

    public PlayerData CurrentPlayer { get {
            return this.players[this.currentPlayer];
        }
    }

  public bool PlaceBuilding { get; internal set; }


  //Awake is always called before any Start functions
  void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

       
        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    internal bool IsBuildingSelected(Vector2d location)
    {
      
        return buildingSelection.Contains(location);
    }

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        currentPlayer = Guid.NewGuid();//TODO: set real player 
        Debug.Log("GameManager Created");
        players[currentPlayer] = new PlayerData("Jonas", currentPlayer,100);

    }

    internal void SpawnUnit(Vector3 point)
    {
        throw new NotImplementedException();
    }

	public bool IsUnitSelected (EntityData gameObject)
	{
		return CurrentPlayer.unitSelection.Contains(gameObject);
	}

     void Update()
    {
        mouseManager.CheckInput();
    }


	internal void AddSelection(EntityData gameObject)
    {
        if (CurrentPlayer.unitSelection.Contains(gameObject))
            CurrentPlayer.unitSelection.Remove(gameObject);
        else
            CurrentPlayer.unitSelection.Add(gameObject);
    }

	internal void ClearUnitSelection()
	{
        CurrentPlayer.unitSelection.Clear();
	}
 
    internal void SelectBuildings(HashSet<Vector2d> locations)
    {
       
        if (buildingSelection.Contains(locations.First())){
            foreach (var item in locations.ToList())
            {
                buildingSelection.Remove(item);
            }
        }

        else
        {
            foreach (var item in locations.ToList())
            {
                buildingSelection.Add(item);
            }
        }
          
    }

	internal void AddEntity(EntityData entityData)
    {
        var playerId = entityData.owner;
        if (!playerEntities.ContainsKey(playerId))
        {
            playerEntities[playerId] = new HashSet<EntityData>();
        }
        playerEntities[playerId].Add(entityData);

    }

	internal HashSet<EntityData> GetPlayerEntities()
    {
        if (playerEntities.ContainsKey(currentPlayer))
        {
            return playerEntities[currentPlayer];
        }
        else
        {
			return new HashSet<EntityData>();
        }
    }
}
