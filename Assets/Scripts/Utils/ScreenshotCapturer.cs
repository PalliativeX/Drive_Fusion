using System;
using System.IO;
using UnityEngine;

namespace Utils
{
#if UNITY_EDITOR
    public sealed class ScreenshotCapturer : Singleton<ScreenshotCapturer>
    {
        [SerializeField] private bool hasCustomPath;

        [SerializeField]
        private string path;

        [SerializeField] [Range(1, 5)] private int resolutionIncreaseFactor = 1;

        private string fileName;

        private string defaultPath;

        protected override void Awake()
        {
            base.Awake();
            
            defaultPath = Path.Combine(Application.dataPath, "../Screenshots/");

            if (!Directory.Exists(defaultPath))
                Directory.CreateDirectory(defaultPath);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                fileName = "screenshot ";
                fileName += Guid.NewGuid() + ".png";

                ScreenCapture.CaptureScreenshot(hasCustomPath ? path + fileName : defaultPath + fileName,
                    resolutionIncreaseFactor);

                Debug.Log("Screenshot captured!");
            }
        }
    }
#else
    public sealed class ScreenshotCapturer : Singleton<ScreenshotCapturer> {}
#endif
}