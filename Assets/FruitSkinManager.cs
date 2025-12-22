using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSkinManager : MonoBehaviour
{
    public static FruitSkinManager Instance;

    [SerializeField] private List<FruitUISkin> skins = new List<FruitUISkin>();
    private int currentSkinIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sprite GetFruitSprite(int index)
    {
        if (index < 0 || index >= skins[currentSkinIndex].fruitSprites.Length)
        {
            Debug.LogWarning($"[SkinManager] Index {index} out of range!");
            return null;
        }

        var sprite = skins[currentSkinIndex].fruitSprites[index];
        if (sprite == null)
        {
            Debug.LogWarning($"[SkinManager] Sprite at index {index} is null!");
        }

        return sprite;
    }



    public void SetSkin(int index)
    {
        Debug.Log($"?? SetSkin({index})");
        if (index >= 0 && index < skins.Count)
        {
            currentSkinIndex = index;
            Debug.Log($"? Skin changed to: {skins[index].skinName}");
        }
        else
        {
            Debug.LogWarning($"? Invalid skin index: {index}");
        }
    }

    public int GetCurrentSkinIndex() => currentSkinIndex;
    public int GetSkinCount() => skins.Count;
}
