using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : Singletone<InteractionUI>
{
    [SerializeField] Text shortcutText;
    [SerializeField] Text interactionText;

    GameObject[] childs;
    
    void Start()
    {
        childs = new GameObject[transform.childCount];
        for(int i = 0; i < childs.Length; i++)
        {
            childs[i] = transform.GetChild(i).gameObject;
        }

        SwitchChild(false);
    }

    private void SwitchChild(bool isActive)
    {
        foreach(GameObject child in childs)
            child.SetActive(isActive);
    }

    public void SetInteractionUI(KeyCode shortKey, Item item)
    {
        shortcutText.text = shortKey.ToString();
        interactionText.text = string.Format("{0} ащ╠Б", item.ToString());
        SwitchChild(true);
    }
    public void CloseUI()
    {
        SwitchChild(false);
    }
}
