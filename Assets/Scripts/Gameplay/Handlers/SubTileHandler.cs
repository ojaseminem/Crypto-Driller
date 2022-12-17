using UnityEngine;

namespace Gameplay.Handlers
{
    public class SubTileHandler : MonoBehaviour
    {
        [SerializeField] private GameObject coin;
        [SerializeField] private GameObject diamond;

        [SerializeField] private Transform[] coinSpawnPos;
        [SerializeField] private Transform[] diamondSpawnPos;

        public void SpawnCollectables()
        {
            foreach (var t in coinSpawnPos)
            {
                Instantiate(coin, t.position, Quaternion.identity);
            }

            foreach (var t in diamondSpawnPos)
            {
                Instantiate(diamond, t.position, Quaternion.identity);
            }
        }
    }
}
