using System;
using Gunfighter.Runtime.Entity.Character.Player.PlayerController;
using Gunfighter.Runtime.ScriptableObjects.Data.Character.Player;
using NUnit.Framework;
using UnityEngine;

namespace Gunfighter.Tests.Editor
{
    public class PlayerLevelContorllerTests 
    {
        public class AddExperience
        {
            private PlayerLevelController _lvlController;

            [SetUp]
            public void SetUp()
            {
                Keyframe[] keyframes = new[] { new Keyframe(1, 0),new Keyframe(2, 25),new Keyframe(3, 75),new Keyframe(4, 100) };
                PlayerData playerData = PlayerData.CreateInstance(new AnimationCurve(keyframes));
                GameObject player = new GameObject();
                _lvlController = player.AddComponent<PlayerLevelController>();
                _lvlController.SetPlayerData(playerData);
                _lvlController.SetLevelAndExperience(1,0);
            }
        
            [Test]
            public void _0_adds_experience_correctly()
            {
            
                _lvlController.AddExperience(10);
                Assert.AreEqual(10,_lvlController.Experience);
            }

            [Test]
            public void _1_level_up_happens_correctly()
            {
                _lvlController.AddExperience(30);
                Assert.AreEqual(2,_lvlController.Level);
            }

            // private void OnLevelUpCall(int arg)
            // {
            //     Debug.Log(arg);
            //     this.lvl = arg;
            // }
            //
            // [Test]
            // public void _2_level_up_event_called_on_level_up()
            // {
            //     _lvlController.OnLevelUp += OnLevelUpCall;
            //     Assert.AreEqual(2,lvl);
            // }

            [Test]
            public void _3_negative_xp_throws_exception()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _lvlController.AddExperience(-1));
            }
        
            [Test]
            public void _4_multiple_level_up_happens_correctly()
            {
                _lvlController.AddExperience(99);
                Assert.AreEqual(3,_lvlController.Level);
            }
        
            [Test]
            public void _5_throws_exception_on_xp_overflow()
            {
            
                Assert.Throws<OverflowException>(() => _lvlController.AddExperience(999));
                Assert.AreEqual(4,_lvlController.Level);
            }
        }
    }
}
