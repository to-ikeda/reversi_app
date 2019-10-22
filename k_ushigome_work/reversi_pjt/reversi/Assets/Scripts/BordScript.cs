using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BordScript : MonoBehaviour, IPointerClickHandler
{

    public GameObject piece;
    public Material[] materials;

    // ゲームマネージャー
    GameObject gm;
    GameManager gmScript;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ボードクリック時の処理
    public void OnPointerClick(PointerEventData data)
    {
        //石を表示
        //piece.SetActive(true);
        Instantiate(piece, transform.position, transform.rotation);

        if (gmScript.player == "black")
        {
            //石を黒に変更
            piece.GetComponent<Renderer>().material = materials[0];
            Debug.Log(gmScript.player);
        }
        else {
            //石を白に変更
            piece.GetComponent<Renderer>().material = materials[1];
            Debug.Log(gmScript.player);
        }

    }

}
