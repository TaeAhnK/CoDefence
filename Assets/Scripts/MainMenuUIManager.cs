using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : Singleton<MainMenuUIManager>
{
    #region const string mainmenustring;
    const string mainmenustring = "Welcome to Mebuntu 24.11.14 LTS (Cinux 2.38 x86_64)\r\n\r\n " +
        "* Documentation:  https://github.com/TaeAhnK/CODEfence\r\n\r\n " +
        "* Some sentences that makes this looks like a terminal.\r\n\r\n" +
        "This message is shown once a day. To disable it please create the /home/.hushlogin file.\r\n\r\n" +
        "<color=red>\r\nWarning : 1 SERIOUS virus detected. Run VirusKiller3 to kill it.\r\n\r\n" +
        "To run VirusKiller, type \"run viruskiller3\"\r\n</color>";
    #endregion
    TypingEffect typingeffect;
    [SerializeField] private TMP_InputField inputField;

    protected override void Awake()
    {
        base.Awake();
        typingeffect = GetComponent<TypingEffect>();
    }

    void Start()
    {
        SoundEvent.PlaySound(SoundType.Start);
        typingeffect.Typing(mainmenustring);
        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void OnWordEnter()
    {
        if (inputField.text == "run viruskiller3")
        {
            SoundEvent.PlaySound(SoundType.Login);
            SceneManager.LoadScene("GameScene");
        }
        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void OnTyping()
    {
        SoundEvent.PlaySound(SoundType.Typing);
    }
}
