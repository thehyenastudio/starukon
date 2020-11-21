using UnityEngine.Advertisements;

static public class ADSManager
{ 
    static public void Init()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3764319", false);
        }
    }

    static public void ShowADS(string type)
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(type);
        }
    }
}
