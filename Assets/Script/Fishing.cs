using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class Fishing : MonoBehaviour
{
    private bool isActive;
    public LineRenderer senar;
    private bool isFishing;
    public Transform posisiSenar;
    public Transform kail;
    public Transform posisiPancing;
    public Transform Ember;

    private bool waiting;

    public List<Ikan> ikans = new List<Ikan>();

    void Update()
    {
        if(!isActive) return;
        
        PosisiKail();

        if (isFishing)
        {
            kail.position = posisiPancing.position;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !waiting)
        {
            Action();
        }
    }
    public void Take()
    {
        isActive = true;
    }

    public void Drop(){
        isActive = false;
        isFishing = false;
        kail.position = posisiSenar.position;
    }


    public void Action()
    {
        if (isFishing)
        {
            TangkapIkan();
        }
        else
        {
            kail.DOMove(posisiPancing.position, 1f);
        }

        isFishing = !isFishing;
    }

    private void TangkapIkan()
    {
       
        Ikan ikan = null;
        if(ikans.Count > 1)
        {
            float distance = Mathf.Infinity;
            foreach(var i in ikans)
            {
                float dist = Vector3.Distance(kail.position, i.transform.position);

                if(dist < distance)
                {
                    ikan = i;
                    distance = dist;
                }
            }
        }
        else if (ikans.Count == 1)
        {
            ikan = ikans[0];
        }
        ikan.Take();
        ikans.Remove(ikan);
        Vector3 posisi = posisiSenar.position;
        kail.DOMove(new Vector3(posisi.x, posisi.y - 5f, posisi.z), 1f);
        StartCoroutine(AmbilIkan(ikan));
    }

    private void PosisiKail()
    {
        senar.SetPosition(0, posisiSenar.position);
        senar.SetPosition(1, kail.position);

    }

    IEnumerator AmbilIkan(Ikan ikan)
    {  
        waiting = true;
        yield return new WaitForSeconds(3f);
        waiting = false;
        Manager.instance.TangkapIkan();
        ikan.Drop();
        ikan.transform.SetParent(Ember.transform);
        ikan = null;
    }
}
