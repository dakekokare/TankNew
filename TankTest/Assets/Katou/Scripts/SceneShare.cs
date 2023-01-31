using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShare : MonoBehaviour
{
	//Fî•ñ
	public static Color color;
	// Use this for initialization
	void Start()
	{
		//”jŠü‚³‚ê‚È‚¢‚æ‚¤‚É‚·‚é
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
