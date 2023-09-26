
using UnityEngine;

public class popupUI : MonoBehaviour
{
    public Transform obj; // ������Ʈ ��ġ
    public Transform cameradistance; //ī�޶� ��ġ
    public Transform Infodistance;// �г���ġ
    public CanvasGroup Info;
    public bool activeinfo = false;

    public float distance;
    float alpha;

    private void Start()
    {
        Info.alpha = 0;
        cameradistance = GameManager.instance.camPos;
    }

    private void Update()
    {

        if(cameradistance == null)
        {
            cameradistance = GameManager.instance.camPos;
        }

        if (IsDistance())
        {
            alpha = 1;
            activeinfo = true;
        }
        if (!IsDistance())
        {
            Info.alpha = 0;
            activeinfo = false;
        }
        Info.alpha = Mathf.Clamp01(Info.alpha + alpha * Time.deltaTime);
    }

    bool IsDistance()
    {
        var dir = Infodistance.position - cameradistance.position;
        if(dir.magnitude < distance)
        {
            if(obj != null)
            {
                var objdir= obj.position - cameradistance.position;
                //�� �Ÿ������� ����
                if(Vector3.Dot(cameradistance.forward, objdir.normalized) < distance)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
