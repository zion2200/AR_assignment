using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Utility : MonoBehaviour
{
    private static ARRaycastManager raycastManager; // ARRaycastManager ������Ʈ�� ���� ����
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>(); // Raycast ����� ������ ����Ʈ

    // ���� �����ڿ��� ARRaycastManager ������Ʈ�� ã�Ƽ� ������ �Ҵ�
    static Utility()
    {
        raycastManager = Object.FindAnyObjectByType<ARRaycastManager>();
    }

    // ȭ�� ��ġ �Է� ��ġ�� ���� Raycast �����ϴ� �Լ�
    // ȭ�� ��ġ, Raycast ���� �� ��� ���� out �Ķ���ͷ� ����
    public static bool Raycast(Vector2 screenPosition, out Pose pose)
    {
        if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon)) // Raycast ����
        {
            pose = hits[0].pose; // ����� �� ù ��° ������Ʈ�� Pose ���� ��ȯ
            return true; // Raycast ����
        }
        else
        {
            pose = Pose.identity; // Pose ���� �⺻������ ����
            return false; // Raycast ����
        }
    }

    // ��ġ �Է� ��ġ�� �������� �Լ�
    // ��ġ �Է��� ������ false, ������ true�� ��ȯ�ϰ� �Է� ��ġ�� out �Ķ���ͷ� ����
    public static bool TryGetInputPosition(out Vector2 position)
    {
        position = Vector2.zero; // �Է� ��ġ�� �ʱ�ȭ

        if (Input.touchCount == 0) // ��ġ �Է��� ������ false ��ȯ
        {
            return false;
        }

        position = Input.GetTouch(0).position; // ù ��° ��ġ �Է� ��ġ�� ������

        if (Input.GetTouch(0).phase != TouchPhase.Began) // ù ��° ��ġ �Է��� Began ���°� �ƴϸ� false ��ȯ
        {
            return false;
        }

        return true; // ��ġ �Է��� �ְ� Began �����̸� true ��ȯ
    }
}
