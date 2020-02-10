using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeList : MonoBehaviour
{
    public GameObject RecipePrefab;
    public GameObject ListCanvas;
    public GameObject RecipeDesc;
    public GameObject MyRecipeTab;

    public List<RecipePreset> RecipePresets;

    public MenuManager UsedManager;

    private List<GameObject> recipes = new List<GameObject>();

    private RecipeType currentRecipeType = RecipeType.NONE;

    private void Awake()
    {
        foreach (RecipePreset recipe in RecipePresets)
        {
            foreach (NeighborData data in  GameplayController.Instance.NeighborDatas)
            {
                if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel)
                {
                    SetRecipe(recipe);
                }
            }
        }
    }

    public void SetStarters() 
    {
        ClearList();
        if (currentRecipeType == RecipeType.STARTER)
        {
            foreach (RecipePreset recipe in RecipePresets)
            {
                foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
                {
                    if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel)
                    {
                        SetRecipe(recipe);
                    }
                }
            }
            currentRecipeType = RecipeType.NONE;
        }
        else
        { 
            foreach (RecipePreset recipe in RecipePresets)
            {
                foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
                {
                    if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel && recipe.Type == RecipeType.STARTER)
                    {
                        SetRecipe(recipe);
                    }
                }
            }
            currentRecipeType = RecipeType.STARTER;
        }

    }

    public void SetMainCourse()
    {
        ClearList();
        if (currentRecipeType == RecipeType.MAIN_COURSE)
        {
            foreach (RecipePreset recipe in RecipePresets)
            {
                foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
                {
                    if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel)
                    {
                        SetRecipe(recipe);
                    }
                }
            }
            currentRecipeType = RecipeType.NONE;
        }
        else
        { 
            foreach (RecipePreset recipe in RecipePresets)
            {
                foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
                {
                    if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel && recipe.Type == RecipeType.MAIN_COURSE)
                    {
                        SetRecipe(recipe);
                    }
                }
            }
            currentRecipeType = RecipeType.MAIN_COURSE;
        }
    }

    public void SetDessert()
    {
        ClearList();
        if (currentRecipeType == RecipeType.DESSERT)
        {
            foreach (RecipePreset recipe in RecipePresets)
            {
                foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
                {
                    if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel)
                    {
                        SetRecipe(recipe);
                    }
                }
            }
            currentRecipeType = RecipeType.NONE;
        }
        else
        {
            foreach (RecipePreset recipe in RecipePresets)
            {
                foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
                {
                    if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel && recipe.Type == RecipeType.DESSERT)
                    {
                        SetRecipe(recipe);
                    }
                }
            }
            currentRecipeType = RecipeType.DESSERT;
        }

    }

    public void SetFavorites()
    {
        ClearList();
        if (currentRecipeType == RecipeType.FAVOURITE)
        {
            foreach (RecipePreset recipe in RecipePresets)
            {
                foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
                {
                    if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel)
                    {
                        SetRecipe(recipe);
                    }
                }
            }
            currentRecipeType = RecipeType.NONE;
        }
        else
        {
            foreach (RecipePreset recipe in RecipePresets)
            {
                foreach (NeighborData data in GameplayController.Instance.NeighborDatas)
                {
                    if (recipe.GivingNeighbor == data.NeighborEnum && recipe.RequiredLevel <= data.NeighborLevel && GameplayController.Instance.FavoritedRecipes.Contains(recipe.Name))
                    {
                        SetRecipe(recipe);
                    }
                }
            }
            currentRecipeType = RecipeType.FAVOURITE;
        }

    }

    public void ShowRecipeDescription(string Name,List<string> Ingredients , string Desc) 
    {
        MyRecipeTab.SetActive(false);
        RecipeDesc.SetActive(true);
        RecipeDesc.GetComponent<RecipeDescription>().SetUI(Name, Ingredients, Desc);
    }

    public void SetRecipe(RecipePreset SetPreset)
    {
        GameObject recipe = Instantiate(RecipePrefab, ListCanvas.transform);
        recipes.Add(recipe);
        recipe.GetComponent<RecipeObject>().SetPreset(SetPreset, this);
    }

    public void ClearList() 
    {
        for (int i = recipes.Count - 1; i >= 0; i--)
        {
            Destroy(recipes[i]);
        }
        recipes.Clear();
    }

    private void OnDisable()
    {
        MyRecipeTab.SetActive(true);
        RecipeDesc.SetActive(false);
    }
}
