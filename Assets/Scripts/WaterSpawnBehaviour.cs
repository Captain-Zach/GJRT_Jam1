using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawnBehaviour : MonoBehaviour
{
    [SerializeField] GameObject WaterBlockPrefab;
    [SerializeField] GameObject SelfPrefab;
    private bool waterLimitReached;
    // Start is called before the first frame update
    void Start()
    {
        waterLimitReached = false;
        transform.position.Set(transform.position.x, transform.position.y, -1);
        foreach (Transform transformChildren in GetComponentsInChildren<Transform>())
        {
            if (transformChildren == transform) continue;
            Destroy(transformChildren.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CreateStream(this.transform);
    }

    private void CreateStream(Transform startingEntity)
    {
        Transform[] waterArray = startingEntity.GetComponentsInChildren<Transform>();

        /*
        Debug.Log(waterArray.Length);
        Debug.Log(startingEntity.position);
        Debug.Log(startingEntity.name);
        Debug.Log(startingEntity.GetComponent<WaterSpawnBehaviour>().waterLimitReached);
        */

        if (waterArray.Length <= 4 && !startingEntity.GetComponent<WaterSpawnBehaviour>().waterLimitReached)
        {
            GameObject createdWater = Instantiate(WaterBlockPrefab, startingEntity);
            createdWater.transform.localPosition = new Vector3(0, -1f * waterArray.Length, -1);
            createdWater.name = "Water Block " + waterArray.Length.ToString();

            //Debug.Log(createdWater.transform.position);

            Collider2D[] colliders = Physics2D.OverlapBoxAll(createdWater.transform.position, new Vector2(0.9f, 0.9f), 0f);
            
            if (colliders.Length > 0)
            {
                foreach (Collider2D collider in colliders)
                {
                    //Edgecases for generation

                    //Ignore player (should be removed soon)
                    if (collider.name == "Player" && colliders.Length == 1) return;

                    //Don't create new stream in case of Wood blocking first block
                    if (collider.name.Contains("Wood") && waterArray.Length == 1)
                    {
                        startingEntity.GetComponent<WaterSpawnBehaviour>().waterLimitReached = true;
                        Destroy(createdWater);
                        return;
                    }

                    //Village/City/Beaver
                    if (collider.name.Contains("Hitbox"))
                    {
                        collider.GetComponent<HitboxHandler>().OnHitboxContact();
                        return;
                    }
                }

                startingEntity.GetComponent<WaterSpawnBehaviour>().waterLimitReached = true;
                //Debug.Log("Collided with success");
                if (Physics2D.OverlapBoxAll(createdWater.transform.position + new Vector3(1, 1, 0), new Vector2(0.9f, 0.9f), 0f).Length == 0)
                {
                    GameObject newWaterStream = Instantiate(SelfPrefab, createdWater.transform.position + new Vector3(1, 2, 0), startingEntity.rotation);
                    
                    //Debug.Log("Right can recieve water");
                }
                if (Physics2D.OverlapBoxAll(createdWater.transform.position + new Vector3(-1, 1, 0), new Vector2(0.9f, 0.9f), 0f).Length == 0)
                {
                    GameObject newWaterStream = Instantiate(SelfPrefab, createdWater.transform.position + new Vector3(-1, 2, 0), startingEntity.rotation);

                    //Debug.Log("Left can recieve water");
                }
                Destroy(createdWater);
            }
        }
    }
}
