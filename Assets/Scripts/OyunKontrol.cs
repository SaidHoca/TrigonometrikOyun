using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    public Button dogru, yanlis;
    public Animator HomePanelAnim, OyunPanelAnim, SonPanelAnim, infoPanelAnim, dogruAnim, yanlisAnim;
    public Text puanText, sonText, sonPuanText;
    public Image resim, yildiz;
    public Sprite[] ozdeslikler;
    public Sprite[] dikucgen;
    public Sprite[] bolge;
    public Sprite Yildizsiz, Biryildiz, Ikiyildiz, Ucyildiz;
    public int puan = 0;
    int eklenecekpuan = 5;
    int eksilecekpuan = 3;
    int sorusayisi = 20;
    int dogrusayisi = 0;
    int yanlissayisi = 0;
    int oyuntipi = 0;
    float yildizBelirleyici = 0;
    int resimsayaci = 0;
    private bool oyunDevam;
    AudioSource[] sesler;


    void Start()
    {
        oyunDevam = true;
        sesler = GetComponents<AudioSource>();
    }


    public void ResmiDegistir()
    {
        if (oyunDevam)
        {
            if (oyuntipi == 1)  // özdeşlikler bölümü
            {
                if (resimsayaci < 19)
                {
                    resim.sprite = ozdeslikler[resimsayaci];
                    resimsayaci++;
                }
                else
                {
                    OyunuBitir();
                }

                //resim.SetNativeSize();
            }
            else if (oyuntipi == 2) // dik üçgen bölümü
            {
                if (resimsayaci < 14)
                {
                    resim.sprite = dikucgen[resimsayaci];
                    resimsayaci++;
                }
                else
                {
                    OyunuBitir();
                }

                //resim.rectTransform.sizeDelta = new Vector2(dikucgen[sayi].rect.width, dikucgen[sayi].rect.height);
                //Debug.Log(resim.sprite.bounds.size.x);
                //Debug.Log(dikucgen[sayi].rect.width);                          
                //resim.SetNativeSize();
            }
            else if (oyuntipi == 3) // bölge dönüşümleri bölümü 
            {
                int sayi = Random.Range(0, 32);
                resim.sprite = bolge[sayi];
                //resim.SetNativeSize();
            }
        }
        // TIKLAYINCA RESMİN DEĞİŞMESİNİ SAĞLAYACAK..

    }

    public void DogruButonResimKontrolEt()
    {
        // SORU SAYISI VE PUANLAMAYI AYARLAMALIYIZ...
        // ÖNCE KONTROLLERİ YAPMALIYIZ.
        if (oyunDevam)
        {
            if (dogrusayisi + yanlissayisi == sorusayisi)
            {
                OyunuBitir();
            }
            else
            {
                if (resim.sprite.name.Substring(0, 1) == "d")
                {
                    PuanArtır();

                }
                else
                {
                    PuanAzalt();

                }

                ResmiDegistir();
            }
        }

        // RESMİN YANLIŞ MI YOKSA DOĞRU MU OLDUĞUNA KARAR VERECEK...       
    }

    public void YanlisButonResimKontrolEt()
    {
        if (dogrusayisi + yanlissayisi == sorusayisi)
        {
            OyunuBitir();
        }
        else
        {
            if (resim.sprite.name.Substring(0, 1) == "y")
            {
                PuanArtır();

            }
            else
            {
                PuanAzalt();

            }

            ResmiDegistir();
        }
    }


    public void PuanArtır()
    {
        puan = puan + eklenecekpuan;
        dogrusayisi = dogrusayisi + 1;
        puanText.text = "Puan : " + puan;
        sesler[0].Play();
        dogruAnim.SetTrigger("DogruAnim");
        // ekranda yazan puan yazısını falan değiştir...
    }

    public void PuanAzalt()
    {
        puan = puan - eksilecekpuan;
        yanlissayisi = yanlissayisi + 1;
        puanText.text = "Puan : " + puan;
        sesler[1].Play();
        yanlisAnim.SetTrigger("YanlisAnim");
    }

    public void OyunuBitir()
    {
        // bir panel gelsin sayfaya.. burada toplam doğru ve yanlış sayıları
        // ile puan gösterilsin..
        oyuntipi = 0;
        oyunDevam = false;
        yildizBelirleyici = (dogrusayisi / (float)(yanlissayisi + dogrusayisi)) * 100; // dogrusayısı oranını yüzdelik olarak verecek
        if (yildizBelirleyici < 30)
        {
            yildiz.sprite = Yildizsiz;
            sonText.text = "Ne desem bilemedim :(";
            sonPuanText.text = "Puanınız : " + puan;

        }
        else if (yildizBelirleyici < 50)
        {
            yildiz.sprite = Biryildiz;
            sonText.text = "Daha iyi olabilirsin.";
            sonPuanText.text = "Puanınız : " + puan;
        }
        else if (yildizBelirleyici < 75)
        {
            yildiz.sprite = Ikiyildiz;
            sonText.text = "Tebrikler !!!";
            sonPuanText.text = "Puanınız : " + puan;
        }
        else
        {
            yildiz.sprite = Ucyildiz;
            sonText.text = "Harikasınnnn....";
            sonPuanText.text = "Puanınız : " + puan;
        }
        OyunPanelAnim.SetBool("OyunAlaniAnim", false);
        SonPanelAnim.SetBool("SonPanel", true);

    }


    public void NewGame()
    {
        puan = 0;
        dogrusayisi = 0;
        yanlissayisi = 0;
        yildizBelirleyici = 0;
        resimsayaci = 0;
        puanText.text = "Puan : " + puan;
        ResmiDegistir();


    }

    public void HompePage()
    {
        HomePanelAnim.SetBool("HomePanel", true);
        OyunPanelAnim.SetBool("OyunAlaniAnim", false);
        SonPanelAnim.SetBool("SonPanel", false);
        puan = 0;
        dogrusayisi = 0;
        yanlissayisi = 0;
        resimsayaci = 0;
        yildizBelirleyici = 0;
        puanText.text = "Puan : " + puan;
        oyuntipi = 0;
        ResmiDegistir();
    }

    public void OzdesliklerBtn()
    {
        oyuntipi = 1;
        HomePanelAnim.SetBool("HomePanel", false);
        OyunPanelAnim.SetBool("OyunAlaniAnim", true);
        resimsayaci = 0;
        oyunDevam = true;
        ResmiDegistir();

    }

    public void DikUzgenBtn()
    {
        oyuntipi = 2;
        HomePanelAnim.SetBool("HomePanel", false);
        OyunPanelAnim.SetBool("OyunAlaniAnim", true);
        resimsayaci = 0;
        oyunDevam = true;
        ResmiDegistir();


    }

    public void BolgeDonusumBtn()
    {
        oyuntipi = 3;

        HomePanelAnim.SetBool("HomePanel", false);
        OyunPanelAnim.SetBool("OyunAlaniAnim", true);
        oyunDevam = true;
        ResmiDegistir();
    }

    public void InfoButon()
    {
        infoPanelAnim.SetBool("InfoPanel", true);

    }
    public void InfoHomeBtn()
    {
        infoPanelAnim.SetBool("InfoPanel", false);
    }

    public void Cikis()
    {
        Application.Quit();
    }
}