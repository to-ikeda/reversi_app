using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordCellFactoryScript : MonoBehaviour
{
    [SerializeField] private GameObject bordSell;
    [SerializeField] private LineRenderer bordHeightLine;
    [SerializeField] private LineRenderer bordSideLine;
    [SerializeField] private GameObject bordMark;

    // ボードの縦横の大きさを定義
    private const int BORD_HEIGHT_MAX = 8;
    private const int BORD_SIDE_MAX = 8;

    // ボードの線の数を定義
    private const int BORD_LINE = 9;

    // Start is called before the first frame update
    // 縦横それぞれ8マスで合計の64個のマスを作成
    void Start()
    {
        int z = 0;

        while (z < BORD_HEIGHT_MAX)
        {
            int x = 0;

            while (x < BORD_SIDE_MAX)
            {
                bordSell.name = "bordSell" + x + z ;
                Instantiate(bordSell, new Vector3(1.0f * x, 0f, 1.0f * z), Quaternion.identity);
                x++;
            }
            z++;
        }

        // ボードのマスの線を作成
        for (int i = 0; i < BORD_LINE; i++)
        {
            // 縦線を作成
            Instantiate(bordHeightLine, new Vector3(-0.5f + i, 0.1f, 7.5f), Quaternion.identity);

            // 横線を作成
            var sideLineObject = Instantiate(bordSideLine, new Vector3(-0.5f, 0.1f, -0.5f + i), Quaternion.identity);
            // 横線を90度回転させる
            sideLineObject.transform.Rotate(0.0f, 90.0f, 0.0f);
        }

        // ボードの丸いマークを4つ作成。
        Instantiate(bordMark, new Vector3(1.5f, 0.1f, 1.5f), Quaternion.identity);
        Instantiate(bordMark, new Vector3(5.5f, 0.1f, 1.5f), Quaternion.identity);
        Instantiate(bordMark, new Vector3(1.5f, 0.1f, 5.5f), Quaternion.identity);
        Instantiate(bordMark, new Vector3(5.5f, 0.1f, 5.5f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
