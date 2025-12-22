using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelectButton : MonoBehaviour
{
    [SerializeField] private int skinIndex;

    public void OnClick()
    {
        Debug.Log("?? SkinSelectButton clicked: " + skinIndex);
        FruitSkinManager.Instance.SetSkin(skinIndex);
        foreach (var controller in FindObjectsOfType<FruitUIController>())
        {
            controller.RefreshUI();
        }
    }
}
