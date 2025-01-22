using TMPro;
using UnityEngine;

public class EconomicSystem : MonoBehaviour
{
    public int coins = 0;
    public int earnAmount = 25;
    public float timeToEarn = 2.0f;
    private float currentTime = 0f;
    public TMP_Text textMesh;

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= timeToEarn)
        {
            coins += earnAmount;
            currentTime = 0f;
        }
        else 
            currentTime += Time.deltaTime;

        UpdateTextMesh();
    }

    private void UpdateTextMesh()
    {
        if (textMesh != null)
            textMesh.SetText(coins.ToString());
    }
}
