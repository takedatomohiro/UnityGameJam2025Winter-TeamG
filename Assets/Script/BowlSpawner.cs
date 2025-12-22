using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlSpawner : MonoBehaviour
{
    public static BowlSpawner Instance;

    public GameObject[] bowlPrefabs;   // ボウルPrefab配列
    public Transform spawnPoint;        // 出現位置
    public AudioSource seSource;
    public AudioClip mergeSE;

    public float moveSpeed = 5f;
    public float minX = -2.5f;
    public float maxX = 2.5f;
    public Image nextBowlImage;

    GameObject currentBowl;
    bool isWaiting = false;
    int nextLevel;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DecideNextLevel();
        Spawn(nextLevel);
        DecideNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameManager>().IsGameOver())
            return;

        if (currentBowl == null) return;

        // 落とす前は左右移動
        float x = Input.GetAxis("Horizontal");
        Vector3 pos = currentBowl.transform.position;
        pos.x += x * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        currentBowl.transform.position = pos;

        // スペースで落とす
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBowl();
        }
    }

    void Spawn(int level)
    {
        GameObject obj = Instantiate(bowlPrefabs[level], spawnPoint.position, Quaternion.identity);
        obj.GetComponent<Bowl>().SetLevel(level);
        currentBowl = obj;
    }


    // =====================
    // 落とす
    // =====================
    void DropBowl()
    {
        if (FindObjectOfType<GameManager>().IsGameOver())
            return;

        currentBowl.GetComponent<Bowl>().Drop();
        currentBowl = null;

        Invoke(nameof(SpawnNext), 0.5f);
    }

    void SpawnNext()
    {
        Spawn(nextLevel);
        DecideNextLevel();
    }

    // =====================
    // 左右移動
    // =====================
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 pos = currentBowl.transform.position;

        pos.x += x * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        currentBowl.transform.position = pos;
    }

    // =====================
    // 合体生成
    // =====================
    public void Merge(int level, Vector2 pos)
    {
        if (level >= bowlPrefabs.Length) return;

        // ★ SE再生
        if (mergeSE != null && seSource != null)
        {
            seSource.PlayOneShot(mergeSE);
        }

        GameObject obj = Instantiate(
            bowlPrefabs[level],
            pos,
            Quaternion.identity
        );

        Bowl bowl = obj.GetComponent<Bowl>();
        bowl.SetLevel(level);
        bowl.ActivateFromMerge();
    }

    // =====================
    // 次のレベル決定
    // =====================
    void DecideNextLevel()
    {
        int max = Mathf.Min(5, bowlPrefabs.Length);
        nextLevel = Random.Range(0, max);

        UpdateNextUI();
    }

    void UpdateNextUI()
    {
        if (nextBowlImage == null) return;

        Sprite sprite = FruitSkinManager.Instance.GetFruitSprite(nextLevel);
        if (sprite != null)
        {
            nextBowlImage.sprite = sprite;
            Debug.Log($"🍇 Next UI updated: {sprite.name}");
        }
    }

}
