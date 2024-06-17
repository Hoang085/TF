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
                Debug.Log("Copy Sprite Component");
                sprites[i].gameObject.CopyComponent(unit.sprites[i].GetComponent<SpriteRenderer>());
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
