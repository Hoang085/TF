using System;
using System.Collections;
using System.Collections.Generic;
using TF.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Boosts/AttackBoost")]
public class AtkBoost : CardBoost
{
    public override void Apply()
    {
        if (GameData.Instance != null) RarityBoost();
    }

    public override void RemoveBoost()
    {
        if (GameData.Instance != null) RarityDeboost();
    }

    private void RarityDeboost()
    {
        switch (rarity)
        {
            case EnumData.Rarity.Common:
                GameData.Instance.playerData.totalDamBoost /= (boostMultiplier + (0.05f * (currentCard.level - 1)));
                break;                                     
            case EnumData.Rarity.Rare:                     
                GameData.Instance.playerData.totalDamBoost /= (boostMultiplier + (0.1f * (currentCard.level - 1)));
                break;                                     
            case EnumData.Rarity.Epic:                     
                GameData.Instance.playerData.totalDamBoost /= (boostMultiplier + (0.15f * (currentCard.level - 1)));
                break;                                     
            case EnumData.Rarity.Legendary:                
                GameData.Instance.playerData.totalDamBoost /= (boostMultiplier + (0.2f * (currentCard.level - 1)));
                break;
        }
    }

    private void RarityBoost()
    {
        switch (rarity)
        {
            case EnumData.Rarity.Common:
                GameData.Instance.playerData.totalDamBoost *= (boostMultiplier + (0.05f * (currentCard.level - 1)));
                break;
            case EnumData.Rarity.Rare:
                GameData.Instance.playerData.totalDamBoost *= (boostMultiplier + (0.1f * (currentCard.level - 1)));
                break;
            case EnumData.Rarity.Epic:
                GameData.Instance.playerData.totalDamBoost *= (boostMultiplier + (0.15f * (currentCard.level - 1)));
                break;
            case EnumData.Rarity.Legendary:
                GameData.Instance.playerData.totalDamBoost *= (boostMultiplier + (0.2f * (currentCard.level - 1)));
                break;
        }
    }
}
