using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class WaterSpawnBehaviour : MonoBehaviour
{
    [SerializeField] GameObject WaterBlockPrefab;
    [SerializeField] GameObject SelfPrefab;
    [SerializeField] int maxRange;
    private bool waterLimitReached;
    private int waterMaximum;
    // Start is called before the first frame update
    void Start()
    {
        waterLimitReached = false;
        transform.position.Set(transform.position.x, transform.position.y, 0);
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

        if (waterMaximum == 0) waterMaximum = maxRange;

        /*
        Debug.Log(waterArray.Length);
        Debug.Log(startingEntity.position);
        Debug.Log(startingEntity.name);
        Debug.Log(startingEntity.GetComponent<WaterSpawnBehaviour>().waterLimitReached);
        */

        if (waterArray.Length <= waterMaximum && !startingEntity.GetComponent<WaterSpawnBehaviour>().waterLimitReached)
        {
            GameObject createdWater = Instantiate(WaterBlockPrefab, startingEntity);
            createdWater.transform.localPosition = new Vector3(0, -1f * waterArray.Length, -1);
            createdWater.name = "Water Block " + waterArray.Length.ToString();

            //Debug.Log(createdWater.transform.position);

            Collider2D[] colliders = Physics2D.OverlapBoxAll(createdWater.transform.position, new Vector2(0.9f, 0.9f), 0f);
            
            if (colliders.Length > 0)
            {
                bool collidedWithTunnel = false;
                foreach (Collider2D collider in colliders)
                {
                    //Edgecases for generation
                    Debug.Log(collider.name);

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

                    //Forks and tunnels
                    if (collider.name.Contains("Tunnel"))
                    {
                        collidedWithTunnel = true;
                        waterMaximum = 5 - waterArray.Length;
                        if (waterMaximum == 0)
                        {
                            startingEntity.GetComponent<WaterSpawnBehaviour>().waterLimitReached = true;
                            Destroy(createdWater);
                            return;
                        }
                    }
                }

                startingEntity.GetComponent<WaterSpawnBehaviour>().waterLimitReached = true;

                Collider2D[] rightColliders = Physics2D.OverlapBoxAll(createdWater.transform.position + new Vector3(1, 1, 0), new Vector2(0.9f, 0.9f), 0f);
                Collider2D[] leftColliders = Physics2D.OverlapBoxAll(createdWater.transform.position + new Vector3(-1, 1, 0), new Vector2(0.9f, 0.9f), 0f);
                bool rightTunnel = false, leftTunnel = false;
                
                foreach (Collider2D colliderNest in rightColliders)
                {
                    if (colliderNest.name.Contains("Tunnel")) rightTunnel = true;
                }
                foreach (Collider2D colliderNest in leftColliders)
                {
                    if (colliderNest.name.Contains("Tunnel")) leftTunnel = true;
                }

                if (rightColliders.Length == 0 || rightTunnel)
                {
                    GameObject newWaterStream = Assets.Scripts.ExtensionMethod.Instantiate(SelfPrefab, (createdWater.transform.position + new Vector3(1, 2, 0)), startingEntity.rotation, waterMaximum);
                    if (collidedWithTunnel) newWaterStream.transform.position = new Vector2(newWaterStream.transform.position.x, newWaterStream.transform.position.y - 1);

                }
                if (leftColliders.Length == 0 || leftTunnel)
                {
                    GameObject newWaterStream = Assets.Scripts.ExtensionMethod.Instantiate(SelfPrefab, (createdWater.transform.position + new Vector3(-1, 2, 0)), startingEntity.rotation, waterMaximum);
                    if (collidedWithTunnel) newWaterStream.transform.position = new Vector2(newWaterStream.transform.position.x, newWaterStream.transform.position.y - 1);
                    
                }
                Destroy(createdWater);
            }
        }
    }

    public void SetMaxRange(int maxRange)
    {
        this.maxRange = maxRange;
    }
}
