using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Food Entity has FoodType properties such as ScoreValue, on top of that
/// a Food Entity can be picked up
/// </summary>
public class Food : Entity<FoodType>
{
    // Start is called before the first frame update
    
    void pickup()
    {
        GameManager.Instance.addScore(entityType.scoreValue); //add to the score
        FoodSpawner.Instance.returnToPool(this); //put food back in the pool

        ComboManager.Instance.updateCombo(entityType);
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            /*
            call the pickup method, which in turn informs the ComboManager
             */
            this.pickup();

            /*
            fill they player's bar, i decided to make the bar a component of the player
            in case we expand this to a Multiplayer scenario where you have
            multiple players
             */
            other.gameObject.GetComponent<PlayerController>().fillBar.fill();
        }
            
    }
}
