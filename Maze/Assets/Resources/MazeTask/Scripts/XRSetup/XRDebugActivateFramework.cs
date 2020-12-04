using System.Collections;
using UnityEngine;
using UnityEngine.XR;

namespace uniwue.hci.vilearn
{
    public class XRDebugActivateFramework : MonoBehaviour
    {

        [SerializeField] private Transform vrPlayer = default;
        [SerializeField] private Transform desktopPlayer = default;

        public void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            desktopPlayer.gameObject.SetActive(false);
            vrPlayer.gameObject.SetActive(false);
        }

        void Start()
        {
            StartCoroutine(LoadVRFramework());
        }

        IEnumerator LoadVRFramework()
        {
            string framework = "";

            GameState.enTrackingConfiguration targetFramework = GameState.Instance.trackingConfiguration;

            switch (targetFramework)
            {
                case GameState.enTrackingConfiguration.Desktop:
                    break;
                case GameState.enTrackingConfiguration.OculusVR:
                    framework = "Oculus";
                    break;
                case GameState.enTrackingConfiguration.SteamVR:
                    framework = "OpenVR";
                    break;
            }

            yield return new WaitForEndOfFrame();

            if (framework != "") XRSettings.LoadDeviceByName(framework);
            yield return new WaitForEndOfFrame();

            if (framework != "") XRSettings.enabled = true;
            yield return new WaitForEndOfFrame();

            if (framework != "") Debug.Log("Activate VR Framework: " + framework);

            if (targetFramework == GameState.enTrackingConfiguration.Desktop)
            {
                desktopPlayer.gameObject.SetActive(true);
                vrPlayer.gameObject.SetActive(false);
            }
            else
            {
                #pragma warning disable 0618
                    XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
                #pragma warning restore 0618

                desktopPlayer.gameObject.SetActive(false);
                vrPlayer.gameObject.SetActive(true);

            }

        }
    }
}