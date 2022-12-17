using Gameplay.Misc;
using UnityEngine;

namespace Gameplay.Handlers
{
    public class GroundTileHandler : MonoBehaviour
    {
        [SerializeField] private GameObject[] elementTiles;

        public void RandomizeContainers()
        {
            var rand = Random.Range(0, elementTiles.Length);
            elementTiles[rand].SetActive(true);
            elementTiles[rand].GetComponent<SubTileHandler>().SpawnCollectables();
        }
    }
}
