using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class LightEstimationController : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    [SerializeField] Light directionalLight;

    public float? brightness { get; private set; } // 현실 환경의 밝기 예측 값
    public float? colorTemperature { get; private set; } // 현실 환경의 색온도 예측 값
    public Color? colorCorrection { get; private set; } // 현실 환경의 색상보정 예측 값
    public Vector3? mainLightDirection { get; private set; } // (iOS) 현실 환경의 광원 방향 예측
    public Color? mainLightColor { get; private set; } // (iOS) 현실 환경의 주광원 색 예측
    public float? mainLightIntensityLumens { get; private set; } // (iOS) 현실 환경의 루멘 단위 주 광원 밝기
    public SphericalHarmonicsL2? sphericalHarmonics { get; private set; } // (iOS) 현실 환경의 주변 장면 조명 추정

    void OnEnable()
    {
        if (cameraManager != null)
        {
            cameraManager.frameReceived += OnCameraFrameReceived; // 새로운 카메라의 프레임 수신될 때마다 호출되는 이벤트 메소드
        }
    }

    void OnDisable()
    {
        if (cameraManager != null)
        {
            cameraManager.frameReceived -= OnCameraFrameReceived; // 메소드 등록 해제
        }
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            brightness = args.lightEstimation.averageBrightness.Value;
            directionalLight.intensity = brightness.Value;
        }
        else
        {
            brightness = null;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            colorTemperature = args.lightEstimation.averageColorTemperature.Value;
            directionalLight.colorTemperature = colorTemperature.Value;
        }
        else
        {
            colorTemperature = null;
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            colorCorrection = args.lightEstimation.colorCorrection.Value;
            directionalLight.color = colorCorrection.Value;
        }
        else
        {
            colorCorrection = null;
        }

        // 추가로 iOS는 아래와 같은 설정이 가능합니다.
        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            mainLightDirection = args.lightEstimation.mainLightDirection;
            directionalLight.transform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
        }

        if (args.lightEstimation.mainLightColor.HasValue)
        {
            mainLightColor = args.lightEstimation.mainLightColor;
            directionalLight.color = mainLightColor.Value;
        }
        else
        {
            mainLightColor = null;
        }

        if (args.lightEstimation.mainLightIntensityLumens.HasValue)
        {
            mainLightIntensityLumens = args.lightEstimation.mainLightIntensityLumens;
            directionalLight.intensity = args.lightEstimation.averageMainLightBrightness.Value;
        }
        else
        {
            mainLightIntensityLumens = null;
        }

        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = sphericalHarmonics.Value;
        }
        else
        {
            sphericalHarmonics = null;
        }
    }
}
