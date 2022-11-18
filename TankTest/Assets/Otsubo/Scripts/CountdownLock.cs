using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownLock : MonoBehaviour
{
    private GameObject tank;
    private ShotShell shotShell;

    // Start is called before the first frame update
    private void Start()
    {
        tank = GameObject.Find("Tank(Clone)");
        shotShell = tank.transform.GetChild(1).GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetComponent<ShotShell>();
    }

    private void ShotUnlock()
    {
        shotShell.ShotUnlock();
    }
}
