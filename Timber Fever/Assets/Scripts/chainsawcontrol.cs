using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainsawcontrol : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject floatingtext;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "tree")
        {

            //Bodynin rigidbodysini ve colliderýný çekiyoruz.
            Rigidbody treerb = other.GetComponent<Rigidbody>();
            CapsuleCollider treecol = other.GetComponent<CapsuleCollider>();
            //Bodynin kesildikten sonra istenilen noktaya düþmesi için kuvvet uyguluyoruz.
            treerb.AddForce(transform.forward * 50);
            treerb.AddForce(transform.up * 15);
            //Para textini aðacýn rewardý olarak güncelliyoruz.
            floatingtext.GetComponent<TextMesh>().text = other.GetComponent<Trees>().moneyReward * gameManager.GetComponent<GameManager>().incomeMultiply + "$";
            //Para textini oluþturuyoruz 
            Instantiate(floatingtext, other.transform.position, Quaternion.identity);
            //Aðaç kesildiði için parayý arttýrýyoruz.
            gameManager.GetComponent<GameManager>().money += other.GetComponent<Trees>().moneyReward * gameManager.GetComponent<GameManager>().incomeMultiply;
            gameManager.GetComponent<GameManager>().moneyText.text = gameManager.GetComponent<GameManager>().money.ToString("f0") + "$";
            //Rootun artýk boþ olduðunu belirtiyoruz ve parenttan ayýrýyoruz.
            other.GetComponentInParent<root>().isRootEmpty = true;
            //Destroy(other.transform.parent.gameObject); //Pivot boþ objeyi siliyoruz
            other.transform.SetParent(null);
            //Bodynin düþmesi için gravity açýyoruz ve trigger özelliðini kapatýyoruz.
            treerb.useGravity = true;
            treecol.isTrigger = false;
        }

    }
}
