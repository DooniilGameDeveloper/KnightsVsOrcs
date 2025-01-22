using UnityEngine;
using UnityEngine.Video;

public class ParallaxController : MonoBehaviour
{
    public Transform cam;
    private Vector3 camStartPos;
    private float distance;
    private GameObject[] backgrounds;
    private Material[] materials;
    private float[] backSpeed;
    private float farthestBack;
    [Range(0.01f, 0.05f)] 
    public float parallaxSpeed;

    void Start()
    {
        camStartPos = cam.position;
        int backCount = transform.childCount;
        materials = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];
        
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    private void BackSpeedCalculate(int backCount)
    {
        foreach (var background in backgrounds)
        {
            var currentDistance = background.transform.position.z - cam.position.z;
            if (currentDistance > farthestBack)
                farthestBack = currentDistance;
        }

        for (int i = 0; i < backCount; i++)
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x + 5.37f, transform.position.y, 0);
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }

}
