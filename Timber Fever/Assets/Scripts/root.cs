using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class root : MonoBehaviour
{
    //prefab aðaç
    public GameObject treeBody;
    //prefab aðaç kök
    public GameObject treeRoot;
    //game manager objesi
    public GameObject gameManager;
    //spawn edilen aðaç
    private GameObject newBody;
    //yapraklarýn açýlmasýný tutan script
    private SkinnedMeshRenderer skinRend;
    private GameObject newBodyRootPrivate;

    //Rootun doluluk/aktiflik durumu
    public bool isRootEmpty = true;
    public bool isRootActive = false;


    private void OnTriggerExit(Collider other)
    {
        //Chainsaw node üzerinden çýktýðý zaman
        if(other.gameObject.tag == "chainsaw")
        {
            //Root aktif ise aðacý oluþturuyoruz
            if (isRootActive)
            {
                //Aðacý rootun üzerine spawn ediyoruz
                newBody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.12f, transform.position.z), Quaternion.identity);
                //Aðacýn parentini root yapýyoruz
                newBody.transform.SetParent(transform);
                //Aðacý aktif hale getiriyoruz
                newBody.SetActive(true);
                //Rootun artýk dolu olduðunu söylüyoruz
                isRootEmpty = false;
            }
            
        }
    }

    //Click eventi ile root kapatma
    public void CloseRoot()
    {
        //Root active ve boþ ise
        if (isRootActive && isRootEmpty)
        {
            //Root boþ olduðu için bodyi oluþturuyoruz
            newBody = Instantiate(treeBody, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
            //Büyümüþ haline getiriyoruz
            newBody.transform.localScale = new Vector3(1, 2, 1);
            //Bodyi yok ediyoruz
            Destroy(newBody);
            Destroy(newBodyRootPrivate);
            //Rootu deaktif edip boþ olduðunu belirtiyoruz
            isRootActive = false;
            isRootEmpty = true;
            //Parayý ekleyip ekrana yazdýrýyoruz
            gameManager.GetComponent<GameManager>().money += 5;
            gameManager.GetComponent<GameManager>().moneyText.text = gameManager.GetComponent<GameManager>().money + "$";
            gameObject.SetActive(false);
        }
        //Root active ama dolu ise (aðaç var)
        else
        {
            //Aðacý büyük hale getiriyoruz
            newBody.transform.localScale = new Vector3(1, 2, 1);
            //Yok ediyoruz
            Destroy(newBody);
            Destroy(newBodyRootPrivate);
            //Rootu deaktif edip boþ olduðunu belirtiyoruz
            isRootActive = false;
            isRootEmpty = true;
            //Parayý ekleyip ekrana yazdýrýyoruz
            gameManager.GetComponent<GameManager>().money += 5;
            gameManager.GetComponent<GameManager>().moneyText.text = gameManager.GetComponent<GameManager>().money + "$";
            gameObject.SetActive(false);
        }
        

    }
    //Click eventi ile rootu aktif ediyoruz.
    public void ActivateRoot()
    {
        //Main rootu aktif ediyoruz
        gameObject.SetActive(true);
        //Root içerisinde bodyroot objesini spawnlýyoruz
        newBodyRootPrivate = Instantiate(treeRoot, new Vector3(this.transform.position.x, this.transform.position.y - 0.15f, this.transform.position.z), Quaternion.identity);
        newBodyRootPrivate.transform.SetParent(transform);
        isRootActive = true;
    }

    private void FixedUpdate()
    {
        

        //root aktifse ve boþ deðilse üzerindeki aðacý büyütüyoruz
        if(isRootActive && !isRootEmpty)
        {
            //Yapraklarý açmak için skinnedmeshrendereri çekiyorz
            skinRend = newBody.transform.GetChild(newBody.transform.childCount - 1).GetComponent<SkinnedMeshRenderer>();
            //Aðaç büyüklüðü 2 olana kadar büyütüyoruz
            if (newBody.transform.localScale.y <= 4.5f)
            {
                newBody.transform.localScale = new Vector3(newBody.transform.localScale.x, newBody.transform.localScale.y + 0.6f * gameManager.GetComponent<GameManager>().speedMultiply * Time.deltaTime, newBody.transform.localScale.z);
            }
            //Aðaç büyüklüðü 1.2 ye ulaþýnca 
            if(newBody.transform.localScale.y >= 2.5f)
            {
                //Yapraklar tamamen açýlana kadar büyütüyoruz.
                if(skinRend.GetBlendShapeWeight(0) >= -35)
                {
                    float deneme = skinRend.GetBlendShapeWeight(0) - 80 * gameManager.GetComponent<GameManager>().speedMultiply * Time.deltaTime;
                    skinRend.SetBlendShapeWeight(0, deneme);
                }
                
            }
        }

    }


}
