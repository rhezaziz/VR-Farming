using UnityEngine;

public class Padi : MonoBehaviour
{
    public GameObject beras;
    public GameObject padi;
    public void Take()
    {
        beras.SetActive(true);
        padi.SetActive(false);
    }

    public void Drop()
    {
        
    }

}
