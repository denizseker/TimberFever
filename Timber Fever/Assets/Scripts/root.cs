using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class root : MonoBehaviour
{
    //prefab a�a�
    public GameObject treeBody;
    //prefab a�a� k�k
    public GameObject treeRoot;
    //game manager objesi
    public GameObject gameManager;
    //spawn edilen a�a�
    private GameObject newBody;
    //yapraklar�n a��lmas�n� tutan script
    private SkinnedMeshRenderer skinRend;
    private GameObject newBodyRootPrivate;

    //Rootun doluluk/aktiflik durumu
    public bool isRootEmpty = true;
    public bool isRootActive = false;


    private void OnTriggerExit(Collider other)
    {
        //Chainsaw node �zerinden ��kt��� zaman
        if(other.gameObject.tag == "chainsaw")
        {
            //Root aktif ise a�ac� olu�turuyoruz
            if (isRootActive)
            {
                //A�ac� rootun �zerine spawn ediyoruz
                newBody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z), Quaternion.identity);
                //A�ac�n parentini root yap�yoruz
                newBody.transform.SetParent(transform);
                //A�ac� aktif hale getiriyoruz
                newBody.SetActive(true);
                //Rootun art�k dolu oldu�unu s�yl�yoruz
                isRootEmpty = false;
            }
            
        }
    }

    //Click eventi ile root kapatma
    public void CloseRoot()
    {
        //Root active ve bo� ise
        if (isRootActive && isRootEmpty)
        {
            //Root bo� oldu�u i�in bodyi olu�turuyoruz
            newBody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
            //B�y�m�� haline getiriyoruz
            newBody.transform.localScale = new Vector3(1, 2, 1);
            //Bodyi yok ediyoruz
            Destroy(newBody);
            Destroy(newBodyRootPrivate);
            //Rootu deaktif edip bo� oldu�unu belirtiyoruz
            isRootActive = false;
            isRootEmpty = true;
            gameObject.SetActive(false);
        }
        //Root active ama dolu ise (a�a� var)
        else
        {
            //A�ac� b�y�k hale getiriyoruz
            newBody.transform.localScale = new Vector3(1, 2, 1);
            //Yok ediyoruz
            Destroy(newBody);
            Destroy(newBodyRootPrivate);
            //Rootu deaktif edip bo� oldu�unu belirtiyoruz
            isRootActive = false;
            isRootEmpty = true;
            gameObject.SetActive(false);
        }
        

    }
    //Click eventi ile rootu aktif ediyoruz.
    public void ActivateRoot()
    {
        //Main rootu aktif ediyoruz
        gameObject.SetActive(true);
        //Root i�erisinde bodyroot objesini spawnl�yoruz
        newBodyRootPrivate = Instantiate(treeRoot, new Vector3(this.transform.position.x, this.transform.position.y - 0.15f, this.transform.position.z), Quaternion.identity);
        newBodyRootPrivate.transform.SetParent(transform);
        isRootActive = true;
    }

    private void FixedUpdate()
    {
        

        //root aktifse ve bo� de�ilse �zerindeki a�ac� b�y�t�yoruz
        if(isRootActive && !isRootEmpty)
        {
            //Yapraklar� a�mak i�in skinnedmeshrendereri �ekiyorz
            skinRend = newBody.transform.GetChild(newBody.transform.childCount - 1).GetComponent<SkinnedMeshRenderer>();
            //A�a� b�y�kl��� 2 olana kadar b�y�t�yoruz
            if (newBody.transform.localScale.y <= 4.5f)
            {
                newBody.transform.localScale = new Vector3(newBody.transform.localScale.x, newBody.transform.localScale.y + 0.6f * gameManager.GetComponent<GameManager>().speedMultiply * Time.deltaTime, newBody.transform.localScale.z);
            }
            //A�a� b�y�kl��� 1.2 ye ula��nca 
            if(newBody.transform.localScale.y >= 2.5f)
            {
                //Yapraklar tamamen a��lana kadar b�y�t�yoruz.
                if(skinRend.GetBlendShapeWeight(0) >= 30)
                {
                    float deneme = skinRend.GetBlendShapeWeight(0) - 80 * gameManager.GetComponent<GameManager>().speedMultiply * Time.deltaTime;
                    skinRend.SetBlendShapeWeight(0, deneme);
                }
                
            }
        }

    }


}
