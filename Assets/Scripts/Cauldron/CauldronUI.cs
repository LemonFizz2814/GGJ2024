using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CauldronUI : MonoBehaviour
{
    [Header("Recipe Properties")]
    public List<Image> RecipeItems;
    public GameObject successText;

    public CauldronEvents Events;

    private void Start()
    {
        successText.SetActive(false);
    }

    public void UpdateUI(int _numCorrectItemsInRecipe, bool _solved)
    {
        StopAllCoroutines();

        // Set Everything White
        for (int i = 0; i < RecipeItems.Count; i++)
        {
            //Debug.Log($"Green {i}: {(i < Events.GetNoCorrectItemsInRecipe())}, {Events.GetNoCorrectItemsInRecipe()}");
            RecipeItems[i].color = (i < _numCorrectItemsInRecipe) ? Color.green : Color.red;
        }

        if (!_solved)
        {
            StartCoroutine(SetBackToNormal());
        }
        else
        {
            successText.SetActive(true);
        }
    }

    IEnumerator SetBackToNormal()
    {
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < RecipeItems.Count; i++)
        {
            if (RecipeItems[i].color != Color.grey)
            {
                RecipeItems[i].color = Color.white;
            }
        }
    }

    public void PutObjectIn(int _index)
    {
        RecipeItems[_index].color = Color.grey;
    }
}
