using UnityEngine;
using System.Collections;
public class EnglishSpawn : MonoBehaviour {
    [SerializeField] float englishAmount;
    [SerializeField] Vector3 spawnValue;
    [SerializeField] GameObject[] english;
    void Start() {
        SpawnEnglish();
    }
    void SpawnEnglish() {
        for(int i = 0; i < english.Length; i++) {
            for(int j = 0; j < englishAmount; j++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, (Random.Range(-spawnValue.z, spawnValue.z)));
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(english[i], spawnPosition, spawnRotation);
            }
        }
    }
}
