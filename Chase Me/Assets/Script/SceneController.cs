using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject instuctionPanel;
    public AudioClip backgroundMusic;
    public void PlayButton()
    {
        instuctionPanel.SetActive(true);
    }

}
