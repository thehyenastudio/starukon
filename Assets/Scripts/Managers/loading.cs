using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public GameObject loadingInfo;
    public GameObject loadingIcon;
    private AsyncOperation async;
    private bool isLoad = false;

    private void Awake()
    {
        Cursor.visible = false;
#if UNITY_ANDROID
        loadingInfo.GetComponent<Text>().text = "TOUCH TO START";
#endif
    }

    IEnumerator Start()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        async = SceneManager.LoadSceneAsync(2);
#endif
#if UNITY_ANDROID
        async = SceneManager.LoadSceneAsync(4);
#endif
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(1.5f);
        loadingInfo.SetActive(true);
        loadingIcon.SetActive(false);
        isLoad = true;
    }

    private void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        if (Input.anyKeyDown && isLoad) async.allowSceneActivation = true;
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0 && isLoad) async.allowSceneActivation = true;
#endif
    }
}
