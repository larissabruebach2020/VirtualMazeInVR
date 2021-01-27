//==============================================================================
// Copyright 2020, ViLeArn (Chair for HCI, University of Wuerzburg)
// For more information visit: vilearn.hci.uni-wuerzburg.de
//==============================================================================
using UnityEngine;
using UnityEngine.XR;
using uniwue.hci.vilearn;

/// <summary>GameState</summary>
public class GameState : MonoBehaviour
    {
        public enum enTrackingConfiguration
        {
            Desktop,
            OculusVR,
            SteamVR
        };

        /// <summary>GameState Instance Internal</summary>
        private static GameState _gameState;

        public enTrackingConfiguration trackingConfiguration = enTrackingConfiguration.Desktop;

        public bool isWalkingEnabled = false;

        [SerializeField] private Transform vrPlayer = default;
        [SerializeField] private Transform desktopPlayer = default;

        [SerializeField] private XRControllerInput inputRight;
        [SerializeField] private XRControllerInput inputLeft;

        public KeyCode desktopWalkForward = KeyCode.W;
        public KeyCode desktopTriggerAgentInteraction = KeyCode.E;

        /// <summary>GameState Instance</summary>
        public static GameState Instance
        {
            get
            {
                if (!_gameState)
                {
                    _gameState = FindObjectOfType(typeof(GameState)) as GameState;

                if (!_gameState)
                    {
                        Debug.LogError(
                            "There needs to be one active GameState script on a GameObject in your scene.");
                    }
                }
                
                return _gameState;
            }
        }

    private void Awake()
    {
        isWalkingEnabled = false;
    }

    public Transform GetPlayerCamera()
    {
        return GetPlayer().GetComponentInChildren<Camera>().transform;
    }

        public Transform GetPlayer()
        {
            if (trackingConfiguration == enTrackingConfiguration.Desktop)
                return desktopPlayer;

            return vrPlayer;
        }

        public XRControllerInput GetXrControllerInput(XRNode node)
        {
            if (node == XRNode.LeftHand) return inputLeft;
            if (node == XRNode.RightHand) return inputRight;

            return null;
        }
    }
