using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public enum Direction {Left, Right, Rand};

	[System.Serializable]
	public struct SpawnPointInspector{
		public GameObject obj;
		public Direction direction;
	}

	public struct SpawnPoint{
		public Vector3 position;
		public Direction direction;
	}

	public float time_z_score, secondsToWait;

	public SpawnPointInspector[] spawnPointObjects;
	public GameObject[] enemyObjects;
	public static GameObject[] enemies;
	public static SpawnPoint[] spawnPoints;

	private float nextEnemyTime;

	// Use this for initialization
	void Awake(){
		spawnPoints = new SpawnPoint[spawnPointObjects.Length];
		for(int i = 0; i < spawnPointObjects.Length; i++){
			spawnPoints[i].position = spawnPointObjects[i].obj.transform.position;
			spawnPoints[i].direction = spawnPointObjects[i].direction;
		}

		enemies = enemyObjects;
	}

	void Start(){
		SpawnRandomEnemy();
		nextEnemyTime = NextEnemyTime(Mathy.NextGaussianFloat());
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextEnemyTime){
			SpawnRandomEnemy();
			nextEnemyTime = NextEnemyTime(Mathy.NextGaussianFloat());
		}
	}

	private float NextEnemyTime(float gauss){
        if(gauss > 0){//positive
            if(gauss < time_z_score){//1 standard deviation above
                return Time.time + secondsToWait * 1f;
            }
            if(gauss < time_z_score * 2f){//2 standard deviations above
                return Time.time + secondsToWait * 1.3f;

            }//3 or more standard deviations
            return Time.time + secondsToWait * 1.5f;

        }
        if(gauss > -time_z_score){//1 standard deviation above
            return Time.time + secondsToWait * 1f;
        }
        if(gauss > -time_z_score * 2f){//2 standard deviations above
            return Time.time + secondsToWait * 1.3f;

        }//3 or more standard deviations
        return Time.time + secondsToWait * 1.5f;

    }

	public static void SpawnRandomEnemy(){
		SpawnPoint spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
		GameObject go = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint.position, Quaternion.identity) as GameObject;
		EnemyController ec = go.GetComponent<EnemyController>();
		if (spawnPoint.direction == Direction.Rand){
			ec.SetDirection(Random.value > 0.5f);
		} else {
			ec.SetDirection(spawnPoint.direction == Direction.Right);
		}
	}
}
