using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitUIController : MonoBehaviour
{
    [SerializeField] private Image[] fruitImages; // 11個のImageをInspectorで設定

    public void RefreshUI()
    {
        Debug.Log($"RefreshUI on {gameObject.name}");

        for (int i = 0; i < fruitImages.Length; i++)
        {
            if (fruitImages[i] == null)
            {
                Debug.LogError($"{gameObject.name}: fruitImages[{i}] is NULL");
                continue;
            }

            var sprite = FruitSkinManager.Instance.GetFruitSprite(i);
            fruitImages[i].sprite = sprite;
        }
    }


    private void Start()
    {
        RefreshUI(); // シーン開始時に反映
    }


}
