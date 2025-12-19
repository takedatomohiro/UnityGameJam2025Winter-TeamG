using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundLoader : MonoBehaviour
{
    public Image backgroundImage;  // UIのImageコンポーネント
    // Start is called before the first frame update
    void Start()
    {
        UpdateBackground();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void UpdateBackground()
    {
        string bgName = BackgroundData.selectedBackgroundName;
        Sprite bgSprite = Resources.Load<Sprite>("Backgrounds/" + bgName);
        if (bgSprite != null)
        {
            backgroundImage.sprite = bgSprite;
            Debug.Log("背景を更新: " + bgName);
        }
        else
        {
            Debug.LogWarning("背景画像が見つかりません: " + bgName);
        }
    }
}
