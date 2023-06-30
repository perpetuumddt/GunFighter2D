using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TilableDisplayControllerTests
{
    
    public class SetupDisplay
    {
        public TilableDisplayController _healthBarController;
        public GameObject _unit;
        
        [SetUp]
        public void setUp()
        {
            _unit = new GameObject();
            Texture2D tex = new Texture2D(1,1);
            Image image = _unit.AddComponent<Image>();
            image.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
            _healthBarController = new HealthBarController(_unit,new GameObject());
        }
        
        [Test]
        public void _0_health_bar_initializes_correctly()
        {
            
            _healthBarController.SetupDisplay(10);
            Assert.AreEqual(10,_healthBarController.GetAmmountOfUnits);
        }
        
        [Test]
        public void _1_max_health_updates_correctly()
        {
            _healthBarController.SetupDisplay(0);
            _healthBarController.SetupDisplay(6);
            Assert.AreEqual(6,_healthBarController.GetAmmountOfUnits);
        }
        
        [Test]
        public void _2_throws_exeption_with_negative_health_value()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=>_healthBarController.SetupDisplay(-1));
        }
    }
    public class UpdateDisplay
    {
        public TilableDisplayController _healthBarController;
        public GameObject _unit;

        [SetUp]
        public void setUp()
        {
            _unit = new GameObject();
            Texture2D tex = new Texture2D(1,1);
            Image image = _unit.AddComponent<Image>();
            image.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
            _healthBarController = new HealthBarController(_unit,new GameObject());
        }
        
        

        [Test]
        public void _0_health_updates_correctly()
        {
            _healthBarController.SetupDisplay(5);
            _healthBarController.UpdateDisplay(4);
            Assert.AreEqual(Color.gray, _healthBarController.GetUnit(4).GetComponent<Image>().color);
            Assert.AreEqual(Color.white, _healthBarController.GetUnit(3).GetComponent<Image>().color);
        }

        [Test]
        public void _1_health_increase_updates_correctly()
        {
            _healthBarController.SetupDisplay(8);
            _healthBarController.UpdateDisplay(4);
            _healthBarController.UpdateDisplay(6);
            Assert.AreEqual(Color.gray, _healthBarController.GetUnit(6).GetComponent<Image>().color);
            Assert.AreEqual(Color.white, _healthBarController.GetUnit(5).GetComponent<Image>().color);
        }
    }

}
