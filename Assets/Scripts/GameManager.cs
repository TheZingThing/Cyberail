using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject explosionPrefab;

    private List<StationController> stationList = new List<StationController>();

    private void Start()
    {
        
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Station"))
        {
            stationList.Add(obj.GetComponent<StationController>());
        }

    }

    private void Update()
    {
        bool levelComplete = true;

        foreach(StationController station in stationList)
        {
            if (!station.HasRequiredPassenger)
                levelComplete = false;
        }

        if (levelComplete)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver(GameObject train)
    {
        GameObject go = Instantiate(explosionPrefab, train.transform.position, explosionPrefab.transform.rotation);
        DontDestroyOnLoad(go);
        Destroy(go, 3f);
        AudioManager.AM.Play("Explosion");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
