using System;
using System.Collections;
using System.Collections.Generic;
using H2910.Common.Singleton;
using H2910.Level;
using H2910.UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace H2910.Map
{
    public class MapController : ManualSingletonMono<MapController>
    {
        [SerializeField] private List<LevelButton> mapLevelButtons;

        public List<LevelButton> MapLevelButtons
        {
            get { return mapLevelButtons; }
            set { mapLevelButtons = value; }
        }

        private int _levelsUnlocked;

        private LevelButton _activeButtonCur;

        public LevelButton ActiveButtonCur
        {
            get { return _activeButtonCur; }
            set { _activeButtonCur = value; }
        }

        [HideInInspector] public Canvas parentCanvas;
        private MapMaker _mapMaker;
        private ScrollRect _scrollRect;
        private RectTransform _content;
        private int _biomesCount = 6;

        private void Start()
        {
            _mapMaker = GetComponent<MapMaker>();
            _content = GetComponent<RectTransform>();
            if (_mapMaker.biomes == null)
            {
                Debug.LogError("No map");
                return;
            }

            parentCanvas = GetComponentInParent<Canvas>();
            _scrollRect = GetComponentInParent<ScrollRect>();

            List<Biome> blist = new List<Biome>(_mapMaker.biomes);
            blist.RemoveAll((b) => { return b == null; });

            if (_mapMaker.mapType == MapType.Vertical) blist.Reverse();
            mapLevelButtons = new List<LevelButton>();
            foreach (var b in blist)
            {
                mapLevelButtons.AddRange(b.levelButtons);
            }
            
            _levelsUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
            
            for (int i = 0; i < MapLevelButtons.Count; i++)
            {
                MapLevelButtons[i].nameTxt.text = $"{i + 1}";
                SetButtonCurrentActive(i, false, false);
            }

            for (int i = 0; i < _levelsUnlocked; i++)
            {
                SetButtonCurrentActive(i,true,true);
            }
        }

        private void SetButtonCurrentActive(int levelNumber, bool active, bool isPassed)
        {
            MapLevelButtons[levelNumber].SetActive(active, isPassed);
        }
    }
}