using UnityEngine;

public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placedPrefab; // �ٴڿ� ��ġ�� �� ������ ����
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private LayerMask placedObjectLayerMask; // ����ĳ��Ʈ�� ������ ���̾� ����ũ ����
    private Vector2 touchPosition;
    private Ray ray;
    private RaycastHit hit;

    private void Update()
    {
        // ȭ���� ��ġ���� �ʾҰų� ��ġ�� Began ���°� �ƴϸ� �Լ��� �����մϴ�.
        if (!Utility.TryGetInputPosition(out touchPosition)) return;

        // ������Ʈ ����(select)
        ray = arCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placedObjectLayerMask))
        {
            PlacedObject.SelectedObject = hit.transform.GetComponentInChildren<PlacedObject>();
            return;
        }

        PlacedObject.SelectedObject = null; // ������Ʈ ���� ���

        // �ٴڿ� Raycast�� �浹�ϸ� ��ü�� �����մϴ�.
        if (Utility.Raycast(touchPosition, out Pose hitPose))
        {
            int index = Random.Range(0, placedPrefab.Length); // ������ �������� �ε����� �����ϰ� �����մϴ�.
            Instantiate(placedPrefab[index], hitPose.position, hitPose.rotation); // ������ �������� ��ġ�� ȸ������ ������� �����մϴ�.
        }
    }
}
