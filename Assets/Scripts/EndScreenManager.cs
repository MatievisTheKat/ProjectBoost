using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PrintMenu();
    }

    void OnUserInput (string input)
    {
        if (input.ToUpper() == "QUIT")
        {
            Terminal.WriteLine("Close the browser if playing online");
            Application.Quit();
        }
        else if (input.ToUpper() == "RESTART")
        {
            SceneManager.LoadScene(1);
        }
    }

    private void PrintMenu()
    {
        Invoke("PrintLoggingIn", 0);
        Invoke("PrintWelcome", 4f);
        Invoke("PrintThankYou", 9f);
        Invoke("PrintQuit", 14f);
    }

    private void PrintLoggingIn ()
    {
        Terminal.WriteLine("[SYSTEM] Logging in as Trainee#2857...");
    }

    private void PrintWelcome ()
    {
        Terminal.WriteLine("[SYSTEM] Welcome back trainee");
        Terminal.WriteLine("[SYSTEM] We shall now begin your training");
    }

    private void PrintThankYou ()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Thank you for playing!");
    }

    private void PrintQuit ()
    {
        Terminal.WriteLine("Type 'quit' or close the browser");
        Terminal.WriteLine("or 'restart' to restart");
    }
}
