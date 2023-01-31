using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownLock : MonoBehaviour
{
    private GameObject tank;
    private ShotShell shotShell;
    private GameObject bgm;
    private BGMSystem BGMsystem;

    // Start is called before the first frame update
    private void Start()
    {
        tank = GameObject.Find("Boat(Clone)");
        shotShell = 
            tank.transform.
            GetChild(0).
            GetChild(0).
            GetChild(0).
            GetChild(1).
            GetChild(0).
            GetComponent<ShotShell>();
        //tank = GameObject.Find("ShotShell");
        //shotShell = tank.transform.GetComponent<ShotShell>();

        bgm = GameObject.Find("BGM");
        BGMsystem = bgm.transform.GetComponent<BGMSystem>();
    }

    private void ShotUnlock()
    {
        shotShell.ShotUnlock();
    }
    private void StartBGM()
    {
        BGMsystem.StartBGM();
    }
}
