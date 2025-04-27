using UnityEngine;
using UnityEngine.XR.ARFoundation; // ����

public class ARFaceMaterialSwitcher : MonoBehaviour
{
    [SerializeField]
    private Material[] materials; // ����� ��Ƽ���� �迭
    private ARFaceManager faceManager; // ARFace ������
    private int index = 0; // ���� ��Ƽ���� �迭 �ε���

    private void Start()
    {
        faceManager = GetComponent<ARFaceManager>(); // ARFaceManager ���� ��� ��������
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // ��ġ �Է��� �����Ǹ�
        {
            index = (index + 1) % materials.Length; // ��Ƽ���� �迭 �ε��� ���� �� ���� ������ ��ȯ

            // ��� ARFace ������Ʈ�� ���� ���ο� ��Ƽ���� �Ҵ�
            foreach (ARFace face in faceManager.trackables)
            {
                face.GetComponent<MeshRenderer>().material = materials[index];
            }
        }
    }
}