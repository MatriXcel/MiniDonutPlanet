using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollFix : MonoBehaviour {


    public Scrollbar sb;

	// Use this for initialization
	void Start () {
        sb.value = 1;
        sb.size = 0;
	}
	
	// Update is called once per frame
	void Update () {
        sb.size = 0;
    }
}
