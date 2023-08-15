//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Player Interface.
    /// </summary>
    public class CanvasSpawner : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Settings")]
        
        [Tooltip("Canvases prefabs spawned at start. Displays the player's user interface.")]
        [SerializeField]
        private GameObject[] canvasPrefabs;
        #endregion

        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>
        private void Awake()
        {
            for (int i = 0; i < canvasPrefabs.Length; i++)
            {
                Instantiate(canvasPrefabs[i]);
            }
        }

        #endregion
    }
}