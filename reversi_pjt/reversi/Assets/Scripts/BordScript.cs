using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BordScript : MonoBehaviour, IPointerClickHandler
{

    public GameObject piece;
    public Material[] materials;

    // ゲームマネージャー
    private static GameObject gm;
    private static GameManager gmScript;
    private static Reversi reversi;

    // ボードの石の描画状態を保持する配列
    private static GameObject[,] pieceArray = new GameObject[8, 8];

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();
        reversi = new Reversi();

        // ゲーム開始時にreversi.Boardの状態を確認し、ボードの描画状態を更新する。
        ConfirmBord();
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
        Debug.Log("座標(" + x_int + "," + z_int + ")");

        // ReversiクラスのCanPutメソッドに座標を渡し、置くことができるか判定
        bool putFlg = reversi.CanPut(x_int, z_int);
        Debug.Log("置くことができるか判定" + putFlg);

        // 置くことができたら、ReversiクラスのPutStoneメソッドに座標を渡し、情報を更新。
        if (putFlg is true)
        {
            reversi.PutStone(x_int, z_int);

            // reversi.Boardの状態を確認し、ボードの描画状態を更新する。
            ConfirmBord();
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
        if(pieceArray[x, z] is null)
        {
            pieceArray[x, z] = Instantiate(piece, new Vector3(x, 0, z), transform.rotation);
            pieceArray[x, z].GetComponent<Renderer>().material = materials[material];
        }
        else
        {
            pieceArray[x, z].GetComponent<Renderer>().material = materials[material];
        }
    }
}