using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public int sawLengthLv1 = 20;
    public int sawLengthLv2 = 53;
    public int sawLengthLv3 = 88;
    public int sawLengthLv4 = 124;
    public int sawLengthLv5 = 161;

    

    public GameObject groundTop;
    public Text moneyText;
    public Button mergeButton;
    public Button addRootButton;
    public Button sizeUpButton;
    public Button incomeButton;
    public Text treeMoneyText;
    public Text expandMoneyText;
    public Text incomeMoneyText;
    public Text incomeMultiText;
    public GameObject maxLevelobj;
    public List<GameObject> rootsLv1;
    public List<GameObject> rootsLv2;
    public List<GameObject> rootsLv3;
    public List<GameObject> rootsLv4;
    public List<GameObject> rootsLv5;
    public GameObject sawMain;
    public int rotationSpeed = 40;
    //A�a� b�y�mesini ve testere h�z�n� kontrol eder
    public int speedMultiply = 1;
    public float money;
    public float expandMoney = 100f;
    public float treeMoney = 15f;
    public float incomeMoney = 30f;
    public float expandMultiply;
    public float incomeMultiply;
    public float treeMultiply;
    public bool canMerge = false;
    public bool moveCam = false;

    private bool level2isActive = false;
    private bool level3isActive = false;
    private bool level4isActive = false;
    private bool level5isActive = false;
    public float areaLength;
    public Vector3 targetCamPosition;
    private Vector3 offSet = new Vector3(0.05f, 0.05f, -0.05f);


    private List<bool> isActiveList = new List<bool>();
    private List<List<GameObject>> rootsList = new List<List<GameObject>>();

    //private List<bool> isActiveList;
    //private List<List<GameObject>> rootsList;




    private void Start()
    {
        //Merge kontrol i�in aktiflikleri ve rootlar� listlere ekliyorum
        isActiveList.Add(level2isActive);
        isActiveList.Add(level3isActive);
        isActiveList.Add(level4isActive);
        isActiveList.Add(level5isActive);
        rootsList.Add(rootsLv2);
        rootsList.Add(rootsLv3);
        rootsList.Add(rootsLv4);
        rootsList.Add(rootsLv5);

        expandMoneyText.text = "100 $";
        treeMoneyText.text = "15 $";
        incomeMoneyText.text = "30 $";

        //Oyun ba�lang�c�nda ilk root aktif ediliyor ve de�erler atan�yor
        money = 0;
        expandMultiply = 1f;
        treeMultiply = 1f;
        incomeMultiply = 1f;
        incomeMultiText.text = "X" + incomeMultiply.ToString("f1");
        moneyText.text = money+"$";
        rootsLv1[0].GetComponent<root>().ActivateRoot();
    }

    
    public void addNewRoot(List<GameObject> _root)
    {
        //ka� adet root var ise d�necek loop
        for (int i = 0; i < _root.Count; i++)
        {
            //Para kontrol
            if (money >= treeMoney * treeMultiply)
            {
                //Root aktif de�ilse
                if (!_root[i].GetComponent<root>().isRootActive)
                {
                    //Para eksilip root aktif hale geliyor
                    money -= treeMoney * treeMultiply;
                    moneyText.text = money.ToString("f0") + "$";
                    _root[i].GetComponent<root>().ActivateRoot();
                    break;
                }
            }
            //else
            //{
            //    Debug.Log("Para yetersiz");
            //}
        }
    }

    //Butona t�kland���nda lv1 e root ekleniyor
    public void rootButtonClick()
    {
        addNewRoot(rootsLv1);
        treeMultiply = 1.1f;
        treeMoney = treeMoney * treeMultiply;
        treeMoneyText.text = treeMoney.ToString("f0");

    }

    public bool IsRootFull(List<GameObject> _roots)
    {
        //level1 i�in �zel durum.. 4 a��k root olmas� gerekiyor
        if(_roots == rootsLv1)
        {
            if(_roots[0].GetComponent<root>().isRootActive && _roots[1].GetComponent<root>().isRootActive && _roots[2].GetComponent<root>().isRootActive && _roots[3].GetComponent<root>().isRootActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //check edilen root level 1 de�ilse 3 tane olup olmad���na bak�l�yor.
        else
        {
            if (_roots[0].GetComponent<root>().isRootActive && _roots[1].GetComponent<root>().isRootActive && _roots[2].GetComponent<root>().isRootActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }

    public void OpenNewLevel(int _level)
    {
        //Gelen level de�i�kenine g�re levelleri a��yoruz
        if(_level == 2)
        {
            //boruyu uzat�yoruz
            sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, sawLengthLv2);
            //chain ana objesinin i�erisindeki gerekli child olan testereyi aktif ediyoruz
            sawMain.transform.GetChild(1).gameObject.SetActive(true);
            //Cameran�n hareket etmesi i�in true yap�yoruz.
            moveCam = true;
            //alan�n a��lmas� i�in de�eri ayarl�yoruz
            areaLength = areaLv2;
            //target positionu set ediyoruz
            targetCamPosition = cameraPosLv2.transform.position;
            //level2 nin aktif oldu�unu belirtiyoruz
            isActiveList[0] = true;
            //Level2 den root a��yoruz.
        }
        else if (_level == 3)
        {
            sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, sawLengthLv3);
            sawMain.transform.GetChild(2).gameObject.SetActive(true);
            moveCam = true;
            areaLength = areaLv3;
            targetCamPosition = cameraPosLv3.transform.position;
            isActiveList[1] = true;
        }
        else if (_level == 4)
        {
            sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, sawLengthLv4);
            sawMain.transform.GetChild(3).gameObject.SetActive(true);
            moveCam = true;
            areaLength = areaLv4;
            targetCamPosition = cameraPosLv4.transform.position;
            isActiveList[2] = true;
        }
        else if (_level == 5)
        {
            sawMain.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, sawLengthLv5);
            sawMain.transform.GetChild(4).gameObject.SetActive(true);
            moveCam = true;
            areaLength = areaLv5;
            targetCamPosition = cameraPosLv5.transform.position;
            isActiveList[3] = true;
            //rootsLv5[0].GetComponent<root>().ActivateRoot();
        }
    }
    public void CloseAllRoot (List<GameObject> _root)
    {
        _root[0].GetComponent<root>().CloseRoot();
        _root[1].GetComponent<root>().CloseRoot();
        _root[2].GetComponent<root>().CloseRoot();

    }

    public void IncreaseIncome()
    {
        money -= incomeMoney * incomeMultiply;
        moneyText.text = money.ToString("f0") + "$";
        incomeMultiply += 0.1f;
        incomeMoney = incomeMoney * incomeMultiply;
        incomeMultiText.text = "X" + incomeMultiply.ToString("f1");
        incomeMoneyText.text = (incomeMoney * incomeMultiply).ToString("f0");

    }

    //Merge yapma
    public void Merge()
    {
        if (canMerge)
        {
            //merge yap�ld��� i�in yap�lamaz oluyor.
            canMerge = false;
            //rootlar� resetliyoruz
            rootsLv1[0].GetComponent<root>().CloseRoot();
            rootsLv1[1].GetComponent<root>().CloseRoot();
            rootsLv1[2].GetComponent<root>().CloseRoot();
            rootsLv1[3].GetComponent<root>().CloseRoot();
            //butonu deaktif ediyoruz
            mergeButton.GetComponent<Button>().interactable = false;
            mergeButton.gameObject.SetActive(false);
            //level2 a��ksa ve dolu de�ilse
            if (isActiveList[0] && !IsRootFull(rootsLv2))
            {
                addNewRoot(rootsLv2);
            }
            //level2 doluysa
            else
            {
                CloseAllRoot(rootsLv2);

                if (isActiveList[1] && !IsRootFull(rootsLv3))
                {
                    addNewRoot(rootsLv3);
                }
                //level3 doluysa
                else
                {
                    CloseAllRoot(rootsLv3);

                    if (isActiveList[2] && !IsRootFull(rootsLv4))
                    {
                        addNewRoot(rootsLv4);
                    }
                    //level 4 doluysa
                    else
                    {
                        CloseAllRoot(rootsLv4);

                        if (isActiveList[3] && !IsRootFull(rootsLv5))
                        {
                            addNewRoot(rootsLv5);
                        }
                        //level5 doluysa
                        else
                        {
                            Debug.Log("Oyun bitti merge i�i");
                        }
                    }
                }
                
            }
            
        }
    }

    private void ChangeColour(Button _obj,byte _colour)
    {
        _obj.transform.GetChild(0).GetComponent<Text>().color = new Color32(_colour, _colour, _colour, 255);
        _obj.transform.GetChild(2).GetComponent<Image>().color = new Color32(_colour, _colour, _colour, 255);
        _obj.transform.GetChild(3).GetComponent<Text>().color = new Color32(_colour, _colour, _colour, 255);
        _obj.transform.GetChild(4).GetComponent<Image>().color = new Color32(_colour, _colour, _colour, 255);
    }

    private void Update()
    {
        //sizeup al�nabilir 5. level a��ksa ba�ka level kalmad��� i�in al�namaz olucak.
        if(money >= expandMoney * expandMultiply && !isActiveList[3] && !gameObject.GetComponent<AccelerateChainSaw>().isMovingBack)
        {
            ChangeColour(sizeUpButton, 255);
            sizeUpButton.interactable = true;
        }
        else
        {
            //level 5 aktif ise son level oldu�u i�in image i a��yorum.
            if (isActiveList[3])
            {
                maxLevelobj.SetActive(true);
            }
            ChangeColour(sizeUpButton, 100);
            sizeUpButton.interactable = false;
        }
        //Income button aktiflik
        if(money >= incomeMoney * incomeMultiply)
        {
            ChangeColour(incomeButton, 255);
            incomeButton.interactable = true;
        }
        else
        {
            ChangeColour(incomeButton, 100);
            incomeButton.transform.GetChild(5).GetComponent<Text>().color = new Color32(100, 100, 100, 255);
            incomeButton.interactable = false;
        }

        //addtree al�nabilir
        if (money >= treeMoney * treeMultiply && !canMerge && !IsRootFull(rootsLv1))
        {
            ChangeColour(addRootButton, 255);
            addRootButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            ChangeColour(addRootButton, 100);
            addRootButton.GetComponent<Button>().interactable = false;
        }
        //merge durumu level1 fullse
        if (IsRootFull(rootsLv1) && !canMerge)
        {
            //level2 a��ksa
            if (isActiveList[0])
            {
                //level2 dolu de�ilse
                if (!IsRootFull(rootsList[0]))
                {
                    canMerge = true;
                    mergeButton.gameObject.SetActive(true);
                    mergeButton.GetComponent<Button>().interactable = true;
                }
                //level2 doluysa
                else
                {
                    //level3 a��ksa
                    if (isActiveList[1])
                    {
                        //level3 dolu de�ilse
                        if (!IsRootFull(rootsList[1]))
                        {
                            canMerge = true;
                            mergeButton.gameObject.SetActive(true);
                            mergeButton.GetComponent<Button>().interactable = true;
                        }
                        //level3 doluysa
                        else
                        {
                            //level4 a��ksa
                            if (isActiveList[2])
                            {
                                //level4 dolu de�ilse
                                if (!IsRootFull(rootsList[2]))
                                {
                                    canMerge = true;
                                    mergeButton.gameObject.SetActive(true);
                                    mergeButton.GetComponent<Button>().interactable = true;
                                }
                                //level 4 doluysa
                                else
                                {
                                    //level5 a��ksa
                                    if (isActiveList[3])
                                    {
                                        //level5 dolu de�ilse
                                        if (!IsRootFull(rootsList[3]))
                                        {
                                            canMerge = true;
                                            mergeButton.gameObject.SetActive(true);
                                            mergeButton.GetComponent<Button>().interactable = true;
                                        }
                                        else
                                        {
                                            Debug.Log("Oyun bitti");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
        }
    }



    public void ExpandLevel()
    {
        //para yetiyorsa
        if(money >= expandMoney * expandMultiply)
        {
            money -= expandMoney * expandMultiply;
            expandMultiply += 1;
            moneyText.text = money.ToString("f0") + "$";
            expandMoneyText.text = (expandMoney * expandMultiply).ToString("f0") + "$";
            

            //level2 a��k de�ilse a��yoruz
            if (!isActiveList[0])
            {
                OpenNewLevel(2);
            }
            else
            {
                //level2 a��k ve 3 a��k de�ilse
                if (!isActiveList[1])
                {
                    OpenNewLevel(3);
                }
                else
                {
                    //level3 a��k ve 4 a��k de�ilse
                    if (!isActiveList[2])
                    {
                        OpenNewLevel(4);
                    }
                    else
                    {
                        //level4 a��k ve 5 a��k de�ilse
                        if (!isActiveList[3])
                        {
                            OpenNewLevel(5);
                        }
                    }
                }
            }
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
            if (cameraMain.transform.position.y > targetCamPosition.y - offSet.y)
            {
                
                moveCam = false;
            }
        }
        //testere hareketi
        sawMain.transform.Rotate(rotationSpeed * speedMultiply * Time.deltaTime * Vector3.up);
    }
}
