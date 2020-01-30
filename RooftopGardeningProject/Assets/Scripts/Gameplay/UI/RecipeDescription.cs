using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDescription : MonoBehaviour
{
    public Text Name;
    public Text Description;

    public void SetUI(string SetName, string SetDesc) 
    {
        Name.text = SetName;
        Description.text = SetDesc;
    }

    public void CloseDescription() 
    {
        gameObject.SetActive(false);
        GetComponentInParent<RecipeList>().MyRecipeTab.SetActive(true);
    }
}
