using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public enum BowlType
    {
        bowl_1,
        bowl_2,
        bowl_3,
        bowl_4,
        bowl_5,
        bowl_6,
        bowl_7,
        bowl_8,
        bowl_9,
        bowl_10,
        bowl_11
    }
    public BowlType type;
    public int level;   // 0,1,2,3...
    public bool isDropped = false;
    public AudioClip mergeSE;
    AudioSource audioSource;

    Rigidbody2D rb;

    bool merged = false;
    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;   // ★ 最初は物理OFF

        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Drop()
    {
        isDropped = true;
        rb.simulated = true;    // ★ 落とした瞬間に物理ON
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Bowl other = collision.gameObject.GetComponent<Bowl>();
        if (other == null) return;
        if (merged || other.merged) return;
        if (other.level != level) return;

        BowlSpawner spawner = FindAnyObjectByType<BowlSpawner>();
        if (spawner == null) return;

        merged = true;
        other.merged = true;

        if (mergeSE != null)
        {
            AudioSource.PlayClipAtPoint(mergeSE, transform.position);
        }

        bool isMaxLevel = level >= spawner.bowlPrefabs.Length - 1;

        // ★ スコア加算（ここが重要）
        int addScore = (level + 1) * 10;
        if (isMaxLevel) addScore *= 5;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(addScore);
        }
        else
        {
            Debug.LogError("ScoreManager.Instance が NULL");
        }

        if (!isMaxLevel)
        {
            Vector3 pos = (transform.position + other.transform.position) * 0.5f;
            Instantiate(spawner.bowlPrefabs[level + 1], pos, Quaternion.identity);
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
