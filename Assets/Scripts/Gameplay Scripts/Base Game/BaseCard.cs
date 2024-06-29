using Bayat.SaveSystem;
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TF.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Bayat.SaveSystem.Demos.ComplexDataDemoController;

public class BaseCard : DataController
{
    [SerializeField] internal CardBoost cardBoost;

    [SerializeField] Image cardImage;
    [SerializeField] Image levelTextBG; 
    [SerializeField] TMP_Text levelText;
    [SerializeField] Image levelProgressBar;
    [SerializeField] TMP_Text progressText;

    [Header("Level Data Var")]
    [SerializeField] internal int level;
    [SerializeField] private int levelProgress;
    [SerializeField] private int currentProgress;

    [Header("Card Data")]
    [SerializeField] internal CardData defaultCardData = new CardData();
    internal CardData cardData;

    private void OnCardChange()
    {
        if (cardBoost.currentCard == null) //if player gets a new type of card
        {
            //Load default card level data
            Load();

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

            //Save card data
            Save();
        }
        else //if player get a duplicate card
        {
            //Load original card level data
            var originalCard = cardBoost.currentCard;
            originalCard.Load();

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

            //Save card data
            Save(); 
        }
        
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

    public override void Save()
    {
        SaveSystemAPI.SaveAsync(cardBoost.name, cardData);
    }

    public override async void Load()
    {
        if (!await SaveSystemAPI.ExistsAsync(cardBoost.name))
        {
            Debug.LogError("Player data not found");
            Debug.Log("Using default player data instead");
            cardData = defaultCardData;
        }

        cardData = await SaveSystemAPI.LoadAsync<CardData>(cardBoost.name);
        Debug.Log("Player data loaded successfully");
    }

    public override void Delete()
    {
        throw new NotImplementedException();
    }

    [Serializable]
    public class CardData
    {
        public int level;
        public int progress;
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
