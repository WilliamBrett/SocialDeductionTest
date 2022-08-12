using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorScript : MonoBehaviour
{
    private CoreGameScript CoreScript;
    private Image ActorImage;
    public int ActorId;
    public GameObject ankh;

    public void Start()
    {
        //Due to how actors are treated, Start() cannot be used conventionally for some neccesary configurations
        CoreScript = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<CoreGameScript>();
        //ActorImage = GetComponent<Image>();
    }

    public void SetActorId(int ID) => ActorId = ID;

    public void SetIcon(Sprite swapIn) => GetComponent<Image>().sprite = swapIn;

    public void ButtonClicked() => CoreScript.ButtonClicked(ActorId);

    public void SetAnkh(bool isActive) => ankh.SetActive(isActive);
}
