using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownLock : MonoBehaviour
{
    private GameObject tank;
    private TankMovement tankMovement;

    // Start is called before the first frame update
    private void Start()
    {
        tank = GameObject.Find("Tank(Clone)");
        tankMovement = tank.GetComponent<TankMovement>();
    }

    private void MoveUnlock()
    {
        tankMovement.TankUnlock();
    }
}
