using UnityEngine;
using UnityEngine.EventSystems;

public class PlacedObject : MonoBehaviour
{
    [SerializeField]
    private GameObject cubeSelected; // ���õ� ��ü�� ǥ���ϴ� ť�� ������
    private static PlacedObject selectedObject; // ���� ���õ� ��ü

    public bool IsSelected
    {
        get => SelectedObject == this;
    }
    public static PlacedObject SelectedObject
    {
        get => selectedObject;
        set
        {
            if (selectedObject == value)
            {
                return;
            }

            if (selectedObject != null)
            {
                selectedObject.cubeSelected.SetActive(false); // ������ ���õ� ��ü�� ��Ȱ��ȭ
            }

            selectedObject = value; // ���ο� ��ü�� ����

            if (value != null)
            {
                value.cubeSelected.SetActive(true); // ���ο� ��ü�� Ȱ��ȭ
            }
        }
    }
    private void Awake()
    {
        cubeSelected.SetActive(false); // ���õ� ť�긦 ��Ȱ��ȭ
    }
    // ��ġ �� �巡���ϴ� ��ġ�� ���� ���õ� ������Ʈ�� �̵�
    public void OnPointerDrag(BaseEventData bed)
    {
        if (IsSelected) // ���� ��ü�� ���õ� ��ü�� ���
        {
            PointerEventData ped = (PointerEventData)bed;
            if (Utility.Raycast(ped.position, out Pose hitPose)) // ��ġ ��ġ�� Raycast�� �����Ͽ� plane�� �ε����� ��ġ�� ����
            {
                transform.position = hitPose.position; // ���� ���õ� ��ü�� plane�� �ε��� ��ġ�� �̵���Ŵ
            }
        }
    }
}
