using UnityEngine;
using DG.Tweening; // Import DOTween

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance = null;
    public GameObject player;
    public Transform[] cameraPosition = new Transform[6];
    public Transform targetCamera;
    public float transitionTime = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DOTween.Init();

        CubeFaceDetector.instance?.GetComponent<CubeFaceDetector>().SetCallBack((prevIdx, idx) =>
        {            
            this.transform.DOMove(cameraPosition[idx].position, transitionTime).SetEase(Ease.OutExpo);
        });
        Snake.instance?.GetComponent<Snake>().SetRotationCallback(() =>
        {
            this.transform.rotation = targetCamera.rotation;
        });
    }
}
