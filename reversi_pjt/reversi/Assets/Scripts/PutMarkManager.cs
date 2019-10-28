using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutMarkManager : MonoBehaviour
{
    [SerializeField] private GameObject putMark = null;

    // ボードの縦横の大きさを指定
    private const int BORD_HEIGHT_MAX = 8;
    private const int BORD_SIDE_MAX = 8;

    private GameObject[,] putMarks = new GameObject[BORD_HEIGHT_MAX, BORD_SIDE_MAX];

    // Start is called before the first frame update
    // 縦横それぞれ8マスで合計の64個の作成
    void Start()
    {
        for (int i = 0; i < BORD_HEIGHT_MAX; i++)
        {
            for (int j = 0; j < BORD_SIDE_MAX; j++)
            {
                var obj = Instantiate(putMark, new Vector3(1.0f * i, 0.15f, 1.0f * j), transform.rotation);
                obj.SetActive(false);
                putMarks[i, j] = obj;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivePutMark(int x_pos, int z_pos)
    {
        var obj = putMarks[x_pos, z_pos];
        obj.SetActive(true);
    }

    public void ResetPutMark()
    {
        for (int i = 0; i < BORD_HEIGHT_MAX; i++)
        {
            for (int j = 0; j < BORD_SIDE_MAX; j++)
            {
                var obj = putMarks[i, j];
                obj.SetActive(false);
            }
        }
    }

}
