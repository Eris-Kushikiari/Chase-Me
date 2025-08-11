using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject instuctionPanel;
    public void PlayButton()
    {
        instuctionPanel.SetActive(true);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
