using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Alive;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }
    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();

        if (mainEngineParticles.isPlaying)
            mainEngineParticles.Stop();

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        rigidBody.freezeRotation = false;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();

        if (mainEngineParticles.isPlaying)
            mainEngineParticles.Stop();

        Invoke("LoadCurrentLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        successParticles.Stop();
    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            ApplyUpThrust();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ApplyDownThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyUpThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            mainEngineParticles.Play();
        }
    }

    private void ApplyDownThrust()
    {
        rigidBody.AddRelativeForce(-(Vector3.up * (mainThrust / 2) * Time.deltaTime));

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            mainEngineParticles.Play();
        }
    }

    private void RespondToRotateInput()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
    }
}
