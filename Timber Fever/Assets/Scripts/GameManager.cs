using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject cameraMain;
    public GameObject cameraPosLv2;
    public GameObject cameraPosLv3;
    public GameObject cameraPosLv4;
    public GameObject cameraPosLv5;
    public float areaLv1 = 1f;
    public float areaLv2 = 1.5f;
    public float areaLv3 = 2f;
    public float areaLv4 = 2.5f;
    public float areaLv5 = 3f;
    public int sawLengthLv1 = 15;
    public int sawLengthLv2 = 50;
    public int sawLengthLv3 = 85;
    public int sawLengthLv4 = 120;
    public int sawLengthLv5 = 160;


    public GameObject groundTop;
    public Text moneyText;
    public Button mergeButton;
    public Button addRootButton;
    public List<GameObject> rootsLv1;
    public List<GameObject> rootsLv2;
    public List<GameObject> rootsLv3;
    public List<GameObject> rootsLv4;
    public List<GameObject> rootsLv5;
    public GameObject sawMain;
    private int rotationSpeed = 40;
    public int money;
    public bool canMerge = false;
    public bool moveCam = false;

    private bool level2isActive = false;
    private bool level3isActive = false;
    private bool level4isActive = false;
    private bool level5isActive = false;
    public float areaLength;
    public Vector3 targetCamPosition;




    private void Start()
    {
        //Oyun baþlangýcýnda ilk root aktif ediliyor
        money = 200;
        moneyText.text = money+"$";
        rootsLv1[0].GetComponent<root>().ActivateRoot();
    }

    
    public void addNewRoot(List<GameObject> _root)
    {
        //kaç adet root var ise dönecek loop
        for (int i = 0; i < _root.Count; i++)
        {
            //Para kontrol
            if (money >= 5)
            {
                //Root aktif deðilse
                if (!_root[i].GetComponent<root>().isRootActive)
                {
                    //Para eklenip root aktif hale geliyor
                    money -= 5;
                    moneyText.text = money + "$";
                    _root[i].GetComponent<root>().ActivateRoot();
                    break;
                }
            }
            else
            {
                Debug.Log("Para yetersiz");
            }
        }
    }

    //Butona týklandýðýnda lv1 e root ekleniyor
    public void rootButtonClick()
    {
        addNewRoot(rootsLv1);
    }

    public bool IsRootFull(List<GameObject> _roots)
    {
        if(_roots[0].GetComponent<root>().isRootActive && _roots[1].GetComponent<root>().isRootActive && _roots[2].GetComponent<root>().isRootActive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OpenNewLevel(int _level)
    {
        //Gelen level deðiþkenine göre levelleri açýyoruz
        if(_level == 2)
        {
            //boruyu uzatýyoruz
            sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, sawLengthLv2);
            //chain ana objesinin içerisindeki gerekli child olan testereyi aktif ediyoruz
            sawMain.transform.GetChild(1).gameObject.SetActive(true);
            //Cameranýn hareket etmesi için true yapýyoruz.
            moveCam = true;
            //alanýn açýlmasý için deðeri ayarlýyoruz
            areaLength = areaLv2;
            //target positionu set ediyoruz
            targetCamPosition = cameraPosLv2.transform.position;
            //level2 nin aktif olduðunu belirtiyoruz
            level2isActive = true;
            //Level2 den root açýyoruz.
        }
        else if (_level == 3)
        {
            sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, sawLengthLv3);
            sawMain.transform.GetChild(2).gameObject.SetActive(true);
            moveCam = true;
            areaLength = areaLv3;
            targetCamPosition = cameraPosLv3.transform.position;
            level3isActive = true;
        }
        else if (_level == 4)
        {
            sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, sawLengthLv4);
            sawMain.transform.GetChild(3).gameObject.SetActive(true);
            moveCam = true;
            areaLength = areaLv4;
            targetCamPosition = cameraPosLv4.transform.position;
            level4isActive = true;
        }
        else if (_level == 5)
        {
            sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, sawLengthLv5);
            sawMain.transform.GetChild(4).gameObject.SetActive(true);
            moveCam = true;
            areaLength = areaLv5;
            targetCamPosition = cameraPosLv5.transform.position;
            level5isActive = true;
            //rootsLv5[0].GetComponent<root>().ActivateRoot();
        }
    }
    public void CloseAllRoot (List<GameObject> _root)
    {
        _root[0].GetComponent<root>().CloseRoot();
        _root[1].GetComponent<root>().CloseRoot();
        _root[2].GetComponent<root>().CloseRoot();

    }


    //Merge yapma
    public void Merge()
    {
        if (canMerge)
        {
            //merge yapýldýðý için yapýlamaz oluyor.
            canMerge = false;
            //rootlarý resetliyoruz
            rootsLv1[0].GetComponent<root>().CloseRoot();
            rootsLv1[1].GetComponent<root>().CloseRoot();
            rootsLv1[2].GetComponent<root>().CloseRoot();
            rootsLv1[3].GetComponent<root>().CloseRoot();
            //butonu deaktif ediyoruz
            mergeButton.GetComponent<Button>().interactable = false;
            //level2 daha önce aktif deðil ise aktif ediliyor ve ilk aðaç ekleniyor
            if(!level2isActive)
            {
                OpenNewLevel(2);
            }
            //Level2 doluysa
            if (IsRootFull(rootsLv2))
            {
                //level2 kapatýlýyor
                CloseAllRoot(rootsLv2);
                //level3 aktif deðil ise açýlýyor
                if (!level3isActive)
                {
                    OpenNewLevel(3);
                }
                //level3 doluysa
                if (IsRootFull(rootsLv3))
                {
                    //level 3 kapatýlýyor
                    CloseAllRoot(rootsLv3);
                    //level4 aktif deðilse açýlýyor
                    if (!level4isActive)
                    {
                        OpenNewLevel(4);
                    }
                    //level4 doluysa
                    if (IsRootFull(rootsLv4))
                    {
                        //level4 kapatýlýyor
                        CloseAllRoot(rootsLv4);
                        //level5 aktif deðilse açýlýyor
                        if (!level5isActive)
                        {
                            OpenNewLevel(5);
                        }
                        if (IsRootFull(rootsLv5))
                        {
                            Debug.Log("Oyun bitti");
                        }
                        //level 5 full deðilse ekleniyor
                        else
                        {
                            addNewRoot(rootsLv5);
                        }
                    }
                    //level 4 full deðilse ekleniyor
                    else
                    {
                        addNewRoot(rootsLv4);
                    }
                }
                //level 3 full deðilse ekleniyor
                else
                {
                    addNewRoot(rootsLv3);
                }
            }
            //level 2 full deðilse ekleniyor
            else
            {
                addNewRoot(rootsLv2);
            }
        }
    }

    private void Update()
    {
        

        if(money >= 5)
        {
            addRootButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            addRootButton.GetComponent<Button>().interactable = false;
        }
        //Tüm rootlar aktif ve merge butonu aktif deðilse kontrol edip, merge olabilir durumu
        if(rootsLv1[0].GetComponent<root>().isRootActive && rootsLv1[1].GetComponent<root>().isRootActive && rootsLv1[2].GetComponent<root>().isRootActive && rootsLv1[3].GetComponent<root>().isRootActive && !canMerge)
        {
            canMerge = true;
            mergeButton.GetComponent<Button>().interactable = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Kamera hareketi
        if (moveCam)
        {
            if (groundTop.transform.localScale.x <= areaLength)
            {
                groundTop.transform.localScale = new Vector3(groundTop.transform.localScale.x + 0.5f * Time.deltaTime, groundTop.transform.localScale.y + 0.5f * Time.deltaTime, groundTop.transform.localScale.z + 0.5f * Time.deltaTime);
            }
            cameraMain.transform.position = Vector3.Lerp(cameraMain.transform.position, targetCamPosition, 3 * Time.deltaTime);
            if (cameraMain.transform.position == targetCamPosition)
            {
                moveCam = false;
            }
        }



        //testere hareketi
        sawMain.transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
    }
}
