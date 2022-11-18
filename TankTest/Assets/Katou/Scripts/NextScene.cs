using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //3秒後にメソッドを実行する
        Invoke("Next", 3);

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Next()
    {
        SceneManager.LoadScene("Title");
    }
}
