using UnityEngine;

public class PlatformObstaclesRandom : MonoBehaviour {
public GameObject[] obstacles;
private bool stepped = false;

private void OnEnable() {

        stepped = false;

        for(int i=0; i<obstacles.Length; i++)
        {
            if(Random.Range(0,2) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }

    }

}

