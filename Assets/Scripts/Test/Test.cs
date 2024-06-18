using System;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteToMerge;
    [SerializeField] private SpriteRenderer finalSprite;

    private void Awake()
    {
        Merge();
    }

    private void Merge()
    {
        Resources.UnloadUnusedAssets();
        var NewTex = new Texture2D(500, 500);

        for (int x = 0; x < NewTex.width; x++)
        {
            for (int y = 0; y < NewTex.height; y++)
            {
                NewTex.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }

        for (int i = 0; i < spriteToMerge.Length; i++)
        {
            for (int x = 0; x < spriteToMerge[i].sprite.texture.width; x++)
            {
                for (int y = 0; y < spriteToMerge[i].sprite.texture.width; y++)
                {
                    var color = spriteToMerge[i].sprite.texture.GetPixel(x, y).a == 0 ? 
                        NewTex.GetPixel(x, y) : 
                        spriteToMerge[i].sprite.texture.GetPixel(x, y);

                    NewTex.SetPixel(x, y, color);
                }
            }
        }
        
        NewTex.Apply();
        var finalsprite = Sprite.Create(NewTex, new Rect(0, 0, NewTex.width, NewTex.height), new Vector2(0.5f, 0.5f));
        finalsprite.name = "NewSprite";
        finalSprite.sprite = finalsprite;
    }

}
