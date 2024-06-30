using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour
{
    [SerializeField] internal CardBoost cardBoost;

    [SerializeField] internal Image cardImage;
    [SerializeField] internal Image levelTextBG; 
    [SerializeField] internal TMP_Text levelText;
    [SerializeField] internal Image levelProgressBar;
    [SerializeField] internal TMP_Text progressText;

    [Header("Level Data Var")]
    [SerializeField] internal float level;
    [SerializeField] internal float levelProgress;
    //[SerializeField] internal int currentProgress;

    private void Start()
    {
        levelText.text = level.ToString();
        progressText.text = $"{levelProgress}/3";
        levelProgressBar.fillAmount = levelProgress / 3;
    }

    private void OnCardChange()
    {
        /*if (cardBoost.currentCard == null) //if player gets a new type of card
        {

            //Assign new card to scriptable object
            cardBoost.currentCard = this;
            cardImage.sprite = cardBoost.cardImage;

            //Assign card level data
            level = cardData.level;
            levelProgress = level + 1; //the number of card needed to progress to the next level
            currentProgress = cardData.progress;

            //Assign card level  data UI
            levelText.text = level.ToString();
            levelProgressBar.fillAmount = (float)currentProgress / levelProgress;
            progressText.text = currentProgress.ToString() + "/" + levelProgress.ToString();

            //Assign card color based on rarity 
            RarityColor();

            //Apply boost to the game
            cardBoost.Apply();
        }
        else //if player get a duplicate card
        {
            //Load original card level data
            var originalCard = cardBoost.currentCard;

            originalCard.currentProgress++;
            if(originalCard.currentProgress >= originalCard.levelProgress)
            {
                //remove original boost of the original card from the previous level
                originalCard.cardBoost.RemoveBoost();

                //Update card level data
                originalCard.level++;
                originalCard.levelProgress++;
                originalCard.currentProgress = 0;

                //Update card level data UI
                originalCard.levelText.text = originalCard.level.ToString();
                originalCard.levelProgressBar.fillAmount = (float)originalCard.currentProgress / originalCard.levelProgress;
                originalCard.progressText.text = originalCard.currentProgress.ToString() + "/" + originalCard.levelProgress.ToString();

                //Reapply card boost with the new level data
                originalCard.cardBoost.Apply();
            }
            else
            {
                //if card didn't level up, only update data UI
                originalCard.levelProgressBar.fillAmount = (float)originalCard.currentProgress / originalCard.levelProgress;
                originalCard.progressText.text = originalCard.currentProgress.ToString() + "/" + originalCard.levelProgress.ToString();
            }

            //destroy duplicated card
            DestroyImmediate(this.gameObject);
        }*/
        
    }
    private void RarityColor()
    {
        switch (cardBoost.rarity)
        {
            case EnumData.Rarity.Common:
                levelTextBG.color = Color.gray;
                break;
            case EnumData.Rarity.Rare:
                levelTextBG.color = Color.blue;
                break;
            case EnumData.Rarity.Epic:
                levelTextBG.color = new Color32(143, 0, 254, 1);
                break;
            case EnumData.Rarity.Legendary:
                levelTextBG.color = Color.yellow;
                break;
        }
    }

#if UNITY_EDITOR
    private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

    private void _OnValidate()
    {
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;
        OnCardChange();
    }
#endif
}
