using Gameplay.Handlers;
using UnityEngine;

namespace Gameplay.Managers
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        [SerializeField] private GameObject[] tilePrefabs;

        [SerializeField] private float tileLength;
        [SerializeField] private int numberOfTiles = 3;

        private float _spawn;
        
        private void Start()
        {
            for (int i = 0; i < numberOfTiles; i++)
            {
                SpawnTile();
            }
        }
        private void Update()
        {
            if(-13 - playerTransform.position.y >= _spawn - (numberOfTiles * tileLength))
            {
                SpawnTile();
            }
        }
        private void SpawnTile()
        {
            var random = Random.Range(0, tilePrefabs.Length);
            var groundTile = Instantiate(tilePrefabs[random], Vector3.down * _spawn, Quaternion.identity);
            groundTile.GetComponent<GroundTileHandler>().RandomizeContainers();
            _spawn += tileLength;
        }
    }
}
