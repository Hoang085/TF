using H2910.Map;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace H2910.Level
{
    public class LevelButton : MonoBehaviour
    { 
        public TextMeshProUGUI nameTxt;
        [SerializeField] private GameObject groupText;
        [SerializeField] private GameObject lockObj;

        public bool Interactable { get; private set; }

        internal void SetActive(bool active, bool isPassed)
        {
            Interactable = active;
            if (active)
            {
                MapController.Instance.ActiveButtonCur = this;
            }

            if (!isPassed && !active)
            {
                lockObj.SetActive(true);
                groupText.SetActive(false);
            }
            else
            {
                lockObj.SetActive(false);
                groupText.SetActive(true);
            }
        }
    }
}