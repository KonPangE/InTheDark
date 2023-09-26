
using UnityEngine;

public class popupUI : MonoBehaviour
{
    public Transform obj; // 오브젝트 위치
    public Transform cameradistance; //카메라 위치
    public Transform Infodistance;// 패널위치
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
                //두 거리사이의 각도
                if(Vector3.Dot(cameradistance.forward, objdir.normalized) < distance)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
