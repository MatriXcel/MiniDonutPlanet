using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// reponsible for filling the bar when the player
/// eats a food
/// </summary>
/// 
public class FillBar : MonoBehaviour {

    public float fillSpeed; //how fast it should fill
    public float drainSpeed; //how fast it should drain
    
    [Range(0, 1)]
    public float fillMargin; //how much the fill should jump by each time

    Image bar;


    float currFillAmt, newTargetFill, t; 

    Queue<IEnumerator> fillRequestQueue; //a queue of fill requests

    public delegate void OnBarComplete();
    public event OnBarComplete onBarCompleted; //other classes can subscribe to the onBarCompleted event

    public delegate void OnBarDrained(); 
    public event OnBarDrained onBarDrained; //other classes can subscribe to the OnBarDrained event

    void Start () {
        bar = GetComponent<Image>();
        fillRequestQueue = new Queue<IEnumerator>();

        StartCoroutine(processFillRequests());

    }

	
    public void fill()
    {
        fillRequestQueue.Enqueue(fillBar()); //queue a new reuqest
    }

    IEnumerator processFillRequests()
    {
        while(true) 
        {
            while(fillRequestQueue.Count > 0)
            {
                currFillAmt = bar.fillAmount;

                //the target fill is the fill amount the bar is trying to reach
                newTargetFill = bar.fillAmount + fillMargin;

                yield return StartCoroutine(fillRequestQueue.Dequeue());
                
                /*
                 if the bar is completely filled then signal the onBarComplete() event
                 */
                if(bar.fillAmount == 1)
                    onBarCompleted();
            }
            
            //drain the bar continuously
            bar.fillAmount -= (Time.deltaTime * drainSpeed);

            //once the bar is completely drained signal the onBarDrained() event
            if (bar.fillAmount == 0)
                onBarDrained(); 

            yield return null;
        }
    }

    IEnumerator fillBar()
    {
        while (true)
        {
            t += (Time.deltaTime * fillSpeed) - (Time.deltaTime * drainSpeed);
            float lerpVal = Mathf.Lerp(currFillAmt, newTargetFill, t); //lerp the fill

            bar.fillAmount = lerpVal; //set current fillAmount to the lerped fill

            if (t >= 1)
            {
                t = 0;
                yield break;
            }

            yield return null;
        } 
    }


}
