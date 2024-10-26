using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class AppPrefs : MonoBehaviour
{
    /*
    ? Declaration of variables
    * @_info - TextBox to hold the information of the current build, version and product name
    * @_companyName - string to hold the company name
    * @_version - string to hold the version number
    * @_buildGUID - string to hold the GUID of the build
    * @_productName - string to hold the product name
    * @_fpsText - TextBox to hold the current of while the game is playing
    * @_timer - timer to be able to calculate how many frames per each second there are
    * @_hudRefreshRate - number of seconds that the FPS counter is updated per second 
    */
    public TMP_Text _info;
    private string _version;
    private string _productName;
    public TMP_Text _fpsText;
    public bool _showFPS;

    private float _timer;
    private float _hudRefreshRate = 1f;

    void OnValidate() //validar e colocar logo a informação nas variáveis
    {
        _version = Application.version.ToString();
        _productName = Application.productName.ToString();
    }

    void Start()
    {   // ? setting the frameRate to 30 while in development
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "01")
        {
            if (Application.isEditor || Application.isMobilePlatform) Application.targetFrameRate = 144;
            else
            {
                QualitySettings.vSyncCount = 0; // ? setting the vSync to 0
                //Application.targetFrameRate = 144; // ? setting the frameRate to 60 
            }

            _info.text = _productName + "\nVersion: " + _version;
        }
    }

    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "01")
        {
            if (_showFPS) //colocar isto no menu de opções
            {
                if (Time.unscaledTime > _timer) //timer to handle the FPS counter
                {
                    int fps = (int)(1f / Time.unscaledDeltaTime); //divide the unscaled time by 1
                    _fpsText.text = "FPS: " + fps;
                    _timer = Time.unscaledTime + _hudRefreshRate;
                }
            }
            else _fpsText.text = "  ";
        }
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("01");
    }

    public void Options()
    {
        new DebugUI.Panel().SetDirty();
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }

}
