using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public int padiTarget;
    public int ikanTarget;
    public int waktuTarget;

    private int ikanCount;
    private int padiCount;

    private float timer;

    public TMP_Text timerText;
    public TMP_Text padiText;
    public TMP_Text ikanText;

    void Start()
    {
        instance = this;

        padiText.text = $"{padiCount}/{padiTarget}";
        ikanText.text = $"{ikanCount}/{ikanTarget}";
    }

    void Update()
    {
         timer += Time.deltaTime;
         UpdateTimer(timer);
    }

    void UpdateTimer(float totalDetik)
    {
        int menit = Mathf.FloorToInt(totalDetik / 60f);
        int detik = Mathf.FloorToInt(totalDetik % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);

        if(menit >= 3 && fisish())
        {
            string scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }

    public void TangkapIkan()
    {
        ikanCount += 1;
        string count = $"{ikanCount}/{ikanTarget}";
        ikanText.text = count;
    }

    public void AmbilPadi()
    {
        padiCount += 1;
        string count = $"{padiCount}/{padiTarget}";
        padiText.text = count;
    }

    private bool fisish()
    {
        return ikanTarget <= ikanCount && padiCount >= padiTarget;
    }
}
