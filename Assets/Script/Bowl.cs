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

    Rigidbody2D rb;

    bool isMerging = false;
    bool merged = false;
    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // ★ 落とす前は止める
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Prepare()
    {
        rb.simulated = false; // スポーン直後用
    }

    public void Drop()
    {
        if (isDropped) return;

        isDropped = true;

        // ★ 落とした瞬間に有効化
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMerging) return;
        if (!isDropped) return;

        Bowl other = collision.gameObject.GetComponent<Bowl>();
        if (other == null) return;
        if (!other.isDropped) return;
        if (other.isMerging) return;

        if (other.level == level)
        {
            isMerging = true;
            other.isMerging = true;

            Vector2 pos = (transform.position + other.transform.position) / 2f;

            Destroy(other.gameObject);
            Destroy(gameObject);

            BowlSpawner.Instance.Merge(level + 1, pos);
        }
    }

    public void ActivateFromMerge()
    {
        isDropped = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
    }
    public void SetLevel(int l)
    {
        level = l;
    }
}
