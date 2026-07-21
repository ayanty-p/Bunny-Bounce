using UnityEngine;
using System.Runtime.InteropServices;

public class MobileControlsUI : MonoBehaviour
{
    [Tooltip("LeftButton, RightButton, JumpButtonをまとめた親オブジェクト")]
    public GameObject controlsRoot;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern int IsMobileBrowser();
#endif

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // ブラウザ(index.html)がwindow.isMobileDeviceに保存した判定結果を、
        // シーンが読み込まれるたびに毎回問い合わせる(Input.touchSupportedはPCで誤判定することがあるため使わない)
        controlsRoot.SetActive(IsMobileBrowser() == 1);
#else
        controlsRoot.SetActive(Input.touchSupported);
#endif
    }
}
