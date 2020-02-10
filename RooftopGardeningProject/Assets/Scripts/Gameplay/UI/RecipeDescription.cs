using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDescription : MonoBehaviour
{
    public Text Name;
    public Text Description;

    public void SetUI(string SetName, List<string> SetIngredients , string SetDesc) 
    {
        Name.text = SetName;
        StringBuilder descBuilder = new StringBuilder();
        foreach (string ingredients in SetIngredients)
        {
            descBuilder.Append(ingredients);
            descBuilder.Append("/n");
        }
        descBuilder.Append(SetDesc);
        Description.text = descBuilder.ToString();
    }

    public void CloseDescription() 
    {
        gameObject.SetActive(false);
        GetComponentInParent<RecipeList>().MyRecipeTab.SetActive(true);
    }
}
