using System.Collections;
using UnityEngine;

public class TreasureHint : MonoBehaviour
{
    public GameObject hintPanel;
    public float showAfterSeconds = 300f; // 5 phút

    void Start()
    {
        hintPanel.SetActive(false);
        StartCoroutine(ShowAfterDelay());
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(showAfterSeconds);
        hintPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        hintPanel.SetActive(false);
    }
}