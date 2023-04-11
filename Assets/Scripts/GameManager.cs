using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //zoom system
    /*public Transform lookTarget;

    public float zoomPOV = 30f;
    float zoomOriginal = 60f;
    float zoomSpeed = 5f;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void zoomCamIn()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomPOV, zoomSpeed);
        cam.transform.LookAt(lookTarget.position);
    }

    public void zoomCamOut()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomOriginal, zoomSpeed);
    }*/

    //reset the game
    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
