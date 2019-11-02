using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class BordScript : MonoBehaviour, IPointerClickHandler
{

    public GameObject piece;
    public Material[] materials;
    public Material[] materialsBoard;

    // ゲームマネージャー
    private static Reversi reversi;

    // ボードの石の描画状態を保持する配列
    private static GameObject[,] pieceArray = new GameObject[8, 8];

    // Start is called before the first frame update
    void Start()
    {

        reversi = new Reversi();

        // ゲーム開始時にreversi.Boardの状態を確認し、ボードの描画状態を更新する。
        ConfirmBord();
        UpdatePutMark();

        Debug.Log(reversi.Turn);

        //// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
        //foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        //{
        //    // シーン上に存在するオブジェクトならば処理.
        //    if (obj.activeInHierarchy)
        //    {
        //        // GameObjectの名前を表示.
        //        Debug.Log(obj.name);
        //    }
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ボードクリック時の処理
    public void OnPointerClick(PointerEventData data)
    {

        // クリックされたマスから座標を取得する。
        float x = transform.position.x;
        float z = transform.position.z;
        int x_int = (int)x;
        int z_int = (int)z;

        // ReversiクラスのCanPutメソッドに座標を渡し、置くことができるか判定
        bool putFlg = reversi.CanPut(x_int, z_int);

        // 置くことができたら、ReversiクラスのPutStoneメソッドに座標を渡し、情報を更新。
        if (putFlg is true)
        {
            reversi.PutStone(x_int, z_int);

            // reversi.Boardの状態を確認し、ボードの描画状態を更新する。
            ConfirmBord();
            UpdatePutMark();
        }

        //終了判定
        if (reversi.CheckFinish())
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pieceArray[i, j] = null;
                }
            }
            SceneManager.LoadScene("TopScene");
        }
    }

    // ボードの石の状態を確認するメソッド。石が置いてある場合描画を更新する。
    private void ConfirmBord()
    {
        int mateBLACK = 0;
        int mateWHITE = 1;

        int[,] bord = reversi.Board;     

        int z = 0;

        while (z < 8)
        {
            int x = 0;
            while (x < 8)
            {
                int stoneColor = bord[x, z];
                if (stoneColor == Reversi.BLACK)
                {
                    UpdatePieceArray(x, z, mateBLACK);
                }
                else if (stoneColor == Reversi.WHITE)
                {
                    UpdatePieceArray(x, z, mateWHITE);
                }

                x++;
            }
            z++;
        }
    }

    // 石の描画を更新するメソッド。
    private void UpdatePieceArray(int x, int z, int material)
    {
        if (pieceArray[x, z] is null)
        {
            pieceArray[x, z] = Instantiate(piece, new Vector3(x, 0.15f, z), transform.rotation);
            pieceArray[x, z].GetComponent<Renderer>().material = materials[material];
        }
        else
        {
            pieceArray[x, z].GetComponent<Renderer>().material = materials[material];
        }
    }

    private void UpdatePutMark()
    {

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                bool putFlg = reversi.CanPut(i, j);
                if (putFlg)
                {
                    if (reversi.Turn)
                    {
                        GameObject obj = GameObject.Find("bordSell" + i + j + "(Clone)");
                        obj.GetComponent<Renderer>().material = materialsBoard[1];
                    }
                    else {
                        GameObject obj = GameObject.Find("bordSell" + i + j + "(Clone)");
                        obj.GetComponent<Renderer>().material = materialsBoard[2];
                    }

                }
                else
                {
                    GameObject obj = GameObject.Find("bordSell" + i + j + "(Clone)");
                    obj.GetComponent<Renderer>().material = materialsBoard[0];
                }
            }
        }
    }
}