using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShare : MonoBehaviour
{
	//色情報
	public static Color color;
	// Use this for initialization
	void Start()
	{
		//破棄されないようにする
		DontDestroyOnLoad(this);

	}
	public static Color GetColor()
	{
		return color;
	}
	public static void SetColor(Color c)
    {
		color = c;
    }
}
