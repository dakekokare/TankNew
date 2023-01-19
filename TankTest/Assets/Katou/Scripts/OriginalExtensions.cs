using ExitGames.Client.Photon;
using Photon.Realtime;
public static class OriginalExtensions
{
    /*--------------------Custom Properties*/
    private const string SpawnNum = "SpawnNum";

    private static readonly Hashtable propsToSet = new Hashtable();


    // �v���C���[�̃X�R�A���擾����
    public static int GetSpawn(this Room r)
    {
        return (r.CustomProperties[SpawnNum] is int num) ? num : 0;
    }


    // �v���C���[�̃X�R�A��ݒ肷��
    public static void SetSpawn(this Room r, int num)
    {
        propsToSet[SpawnNum] = num;
        r.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

}
