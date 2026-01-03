using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleEnlarger : MonoBehaviour
{
    [SerializeField] private float _enlargeingCount;

    private void Update()
    {
        Enlarge();
    }

    private void Enlarge()
    {
        Vector3 enlargeCounter = new Vector3(+_enlargeingCount, +_enlargeingCount, +_enlargeingCount) * Time.deltaTime;
        Vector3 newScale = transform.localScale + enlargeCounter;

        transform.localScale = newScale;
    }
}
