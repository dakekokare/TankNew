using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShare : MonoBehaviour
{
	//êFèÓïÒ
	public static Color color=Color.blue;
	// Use this for initialization
	void Start()
	{
		//îjä¸Ç≥ÇÍÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
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
