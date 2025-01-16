using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcUIStates : MonoBehaviour
{
    [Header("Npcs")]

    public List<TMP_Text> npcTextObjs;

    public NpcAIManager npcAIManager;


    [Header("Prefabs")]

    public TMP_Text prefabNpcStateText;


    void FixedUpdate()
    {
        //Check for difference in text objects and npcs.
        while (npcTextObjs.Count != npcAIManager.npcs.Count)
        {
            //Check if there are more text objects than npcs.
            if (npcTextObjs.Count < npcAIManager.npcs.Count)
            {
                AddUIText();
            }
            else
            {
                RemoveUIText();
            }
        }

        for (int i = 0; i < npcTextObjs.Count; ++i)
        {
            NpcAI npc = npcAIManager.npcs[i];

            npcTextObjs[i].transform.position = Camera.main.WorldToScreenPoint(npc.transform.position + Vector3.up * 2);
            npcTextObjs[i].text = npc.behaviour.startNode.name;
        }
    }

    void AddUIText()
    {
        TMP_Text textObj = Instantiate(prefabNpcStateText);
        textObj.transform.SetParent(transform, false);
        
        npcTextObjs.Add(textObj);
    }

    void RemoveUIText()
    {
        TMP_Text lastTextObj = npcTextObjs[^1];
        Destroy(lastTextObj.gameObject);

        npcTextObjs.RemoveAt(npcTextObjs.Count - 1);
    }
}
