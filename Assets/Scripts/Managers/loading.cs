using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    public GameObject loadingInfo;
    public GameObject background;
    private AsyncOperation async;

    private void Awake()
    {
        Cursor.visible = false;
        Helper.Set2DCameraToObject(background);
    }

    IEnumerator Start()
    {
        async = SceneManager.LoadSceneAsync(1);
        loadingInfo.SetActive(false);
        yield return true;
        async.allowSceneActivation = false;
        loadingInfo.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown) async.allowSceneActivation = true;
    }
}
