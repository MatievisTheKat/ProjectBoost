using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip siren;
    [SerializeField] AudioClip explosion;
    [SerializeField] float loadLevelDelay = 20f;

    enum State {  AwaitingUserInput, Writing };
    State state = State.Writing;

    // Update is called once per frame
    void Start ()
    {
        PrintMenu();
    }

    void OnUserInput (string input)
    {
        HandleOptions(input);
    }

    void HandleOptions(string input)
    {
        if (input.ToUpper() == "CONTROLS")
        {
            PrintConrolMenu();
        }
        else if (input.ToUpper() == "START")
        {
            StartGame();
        }
        else if (input.ToUpper() == "BACK")
        {
            PrintMenu();
        }
    }

    private void PrintMenu ()
    {
        Terminal.ClearScreen();
        Invoke("PrintBootingUp", .5f);
        Invoke("PrintWelcome", 2f);
        Invoke("PrintIntro", 4f);
        Invoke("PrintAskForRoute", 5f);
        Invoke("PrintRoutes", 5.5f);
    }

    private void PrintConrolMenu()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Ship Controls:");
        Terminal.WriteLine("");
        Terminal.WriteLine("[SPACE_BAR] Engage forward boosters");
        Terminal.WriteLine("[LEFT_ARROW] Rotate anti-clockwise");
        Terminal.WriteLine("[RIGHT_ARROW] Rotate clockwise");
        Terminal.WriteLine("[DOWN_ARROW] Engage backward boosters");
        Terminal.WriteLine("");
        Terminal.WriteLine("Type 'back' to return to the menu");
    }

    private void StartGame()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Initializing training program...");
        Invoke("PrintCrash", 2f);
        Invoke("PrintReboot", 3f);
        Invoke("PrintRebootComplete", 6f);
        Invoke("PrintLanding", 7f);
        Invoke("PrintPurpose", 8f);
        Invoke("Explode", 12f);
        Invoke("PrintWin", 14f);
        Invoke("LoadFirstLevel", loadLevelDelay);
    }

    void PrintWin ()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("You need to land on");
        Terminal.WriteLine("the green landing pads");
        Terminal.WriteLine("in each level");
        Terminal.WriteLine("to escape the planet");
    }
    void PrintCrash()
    {
        Terminal.WriteLine("[SYSTEM] Fatal crash");
        audioSource.PlayOneShot(siren);
    }
    void PrintReboot()
    {
        Terminal.WriteLine("[SYSTEM] Force rebooting...");
    }
    void PrintRebootComplete()
    {
        Terminal.WriteLine("[SYSTEM] Reboot complete.");
        Terminal.WriteLine("[SYSTEM] Large scale planet detected");
    }
    void PrintLanding ()
    {
        Terminal.WriteLine("[SYSTEM] Prepare for emergency landing");
    }
    void PrintPurpose ()
    {
        Terminal.WriteLine("[COMMANDER] Trainee, you need to escape");
    }
    void Explode()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.PlayOneShot(explosion);
    }

    private void LoadFirstLevel ()
    {
        SceneManager.LoadScene(1);
    }

    void PrintBootingUp()
    {
        Terminal.WriteLine("System booting up...");
    }

    void PrintWelcome()
    {
        Terminal.WriteLine("Welcome Trainee#2857");
    }

    void PrintIntro()
    {
        Terminal.WriteLine("You are here to take part in the");
        Terminal.WriteLine("Interstelar Police Training Academy");
    }

    void PrintAskForRoute()
    {
        Terminal.WriteLine("Please select you prefered route:");
        Terminal.WriteLine("");
    }

    private void PrintRoutes ()
    {
        Terminal.WriteLine("[CONTROLS] Navigation");
        Terminal.WriteLine("[START] Begin Training");
        Terminal.WriteLine("");
        Terminal.WriteLine("Type the index between the []");

        state = State.AwaitingUserInput;
    }
}
