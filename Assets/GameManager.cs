using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEngine;

public class GameManager : MonoBehaviour {


    [SerializeField]
    HashSet<GameObject> selection = new HashSet<GameObject>();

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.


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

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        Debug.Log("GameManager Created");

    }



    internal void AddSelection(GameObject gameObject)
    {
        if (selection.Contains(gameObject))
            selection.Remove(gameObject);
        else
            selection.Add(gameObject);
    }

    internal void ClickOnMap(Vector3 point, Vector2d currentLatitudeLongitude)
    {
       foreach(var go in this.selection)
        {
            MonoBehaviour[] list =go.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is IMovable)
                {
                    IMovable movable = (IMovable)mb;
                    movable.Move(point);
                    
                }
            }
        }
    }
}
