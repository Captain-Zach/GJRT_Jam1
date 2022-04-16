using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxHandler : MonoBehaviour
{
    [SerializeField] HitboxType hitboxType;
    private bool HitboxFlooded;
    // Start is called before the first frame update
    void Start()
    {
        HitboxFlooded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHitboxContact()
    {
        HitboxHandler[] allHitboxesfromParent = transform.parent.GetComponentsInChildren<HitboxHandler>();
        HitboxFlooded = true;

        switch (hitboxType)
        {
            case HitboxType.Village:
                foreach (HitboxHandler hitboxHandler in allHitboxesfromParent)
                {
                    hitboxHandler.HitboxFlooded = true;
                }
                Debug.Log("Village flooded");

                break;
            case HitboxType.City:
                bool allFlooded = true;
                foreach (HitboxHandler hitboxHandler in allHitboxesfromParent)
                {
                    if (!hitboxHandler.HitboxFlooded)
                    {
                        allFlooded = false;
                        Debug.Log("City not flooded completely");
                    }
                }
                if (allFlooded)
                {
                    Debug.Log("City flooded");
                }

                break;
            case HitboxType.BeaverHouse:
                Debug.Log("You lose!");

                break;
        }
    }

    public enum HitboxType
    {
        Village = 0,
        City = 1,
        BeaverHouse = 2
    }
}
