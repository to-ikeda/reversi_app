using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BordScript : MonoBehaviour, IPointerClickHandler
{

    public GameObject piece;
    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ボードクリック時の処理
    public void OnPointerClick(PointerEventData data)
    {
        //石を表示
        piece.SetActive(true);

        //石を白に変更
        piece.GetComponent<Renderer>().material = materials[1];

        //石を黒に変更
        //piece.GetComponent<Renderer>().material = materials[0];

    }

}
