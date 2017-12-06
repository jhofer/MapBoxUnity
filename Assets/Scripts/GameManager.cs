using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEngine;
using Mapbox.Unity.Map;
using System.Linq;

public class GameManager : MonoBehaviour {

    public string currentPlayer = "Jonas";//TODO: set real player 
    public HashSet<GameObject> unitSelection = new HashSet<GameObject>();
    public Dictionary<string, HashSet<IUnit>> playerUnits = new Dictionary<string, HashSet<IUnit>>();

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

     void Update()
    {
        mouseManager.CheckInput();
    }


    internal void AddSelection(GameObject gameObject)
    {
        if (unitSelection.Contains(gameObject))
            unitSelection.Remove(gameObject);
        else
            unitSelection.Add(gameObject);
    }

    internal void SelectBuilding(Vector2d location)
    {
        if (buildingSelection.Contains(location))
            buildingSelection.Remove(location);
        else
            buildingSelection.Add(location);
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

    internal void AddUnit(string player, IUnit gameObject)
    {
        if (!playerUnits.ContainsKey(player))
        {
            playerUnits[player] = new HashSet<IUnit>();
        }
        playerUnits[player].Add(gameObject);

    }

    internal HashSet<IUnit> GetPlayerUnits()
    {
        if (playerUnits.ContainsKey(currentPlayer))
        {
            return playerUnits[currentPlayer];
        }
        else
        {
            return new HashSet<IUnit>();
        }
    }
}
