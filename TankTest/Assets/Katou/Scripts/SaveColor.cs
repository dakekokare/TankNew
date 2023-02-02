using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SaveColor : MonoBehaviourPunCallbacks
{
    //�v���C���[�J���[
    private Vector3 pColor;
    //�G�l�~�[�J���[
    private Vector3 eColor;
    public Vector3 GetPlayerColor()
    {
        return pColor;
    }
    public void AddPlayerColor(Vector3 vec)
    {
        pColor = vec;
    }
    public Vector3 GetEnemyColor()
    {
        return eColor;
    }
    public void AddEnemyColor(Vector3 vec)
    {
        eColor = vec;
    }


}
