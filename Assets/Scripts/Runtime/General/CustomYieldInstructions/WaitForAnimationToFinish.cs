using UnityEngine;

namespace Gunfighter.Runtime.General.CustomYieldInstructions
{

    /// <summary>
    ///     Waits for an animation to finish
    ///     From: https://github.com/ComradeVanti/UnityWaitForAnim
    /// </summary>
    public class WaitForAnimationToFinish : CustomYieldInstruction
    {

        private readonly string _animationName;

        private readonly Animator _animator;
        private readonly int _layerIndex;


        private AnimatorStateInfo StateInfo => _animator.GetCurrentAnimatorStateInfo(_layerIndex);

        private bool CorrectAnimationIsPlaying => StateInfo.IsName(_animationName);

        private bool AnimationIsDone => StateInfo.normalizedTime >= 1;

        public override bool keepWaiting => !AnimationIsDone;

        
        /// <summary>
        ///     Creates a new yield-instruction
        /// </summary>
        /// <param name="animator">The animator to track</param>
        /// <param name="animationName">The name of the animation</param>
        /// <param name="layerIndex">The layer the animation is playing on</param>
        public WaitForAnimationToFinish(Animator animator,  int layerIndex = 0)
        {
            _animator = animator;
            _layerIndex = layerIndex;
            //this.animationName = animationName;
        }

    }

}