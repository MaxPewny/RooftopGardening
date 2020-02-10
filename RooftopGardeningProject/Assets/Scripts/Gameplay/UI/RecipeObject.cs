using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeObject : MonoBehaviour
{
    public Image IconDisplay;
    public Image VeganIcon;
    public Image NameBackground;
    public Image FavButtonImage;

    public Text NameDisplay;

    public Sprite HighlightedFav;
    public Sprite Fav;

    private RecipePreset preset;
    private RecipeList recipeList;
    private bool isFav;


    public void SetPreset(RecipePreset Preset, RecipeList AssignedList) 
    {
        preset = Preset;
        recipeList = AssignedList;

        IconDisplay.sprite = preset.Icon;
        NameBackground.color = preset.UsedColor;
        NameDisplay.text = preset.Name;
        isFav = GameplayController.Instance.FavoritedRecipes.Contains(preset.Name);

        if (isFav)
        {
            FavButtonImage.sprite = HighlightedFav;
        }
    }

    public void SetFav() 
    {
        if (isFav)
        {
            FavButtonImage.sprite = Fav;
            GameplayController.Instance.FavoritedRecipes.Remove(preset.Name);
            isFav = false;
        }
        else 
        {
            FavButtonImage.sprite = HighlightedFav;
            GameplayController.Instance.FavoritedRecipes.Add(preset.Name);
            isFav = true;
        }
    }

    public void ShowDesc()
    {
        recipeList.ShowRecipeDescription(preset.Name, preset.Ingredients , preset.Description);
    }
}
