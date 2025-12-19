using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    GameObject currentBowl;
    bool isWaiting = false;
    int nextLevel = 0;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Spawn(0);
    }

    // Update is called once per frame
    void Update()
    {
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
        GameObject obj = Instantiate(
                bowlPrefabs[level],
                spawnPoint.position,
                Quaternion.identity
            );

        obj.GetComponent<Bowl>().SetLevel(level);
        currentBowl = obj;
    }

    // =====================
    // 落とす
    // =====================
    void DropBowl()
    {
        currentBowl.GetComponent<Bowl>().Drop();
        currentBowl = null;

        Invoke(nameof(SpawnNext), 0.5f);
    }

    void SpawnNext()
    {
    int max = Mathf.Min(5, bowlPrefabs.Length);
    int level = Random.Range(0, max);
    Spawn(level);
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
        // 小さいボウル中心（スイカゲームっぽい）
        nextLevel = Random.Range(0, Mathf.Min(5, bowlPrefabs.Length));
    }
}
