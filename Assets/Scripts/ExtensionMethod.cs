using UnityEngine;

namespace Assets.Scripts
{
    public static class ExtensionMethod
    {
        public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation, int waterMaximum) 
        {
            GameObject waterStream = GameObject.Instantiate(original, position, rotation) as GameObject;
            waterStream.GetComponent<WaterSpawnBehaviour>().SetMaxRange(waterMaximum);
            return waterStream;
        }
    }
}
