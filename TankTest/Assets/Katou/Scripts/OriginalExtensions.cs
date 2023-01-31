using ExitGames.Client.Photon;
using Photon.Realtime;
public static class OriginalExtensions
{
    /*--------------------Custom Properties*/
    
    //プレイヤーのスポーン座標番号
    private const string SpawnNum = "SpawnNum";

    //アイテムスポーン座標番号
    private const string SpawnItemNum = "SpawnItemNum";

    //カラー情報
    private const string PlayerColor = "PlayerColor";

    private static readonly Hashtable propsToSet = new Hashtable();


    // 取得する
    public static int GetSpawn(this Room r)
    {
        return (r.CustomProperties[SpawnNum] is int num) ? num : 0;
    }


    // 設定する
    public static void SetSpawn(this Room r, int num)
    {
        propsToSet[SpawnNum] = num;
        r.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    // 取得する
    public static int[] GetItemSpawn(this Room r)
    {
        return (r.CustomProperties[SpawnItemNum] is int[] num) ? num : null;
    }


    // 設定する
    public static void SetItemSpawn(this Room r, int[] num)
    {
        propsToSet[SpawnItemNum] = num;
        r.SetCustomProperties(propsToSet);
        propsToSet.Clear();

    }
    // 取得する
    public static float[] GetColor(this Room r)
    {
        return (r.CustomProperties[PlayerColor] is float[] col) ? col : null;
    }


    // 設定する
    public static void SetColor(this Room r, float[] col)
    {
        propsToSet[PlayerColor] = col;
        r.SetCustomProperties(propsToSet);
        propsToSet.Clear();

    }


}
