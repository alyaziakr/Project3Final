using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType { Grab, Force, Spin, Push }

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    public InteractableType interactableType = InteractableType.Force;

    [Header("Feedback")]
    public Renderer renderer;
    public Color highlighColor = Color.black;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private Material material;

    private Rigidbody rigidbody;
    private Vector3 touchOffset;

    private float startAngle;
    private Quaternion startRotation;

    private float currentRotationSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (renderer == null)
        {
            renderer = GetComponentInChildren<Renderer>();
        }
        material = renderer.material;

        if (highlighColor == Color.black)
        {
            highlighColor = material.color;
        }
        material.EnableKeyword("_EMISSION");


        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }
    }

    private void FixedUpdate()
    {
        currentRotationSpeed = rigidbody.angularVelocity.sqrMagnitude;

        if (interactableType == InteractableType.Spin)
        {
            if (currentRotationSpeed >= 0.2f && !audioSource.isPlaying)
            {
                audioSource.volume = 0;
                audioSource.Play();
            }
            else if (Mathf.Approximately(currentRotationSpeed, 0f) && audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (audioSource.isPlaying)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, Mathf.Clamp(currentRotationSpeed / 20f, 0f, .8f), Time.deltaTime * 5f);
                audioSource.pitch = Mathf.Lerp(audioSource.pitch, Mathf.Clamp(currentRotationSpeed / 10f, 0.4f, 1.2f), Time.deltaTime * 5f);
            }
        }
    }

    public void OnTouchDown(Vector3 startTouchPosition)
    {
        //Debug.Log("on touch down");
        touchOffset = startTouchPosition - transform.position;

        startAngle = Mathf.Atan2(startTouchPosition.z - rigidbody.position.z, startTouchPosition.x - rigidbody.position.x);
        startRotation = rigidbody.rotation;

        if (interactableType == InteractableType.Grab || interactableType == InteractableType.Spin)
        {
            rigidbody.isKinematic = true;
        }

        material.SetColor("_EmissionColor", highlighColor);
    }

    public void OnTouchUp()
    {
        //Debug.Log("on touch up");
        rigidbody.isKinematic = false;

        material.SetColor("_EmissionColor", Color.black);
    }

    /// <summary>
    /// Move function is called by InteractionManager
    /// It call differnt action functions based on InteractableType
    /// </summary>
    public void Move(Vector3 currentTouchPosition)
    {
        switch (interactableType)
        {
            case InteractableType.Grab:
                MovePosition(currentTouchPosition);
                break;

            case InteractableType.Force:
                AddForce(currentTouchPosition);
                break;

            case InteractableType.Spin:
                MoveRotation(currentTouchPosition);
                break;

            case InteractableType.Push:
                AddForceToButton();
                break;
        }
    }

    //////////////////////////////////////////////////
    //////////////////////////////////////////////////
    ///   Different actions of InteractableType    ///
    //////////////////////////////////////////////////
    //////////////////////////////////////////////////
    private void AddForce(Vector3 currentTouchPosition)
    {
        Vector3 force = currentTouchPosition - rigidbody.position;
        rigidbody.AddForce(force * Time.deltaTime * 2000f);
    }

    private void MovePosition(Vector3 currentTouchPosition)
    {
        Vector3 finalPosition = currentTouchPosition - touchOffset;
        rigidbody.MovePosition(finalPosition);
    }

    private void MoveRotation(Vector3 currentTouchPosition)
    {
        float currentAngle = Mathf.Atan2(currentTouchPosition.z - rigidbody.position.z, currentTouchPosition.x - rigidbody.position.x);
        float angleDifference = currentAngle - startAngle;
        // Convert from counterclockwise being positive in standard math world, to clockwise being positive in Unity
        angleDifference *= -1f;
        // Convert from radian to degree
        angleDifference *= Mathf.Rad2Deg;

        rigidbody.MoveRotation(startRotation * Quaternion.Euler(0, angleDifference, 0));
    }

    private void AddForceToButton()
    {
        rigidbody.AddRelativeForce(Vector3.right * 50f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (interactableType == InteractableType.Spin) return;

        // use force to decide the volume and the pitch of the audio source
        float relativeForce = collision.relativeVelocity.magnitude;

        audioSource.pitch = Mathf.Clamp(relativeForce / 3f, 0.8f, 2f);
        audioSource.PlayOneShot(audioClip, Mathf.Clamp01(relativeForce * 0.05f));

        // Particles
        if (collision.contactCount == 0) return;
        InteractionManager.Instance.collisionParticles.transform.position = collision.GetContact(0).point;
        InteractionManager.Instance.collisionParticles.Emit(3);
    }
}