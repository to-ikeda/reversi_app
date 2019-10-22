using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordCellFactoryScript : MonoBehaviour
{
    public GameObject bordSell;

    //ボードの縦横の大きさを指定
    private const int CELL_HEIGHT_MAX = 8;
    private const int CELL_SIDE_MAX = 8;

    // Start is called before the first frame update
    // 縦横それぞれ8マスで合計の64個のマスを作成
    void Start()
    {
        int height = 0;

        while (height < CELL_HEIGHT_MAX)
        {
            int side = 0;

            while (side < CELL_SIDE_MAX)
            {
                Instantiate(bordSell, new Vector3(1.0f * side, 0f, 1.0f * height), transform.rotation);
                side++;
            }
            height++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
