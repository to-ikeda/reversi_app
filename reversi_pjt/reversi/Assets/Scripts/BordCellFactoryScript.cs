using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordCellFactoryScript : MonoBehaviour
{
    public GameObject bordSell;
    

    // ボードの縦横の大きさを指定
    private const int BORD_HEIGHT_MAX = 8;
    private const int BORD_SIDE_MAX = 8;

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
                bordSell.name = "bordSell(" + x + "," + z + ")";
                Instantiate(bordSell, new Vector3(1.0f * x, 0f, 1.0f * z), transform.rotation);
                x++;
            }
            z++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
