using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public int CurrScore { get; set; }

    //Dictionary of newly unlocked in-game items to add to the spawners in-game
    public Dictionary<string,EntityType> UnlockedItems; 
    
	void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void addScore(int score)
    {
        CurrScore += score;
    }

	void Start () {
		
	}
	
	void Update () {
		
	}
}
