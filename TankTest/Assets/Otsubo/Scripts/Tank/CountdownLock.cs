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
        tank = GameObject.Find("Boat(Clone)");
        shotShell = tank.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<ShotShell>();
    }

    private void ShotUnlock()
    {
        shotShell.ShotUnlock();
    }
}
