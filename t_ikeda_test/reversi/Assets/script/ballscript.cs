using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ballscript : MonoBehaviour
{

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f,0f,-speed * Time.deltaTime);
        if (transform.position.z < -13.0f)
        {
            //Debug.Log("Game Over");
            //Time.timeScale = 0;
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void OnCollisionEnter(Collision collosion)
    {
        if (collosion.gameObject.CompareTag("bar"))
        {
            Destroy(gameObject);
            collosion.gameObject.transform.localScale -= new Vector3(Random.Range(0.2f, 1.0f), 0f, 0f);
            if (collosion.gameObject.transform.localScale.x < 1.0f)
            {
                collosion.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

}
