

using System;
using TF.Data;
using TMPro;
using UnityEngine;

public class UIUpgradesPanel : PanelBase
{
      [SerializeField] private TextMeshProUGUI damageBoostTxt;
      [SerializeField] private TextMeshProUGUI heathBoostTxt;

      private void Start()
      {
            UpdateTxt();
      }

      private void UpdateTxt()
      {
            //damageBoostTxt.text = $"x{GameData.Instance.playerData.dameBoost.ToString()}";
            //heathBoostTxt.text = $"x{GameData.Instance.playerData.healthBoost.ToString()}";
      }
}
