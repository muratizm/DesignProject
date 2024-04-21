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
    public class FirstPersonController : MonoBehaviour
    {
        private static FirstPersonController _firstPersonController;
        public static FirstPersonController Instance { get { return _firstPersonController;} private set { return;} }

        private GameManager gameManager;
        private DialogueManager dialogueManager;
        private PlayerAudio playerAudio;
                        
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private bool m_IsRunning;
        [SerializeField] private bool isInjured;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_InjuredSpeed;
        [SerializeField] private float m_RunningFactor;
        [SerializeField] private bool m_canRotateView = true;
        public bool CanRotateView { get { return m_canRotateView; } set { m_canRotateView = value; } }
        public bool IsWalking { get { return m_IsWalking; } set { m_IsWalking = value; } }
        public bool IsRunning { get { return m_IsRunning; } set { m_IsRunning = value; } }
        public bool IsInjured { get { return isInjured; } set { isInjured = value; } }





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


        void Awake()
        {
            if (_firstPersonController != null)
            {
                Destroy(gameObject);
                return;
            }
            _firstPersonController = this;
        }



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
            playerAudio = GetComponent<PlayerAudio>();
			m_MouseLook.Init(transform , m_Camera.transform);

            //

            gameManager = GameManager.Instance;
            dialogueManager = DialogueManager.Instance;

            DetectGround();
        }


        // Update is called once per frame
        private void Update()
        {
            if(gameManager.IsGamePaused){ return; } //if game is paused, dont do anything

            if(m_canRotateView){
                RotateView();
            }

            if(dialogueManager != null && dialogueManager.IsDialoguePlaying){ //if dialogue is playing dont move
                return;
            }


            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                DetectGround();
                playerAudio.PlayLandingSound();
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
            if(gameManager.IsGamePaused){ return; } //if game is paused, dont do anything

            
            if(dialogueManager != null && dialogueManager.IsDialoguePlaying &&  m_CharacterController.isGrounded){ //if dialogue is playing dont move
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
                    DetectGround();
                    playerAudio.PlayJumpSound();
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

            DetectGround();
            playerAudio.PlayFootStepAudio();
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
                playerAudio.LoadSoundsForGround(currentGround);
            }
            //else: no ground detected
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


            CalculateStamina();


           
            if(Input.GetKeyUp(KeyCode.LeftShift)){
                m_IsWalking = true;
                m_IsRunning = false;
            }
            else if(Input.GetKeyDown(KeyCode.LeftShift)){
                m_IsRunning = true;
                m_IsWalking = false;
            }

            float temp = isInjured ? m_InjuredSpeed : m_WalkSpeed;
            speed = m_IsRunning ? (temp) * m_RunningFactor : (temp);


            //speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
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


        private void CalculateStamina(){
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
