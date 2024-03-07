using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
                        
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        
        [SerializeField] private float m_RunSpeed;
        [SerializeField] private float maxStaminaSeconds;
        private float staminaSeconds;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        private List<AudioClip> footstepSounds;    // an array of footstep sounds that will be randomly selected from.
        private int previousFootstepIndex;

        private AudioClip jumpSound;
        private AudioClip landSound;

        private string currentGround = "ZORT";


        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;


        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);

            DetectGround();
        }


        // Update is called once per frame
        private void Update()
        {
            
            RotateView();

            if(DialogueManager.GetInstance().dialogueIsPlaying){ //if dialogue is playing dont move
                return;
            }

            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        
        
        }


        private void FixedUpdate()
        {
            if(DialogueManager.GetInstance().dialogueIsPlaying &&  m_CharacterController.isGrounded){ //if dialogue is playing dont move
               return;
            }
                float speed;
                GetInput(out speed);
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

                // get a normal for the surface that is being touched to move along it
                RaycastHit hitInfo;
                Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                                m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

                m_MoveDir.x = desiredMove.x*speed;
                m_MoveDir.z = desiredMove.z*speed;



            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
            
            ProgressStepCycle(2*speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }
            

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }

            // Select the appropriate footstep sounds array
            DetectGround();

        
            int n = Random.Range(1, footstepSounds.Count);
            while (n == previousFootstepIndex) // Check if the new index is the same as the previous one
            {
                n = Random.Range(1, footstepSounds.Count); // Generate a new random index
            }

            previousFootstepIndex = n; // Store the current index as the previous one

            m_AudioSource.clip = footstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time


            

        
        }


        private void PlayJumpSound()
        {
            DetectGround();
            m_AudioSource.clip = jumpSound;
            m_AudioSource.Play();
        }

        private void PlayLandingSound()
        {
            DetectGround();
            m_AudioSource.clip = landSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }

        private void DetectGround()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            {
                if (hit.collider.tag == currentGround) return; // still the same ground
                if (hit.collider.tag == "Untagged"){
                    Debug.LogError("No ground detected, setting to dirt. Please tag the ground with the correct tag.");
                    return;
                } 
                //else: new ground detected
                //update current ground
                currentGround = hit.collider.tag;
                LoadSoundsForGround(currentGround);
            }
            //else: no ground detected
        }

        private void LoadSoundsForGround(string groundTag)
        {         
            //addressable code to load footstep sounds
            Addressables.LoadAssetAsync<AudioClip>(groundTag + "_jump").Completed += (audioClip) => {
                jumpSound = audioClip.Result;
            };
            Addressables.LoadAssetAsync<AudioClip>(groundTag + "_land").Completed += (audioClip) => {
                landSound = audioClip.Result;
            };
            Addressables.LoadAssetsAsync<AudioClip>(groundTag + "_walk", null).Completed += handle =>
            {
                footstepSounds = new List<AudioClip>();
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    foreach (var audioClip in handle.Result)
                    {
                        footstepSounds.Add(audioClip);
                    }
                }
                else
                {
                    Debug.LogError("Failed to load audio clips: " + handle.OperationException);
                }
            };
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            //m_IsWalking = !Input.GetKey(KeyCode.LeftShift);

            if(Input.GetKey(KeyCode.LeftShift) && !m_IsWalking){
                staminaSeconds -= Time.deltaTime;  
                if(staminaSeconds <= 0){
                    Debug.Log("Out of stamina");
                    m_IsWalking = true;
                }
            }
            else if (staminaSeconds < maxStaminaSeconds){
                staminaSeconds += Time.deltaTime;
            }

            if(Input.GetKeyUp(KeyCode.LeftShift)){
                Debug.Log("Stopped running");
                m_IsWalking = true;
            }
            else if(Input.GetKeyDown(KeyCode.LeftShift)){
                Debug.Log("Started running");
                m_IsWalking = false;
            }

#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
