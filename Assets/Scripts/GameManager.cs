using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEngine;
using Mapbox.Unity.Map;
using System.Linq;

public class GameManager : MonoBehaviour {

    public string currentPlayer = "Jonas";//TODO: set real player 
	public HashSet<EntityData> unitSelection = new HashSet<EntityData>();
	public Dictionary<string, HashSet<EntityData>> playerUnits = new Dictionary<string, HashSet<EntityData>>();

    public MouseManager mouseManager = new MouseManager();

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public HashSet<Vector2d> buildingSelection = new HashSet<Vector2d>();


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
  
        Debug.Log("GameManager Created");

    }

    internal void SpawnUnit(Vector3 point)
    {
        throw new NotImplementedException();
    }

	public bool IsUnitSelected (EntityData gameObject)
	{
		return unitSelection.Contains(gameObject);
	}

     void Update()
    {
        mouseManager.CheckInput();
    }


	internal void AddSelection(EntityData gameObject)
    {
        if (unitSelection.Contains(gameObject))
            unitSelection.Remove(gameObject);
        else
            unitSelection.Add(gameObject);
    }

	internal void ClearUnitSelection()
	{
		unitSelection.Clear();
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

	internal void AddUnit(string player, EntityData gameObject)
    {
        if (!playerUnits.ContainsKey(player))
        {
			playerUnits[player] = new HashSet<EntityData>();
        }
        playerUnits[player].Add(gameObject);

    }

	internal HashSet<EntityData> GetPlayerUnits()
    {
        if (playerUnits.ContainsKey(currentPlayer))
        {
            return playerUnits[currentPlayer];
        }
        else
        {
			return new HashSet<EntityData>();
        }
    }
}
