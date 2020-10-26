using UnityEngine;
using TMPro;
using Dacen.Utility;

public class Timer : MonoBehaviour
{
    private float timeLastModifiedAt;
    new private bool enabled = false;

    public TextMeshProUGUI textMesh;
    public int CurrentTime { get; private set; }

    public static Timer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!enabled)
            return;

        if(Time.time - timeLastModifiedAt >= 1)
        {
            CurrentTime++;
            ShowTime();
            timeLastModifiedAt = Time.time;
        }
    }

    private void ShowTime() => textMesh.text = DacenUtility.ConvertToMinutesAndSeconds(CurrentTime);

    public void Stop() => enabled = false;

    public void ResetTime()
    {
        CurrentTime = 0;
        ShowTime();
    }

    public void StartCounting()
    {
        timeLastModifiedAt = Time.time;
        enabled = true;
    }
}
