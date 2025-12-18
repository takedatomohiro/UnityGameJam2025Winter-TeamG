using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlSpawner : MonoBehaviour
{
    public static BowlSpawner Instance;
    public GameObject[] bowlPrefabs;
    public Transform spawnPoint;
    public int nextLevel;

    public float moveSpeed = 5f;
    public float minX = -2.5f;
    public float maxX = 2.5f;

    GameObject currentBowl;
    bool isWaiting = false;
    // Start is called before the first frame update
    void Start()
    {
        //Spawn(int level);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting && currentBowl != null)
        {
            Move();
        }

        // ★ ここが NullReference 対策
        if (currentBowl == null) return;

        // 落とす前はスポーン位置に固定
        currentBowl.transform.position = spawnPoint.position;

        // スペースキーで落とす
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBowl();
        }
    }

    void DropBowl()
    {
        // ★ 落としたフラグをON
        currentBowl.GetComponent<Bowl>().Drop();

        currentBowl = null;

        // 次のボウルを出す
        Invoke(nameof(Spawn), 0.5f);
    }

    public void Spawn(int level)
    {
        int index = Random.Range(0, bowlPrefabs.Length);

        currentBowl = Instantiate(
            bowlPrefabs[index],
            spawnPoint.position,
            Quaternion.identity
        );
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 pos = currentBowl.transform.position;
        pos.x += x * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        currentBowl.transform.position = pos;
    }

    // 合体用
    public void Merge(int nextLevel, Vector2 pos)
    {
        if (nextLevel >= bowlPrefabs.Length) return;

        Instantiate(bowlPrefabs[nextLevel], pos, Quaternion.identity);
    }

    void DecideNextLevel()
    {
        // 小さいボウル中心にする（スイカゲームっぽく）
        nextLevel = Random.Range(0, Mathf.Min(5, bowlPrefabs.Length));
    }
}
