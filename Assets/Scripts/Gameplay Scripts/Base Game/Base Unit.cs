using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseUnit : MonoBehaviour
{
    [SerializeField] internal Unit unit;
    [SerializeField] List<SpriteRenderer> sprites;

    private float atk;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnUnitChange()
    {
        if (unit != null)
        {
            gameObject.name = unit.name;
            atk = unit.atk;
            health = unit.health;

            for(int i = 0; i < sprites.Count; i++)
            {
                if (i == unit.sprites.Count)
                {
                    sprites[i].sprite = null;
                    continue; //skip if unit doesn't have the sprite needed for the last element
                }

                //Setting sprite renderer fields
                sprites[i].sprite = unit.sprites[i].sprite;
                sprites[i].color = unit.sprites[i].color;
                sprites[i].sortingOrder = unit.sprites[i].sortingOrder;

                //Setting sprites transform value 
                sprites[i].transform.localPosition = unit.sprites[i].transform.localPosition;
                sprites[i].transform.localRotation = unit.sprites[i].transform.localRotation;
            }
        }
    }
#if UNITY_EDITOR
    private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

    private void _OnValidate()
    {
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;
        Debug.Log("On Validate");
        OnUnitChange();
    }
#endif
}
