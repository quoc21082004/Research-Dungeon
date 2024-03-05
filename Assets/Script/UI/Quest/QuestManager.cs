using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public int currentQuest;    
    public int maxQuest => 3;
}
