using System;
using NUnit.Framework;
using UI;
using UI.HealthBar;
using UnityEngine;
using UnityEngine.UI;

namespace Tests.Editor
{
    public class TilableDisplayControllerTests
    {
    
        public class SetupDisplay
        {
            public TilableDisplayController _tilableDisplayController;
            public GameObject _unit;
        
            [SetUp]
            public void setUp()
            {
                _unit = new GameObject();
                Texture2D tex = new Texture2D(1,1);
                Image image = _unit.AddComponent<Image>();
                image.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
                _tilableDisplayController = new HealthBarController(_unit,new GameObject());
            }
        
            [Test]
            public void _0_health_bar_initializes_correctly()
            {
            
                _tilableDisplayController.SetupDisplay(10);
                Assert.AreEqual(10,_tilableDisplayController.GetAmmountOfUnits);
            }
        
            [Test]
            public void _1_max_health_updates_correctly()
            {
                _tilableDisplayController.SetupDisplay(0);
                _tilableDisplayController.SetupDisplay(6);
                Assert.AreEqual(6,_tilableDisplayController.GetAmmountOfUnits);
            }
        
            [Test]
            public void _2_throws_exeption_with_negative_health_value()
            {
                Assert.Throws<ArgumentOutOfRangeException>(()=>_tilableDisplayController.SetupDisplay(-1));
            }
        }
        public class UpdateDisplay
        {
            public TilableDisplayController _tilableDisplayController;
            public GameObject _unit;

            [SetUp]
            public void setUp()
            {
                _unit = new GameObject();
                Texture2D tex = new Texture2D(1,1);
                Image image = _unit.AddComponent<Image>();
                image.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
                _tilableDisplayController = new HealthBarController(_unit,new GameObject());
            }
        
        

            [Test]
            public void _0_health_updates_correctly()
            {
                _tilableDisplayController.SetupDisplay(5);
                _tilableDisplayController.UpdateDisplay(4);
                Assert.AreEqual(Color.gray, _tilableDisplayController.GetUnit(4).GetComponent<Image>().color);
                Assert.AreEqual(Color.white, _tilableDisplayController.GetUnit(3).GetComponent<Image>().color);
            }

            [Test]
            public void _1_health_increase_updates_correctly()
            {
                _tilableDisplayController.SetupDisplay(8);
                _tilableDisplayController.UpdateDisplay(4);
                _tilableDisplayController.UpdateDisplay(6);
                Assert.AreEqual(Color.gray, _tilableDisplayController.GetUnit(6).GetComponent<Image>().color);
                Assert.AreEqual(Color.white, _tilableDisplayController.GetUnit(5).GetComponent<Image>().color);
            }
        }

    }
}
