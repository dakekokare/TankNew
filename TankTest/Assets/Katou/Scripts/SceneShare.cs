using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShare : MonoBehaviour
{
	//�F���
	public static Color color=Color.blue;
	// Use this for initialization
	void Start()
	{
		//�j������Ȃ��悤�ɂ���
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
