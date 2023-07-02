using System;
using Gunfighter.Runtime.UI;
using Gunfighter.Runtime.UI.HealthBar;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Gunfighter.Tests.Editor
{
    public class TilableDisplayControllerTests
    {
    
        public class SetupDisplay
        {
            public TilableDisplayController TilableDisplayController;
            public GameObject Unit;
        
            [SetUp]
            public void SetUp()
            {
                Unit = new GameObject();
                Texture2D tex = new Texture2D(1,1);
                Image image = Unit.AddComponent<Image>();
                image.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
                TilableDisplayController = new HealthBarController(Unit,new GameObject());
            }
        
            [Test]
            public void _0_health_bar_initializes_correctly()
            {
            
                TilableDisplayController.SetupDisplay(10);
                Assert.AreEqual(10,TilableDisplayController.GetAmmountOfUnits);
            }
        
            [Test]
            public void _1_max_health_updates_correctly()
            {
                TilableDisplayController.SetupDisplay(0);
                TilableDisplayController.SetupDisplay(6);
                Assert.AreEqual(6,TilableDisplayController.GetAmmountOfUnits);
            }
        
            [Test]
            public void _2_throws_exeption_with_negative_health_value()
            {
                Assert.Throws<ArgumentOutOfRangeException>(()=>TilableDisplayController.SetupDisplay(-1));
            }
        }
        public class UpdateDisplay
        {
            public TilableDisplayController TilableDisplayController;
            public GameObject Unit;

            [SetUp]
            public void SetUp()
            {
                Unit = new GameObject();
                Texture2D tex = new Texture2D(1,1);
                Image image = Unit.AddComponent<Image>();
                image.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
                TilableDisplayController = new HealthBarController(Unit,new GameObject());
            }
        
        

            [Test]
            public void _0_health_updates_correctly()
            {
                TilableDisplayController.SetupDisplay(5);
                TilableDisplayController.UpdateDisplay(4);
                Assert.AreEqual(Color.gray, TilableDisplayController.GetUnit(4).GetComponent<Image>().color);
                Assert.AreEqual(Color.white, TilableDisplayController.GetUnit(3).GetComponent<Image>().color);
            }

            [Test]
            public void _1_health_increase_updates_correctly()
            {
                TilableDisplayController.SetupDisplay(8);
                TilableDisplayController.UpdateDisplay(4);
                TilableDisplayController.UpdateDisplay(6);
                Assert.AreEqual(Color.gray, TilableDisplayController.GetUnit(6).GetComponent<Image>().color);
                Assert.AreEqual(Color.white, TilableDisplayController.GetUnit(5).GetComponent<Image>().color);
            }
        }

    }
}
