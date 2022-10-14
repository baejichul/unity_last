using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class NetMonster : NetworkBehaviour
{
    SpriteRenderer _sr;

    void Start()
    {
        SetDefaultSortOrder();
    }

    void SetDefaultSortOrder()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (_sr != null)
            _sr.sortingOrder = 1;
    }

}
