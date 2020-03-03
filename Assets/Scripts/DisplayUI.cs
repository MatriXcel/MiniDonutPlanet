using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayUI : MonoBehaviour {

    public Text scoreText;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "SCORE : " + GameManager.Instance.CurrScore.ToString();
	}






}
