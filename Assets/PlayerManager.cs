using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameManager gameManeger;
    private bool isGrounded = false;
    private bool hasGameOverTriggered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManeger.GameOver();
        }
        if (collision.gameObject.tag == "GameOver")
        {
            Debug.Log("ゲームオーバー");
            gameManeger.GameOver();
        }
    }

}
