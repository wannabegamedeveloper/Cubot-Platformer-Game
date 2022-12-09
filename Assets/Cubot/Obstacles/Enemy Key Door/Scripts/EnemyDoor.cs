using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    public int index;

    public void OpenDoor()
    {
        if (index == 0)
            GetComponent<Animator>().Play("Open Door", -1, 0f);
    }
}
