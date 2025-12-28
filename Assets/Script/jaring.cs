using UnityEngine;

public class jaring : MonoBehaviour
{
    public bool active;
    public Transform posisiIkan;

    public Ikan ikan;

    public void Take()
    {
        active = true;
    }

    public void Drop()
    {
        active = false;
    }

    private void Action(GameObject obj){
        var type = obj.tag;
        switch (type)
        {
            case "Ikan":
                if (!ikan)
                {
                    ikan = obj.GetComponent<Ikan>();

                    ikan.Take();
                    ikan.transform.SetParent(posisiIkan);
                    ikan.transform.localScale = Vector3.one;
                    ikan.transform.localPosition = Vector2.zero;
                }
                break;

            case "Tempat":
                if (ikan)
                {

                    
                    ikan.Drop();
                    ikan.transform.SetParent(obj.transform);
                    ikan = null;
                }
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other != null && active){
            Action(other.gameObject);
        }
    }
}
