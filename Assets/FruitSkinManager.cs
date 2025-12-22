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
        var sprites = skins[currentSkinIndex].fruitSprites;
        int correctedIndex = sprites.Length - 1 - index;

        if (correctedIndex < 0 || correctedIndex >= sprites.Length)
        {
            Debug.LogWarning($"[SkinManager] Invalid index: {correctedIndex}");
            return null;
        }

        return sprites[correctedIndex];
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
