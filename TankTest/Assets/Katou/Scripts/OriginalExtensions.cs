using ExitGames.Client.Photon;
using Photon.Realtime;
public static class OriginalExtensions
{
    /*--------------------Custom Properties*/
    
    //�v���C���[�̃X�|�[�����W�ԍ�
    private const string SpawnNum = "SpawnNum";

    //�A�C�e���X�|�[�����W�ԍ�
    private const string SpawnItemNum = "SpawnItemNum";

    //�J���[���
    private const string PlayerColor = "PlayerColor";

    private static readonly Hashtable propsToSet = new Hashtable();


    // �擾����
    public static int GetSpawn(this Room r)
    {
        return (r.CustomProperties[SpawnNum] is int num) ? num : 0;
    }


    // �ݒ肷��
    public static void SetSpawn(this Room r, int num)
    {
        propsToSet[SpawnNum] = num;
        r.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // �擾����
    public static int[] GetItemSpawn(this Room r)
    {
        return (r.CustomProperties[SpawnItemNum] is int[] num) ? num : null;
    }


    // �ݒ肷��
    public static void SetItemSpawn(this Room r, int[] num)
    {
        propsToSet[SpawnItemNum] = num;
        r.SetCustomProperties(propsToSet);
        propsToSet.Clear();

    }
    // �擾����
    public static float[] GetColor(this Room r)
    {
        return (r.CustomProperties[PlayerColor] is float[] col) ? col : null;
    }


    // �ݒ肷��
    public static void SetColor(this Room r, float[] col)
    {
        propsToSet[PlayerColor] = col;
        r.SetCustomProperties(propsToSet);
        propsToSet.Clear();

    }


}
