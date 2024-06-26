using System;
using System.Collections.Generic;
using Bayat.SaveSystem;
using Bayat.SaveSystem.Demos;
using H2910.Common.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TF.Data
{
    public class GameData : ManualSingletonMono<GameData>
    {
        [SerializeField] protected string identifierField;
        [SerializeField] protected string priceIndentifier;
        [SerializeField] protected PlayerData defaultPlayerData = new PlayerData();
        [SerializeField] protected PriceData defaultPriceData = new PriceData();
        
        internal PlayerData playerData;
        internal PriceData priceData;

        public override void Awake()
        {
            base.Awake();
            Load();
            
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        public async void Save()
        {
            SaveSystemAPI.SaveAsync(this.identifierField, this.playerData);
            SaveSystemAPI.SaveAsync(priceIndentifier, priceData);
            Debug.Log("Player data saved successfully");
        }

        public async void Load()
        {
            if (!await SaveSystemAPI.ExistsAsync(this.identifierField))
            {
                Debug.Log("Player data not found");
                Debug.Log("Using default player data instead");
                this.playerData = this.defaultPlayerData;
                return;
            }
            
            if (!await SaveSystemAPI.ExistsAsync(this.priceIndentifier))
            {
                Debug.Log("Player data not found");
                Debug.Log("Using default data instead");
                priceData = defaultPriceData;
                return;
            }
            
            this.playerData = await SaveSystemAPI.LoadAsync<PlayerData>(this.identifierField);
            priceData = await SaveSystemAPI.LoadAsync<PriceData>(priceIndentifier);
            Debug.Log("Player data loaded successfully");
        }

        public async void Delete()
        {
            SaveSystemAPI.DeleteAsync(this.identifierField);
            Debug.Log("Player data deleted successfully");
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string name = string.Empty;
    public float coin;
    public float gem;
    public float baseHealth;
    public float foodProductionSpeed;
}

[System.Serializable]
public class PriceData
{
    public float foodUpgradePrice;
    public float baseUpgradePrice;
    
}