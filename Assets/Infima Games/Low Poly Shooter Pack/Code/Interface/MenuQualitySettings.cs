//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Quality Settings Menu.
    /// </summary>
    public class MenuQualitySettings : Element
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "Settings")]

        [Tooltip("Canvas to play animations on.")]
        [SerializeField]
        private GameObject animatedCanvas;

        [Tooltip("Animation played when showing this menu.")]
        [SerializeField]
        private AnimationClip animationShow;

        [Tooltip("Animation played when hiding this menu.")]
        [SerializeField]
        private AnimationClip animationHide;

        #endregion
        
        #region FIELDS
        
        /// <summary>
        /// Animation Component.
        /// </summary>
        private Animation animationComponent;
        /// <summary>
        /// If true, it means that this menu is enabled and showing properly.
        /// </summary>
        private bool menuIsEnabled;

        /// <summary>
        /// Main Post Processing Volume.
        /// </summary>
        private PostProcessVolume postProcessingVolume;
        /// <summary>
        /// Scope Post Processing Volume.
        /// </summary>
        private PostProcessVolume postProcessingVolumeScope;

        /// <summary>
        /// Depth Of Field Settings.
        /// </summary>
        private DepthOfField depthOfField;

        #endregion

        #region UNITY

        private void Start()
        {
            //Hide pause menu on start.
            animatedCanvas.GetComponent<CanvasGroup>().alpha = 0;
            //Get canvas animation component.
            animationComponent = animatedCanvas.GetComponent<Animation>();

            //Find post process volumes in scene and assign them.
            postProcessingVolume = GameObject.Find("Post Processing Volume")?.GetComponent<PostProcessVolume>();
            postProcessingVolumeScope = GameObject.Find("Post Processing Volume Scope")?.GetComponent<PostProcessVolume>();
            
            //Get depth of field setting from main post process volume.
            if(postProcessingVolume != null)
                postProcessingVolume.profile.TryGetSettings(out depthOfField);
        }

        protected override void Tick()
        {
            if (Health.Instance.died) return;
            //Switch. Fades in or out the menu based on the cursor's state.
            bool cursorLocked = characterBehaviour.IsCursorLocked();

            switch (cursorLocked)
            {
                //Hide.
                case true when menuIsEnabled:
                    Hide();
                    break;
                //Show.
                case false when !menuIsEnabled:
                    Show();
                    break;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Shows the menu by playing an animation.
        /// </summary>
        public void Show()
        {
            //Enabled.
            menuIsEnabled = true;

            //Play Clip.
            animationComponent.clip = animationShow;
            animationComponent.Play();

            //Enable depth of field effect.
            if(depthOfField != null)
                depthOfField.active = true;
        }
        /// <summary>
        /// Hides the menu by playing an animation.
        /// </summary>
        public void Hide()
        {
            //Disabled.
            menuIsEnabled = false;

            //Play Clip.
            animationComponent.clip = animationHide;
            animationComponent.Play();

            //Disable depth of field effect.
            if(depthOfField != null)
                depthOfField.active = false;
        }

        /// <summary>
        /// Sets whether the post processing is enabled, or disabled.
        /// </summary>
        private void SetPostProcessingState(bool value = true)
        {
            //Enable/Disable the volumes.
            if(postProcessingVolume != null)
                postProcessingVolume.enabled = value;
            if(postProcessingVolumeScope != null)
                postProcessingVolumeScope.enabled = value;
        }

        public void Restart()
        {
            GameData.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            GameData.Instance.LoadLevel(0);
        }

        public bool IsPaused() => menuIsEnabled;
        #endregion
    }
}