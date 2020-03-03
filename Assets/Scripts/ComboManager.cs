using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ComboManager : MonoBehaviour
{

    public static ComboManager Instance { get; private set; }

    List<FoodType> currentCombo; //# this list contains the current acquired foods (the plates, if you will)
    public List<ComboType> allCombos; //# a list of all available combos in the game (currently unpopulated; and there is perhaps a better way to do this)

    public delegate void ComboUpdated(ComboType _currentCombo);
    public event ComboUpdated onComboUpdate;


    bool hasCompleted;


    void Awake()
    {
        Instance = this;
        currentCombo = new List<FoodType>();
    }

    bool isComplete(ComboType combo)
    {
        if (currentCombo.Count != combo.foods.Length)
            return false;

        foreach (FoodType food in combo.foods)
        {
            if (!isInCurrentCombo(food)) return false; 
        }
        return true;
    }

    bool isComboComplete(out ComboType completedCombo) //# checks whether any combo is complete
    {
        
        foreach (ComboType combo in allCombos)
        {
            if (isComplete(combo))
            {
                completedCombo = combo;
                return true;
            };
            
        }

        completedCombo = null;
        return false;
    }

    bool isInCurrentCombo(FoodType food)
    {
        return currentCombo.Contains(food);
    }

    bool isComboRuined() //# checks whether the current combo has been ruined...that is, if no combo is possible any longer, because the current combo is not a subset of any existing combos
    {

        foreach (ComboType combo in allCombos)
        {

            if (combo.foods.Intersect(currentCombo).Count() == currentCombo.Count()) //# a [perhaps convoluted...] way to check whether CurrentCombo is a subset of combo.Foods
            {
                return false; //# given CurrentCombo is a subset of any existing combo, then the combo is not ruined
            }
        }
        return true; //# otherwise, it is ruined
    }

    void completeCombo(ComboType combo) //# completes a combo, clearing the plates (if it's nonpartial) and adding the score
    {
        GameManager.Instance.addScore(combo.score);


        Debug.Log(string.Format("You have completed the {0} combo", combo.entityName));

        if (!combo.isPartial) //# as long as it's not a partial combo, clears the plates by removing everything from CurrentCombo
            hasCompleted = true;
        //# insert some kind of communication with player
    }

    void ruinCombo() //# ruins the current combo, clearing the plates (and forfeiting the score)
    {
        currentCombo.Clear(); //# clears the plates by removing everything from CurrentCombo
    }

    /* some additional code should be added to completeCombo() and ruinCombo() (above) to communicate with the player.
     * for example, the name of the combo coming up in big letters once it's achieved. 
     * or some other notification, once a combo has been ruined. */

    void printCombo()
    {
        string log = "";

        currentCombo.ForEach(food => log += food.entityName + " ");
        Debug.Log(log);
    }

    public void updateCombo(FoodType food) //# this is the function that is actually called when a ComboFood is picked up. it adds to the current Combo, and also takes the opportunity to check if a combo is complete or ruined and call the appropriate funciton.
    {
        if(hasCompleted)
        {
            ruinCombo();
            hasCompleted = false;
        }

        if (!isInCurrentCombo(food)) //# add the ComboFood to the plates, only if it isn't already there
        {
            currentCombo.Add(food);

            ComboType combo;
            if (isComboComplete(out combo)) //# checks if a combo is complete, and completes it if it is
            {
                completeCombo(combo);
            }
            else if (isComboRuined()) //# check if a combo is ruined, and ruins it if it is; and starts a fresh combo with the food that was just picked up
            {
                ruinCombo();
                currentCombo.Add(food);
            }
        }
        else
        {
            ruinCombo();
            currentCombo.Add(food);
        }

        if(onComboUpdate != null)
        //signal onComboUpdate so that the plates can be updated
            onComboUpdate(new ComboType(currentCombo.ToArray())); 

        printCombo();
    }
}
                                                                                     
