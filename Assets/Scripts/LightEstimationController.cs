using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class LightEstimationController : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    [SerializeField] Light directionalLight;

    public float? brightness { get; private set; } // ���� ȯ���� ��� ���� ��
    public float? colorTemperature { get; private set; } // ���� ȯ���� ���µ� ���� ��
    public Color? colorCorrection { get; private set; } // ���� ȯ���� ������ ���� ��
    public Vector3? mainLightDirection { get; private set; } // (iOS) ���� ȯ���� ���� ���� ����
    public Color? mainLightColor { get; private set; } // (iOS) ���� ȯ���� �ֱ��� �� ����
    public float? mainLightIntensityLumens { get; private set; } // (iOS) ���� ȯ���� ��� ���� �� ���� ���
    public SphericalHarmonicsL2? sphericalHarmonics { get; private set; } // (iOS) ���� ȯ���� �ֺ� ��� ���� ����

    void OnEnable()
    {
        if (cameraManager != null)
        {
            cameraManager.frameReceived += OnCameraFrameReceived; // ���ο� ī�޶��� ������ ���ŵ� ������ ȣ��Ǵ� �̺�Ʈ �޼ҵ�
        }
    }

    void OnDisable()
    {
        if (cameraManager != null)
        {
            cameraManager.frameReceived -= OnCameraFrameReceived; // �޼ҵ� ��� ����
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

        // �߰��� iOS�� �Ʒ��� ���� ������ �����մϴ�.
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
