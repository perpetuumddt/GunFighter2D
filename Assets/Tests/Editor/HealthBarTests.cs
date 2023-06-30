using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthBarTests
{
    public class UpdateHealthBar
    {
        private HealthBarController _healthBarController;
        private GameObject _uiElement;

        [SetUp]
        public void setUp()
        {
            _healthBarController = new HealthBarController(new GameObject());
            _uiElement = new GameObject();
        }
        
        // [Test]
        // public void _0_health_bar_initializes_correctly()
        // {
        //     
        //     _healthBarController.UpdateHealthBar(10,_uiElement);
        //     Assert.AreEqual(10,_healthBarController.GetAmmountOfUnits);
        // }
        //
        // [Test]
        // public void _1_health_bar_updates_correctly()
        // {
        //     _healthBarController.UpdateHealthBar(0,_uiElement);
        //     _healthBarController.UpdateHealthBar(6,_uiElement);
        //     Assert.AreEqual(6,_healthBarController.GetAmmountOfUnits);
        // }
        //
        // [Test]
        // public void _2_throws_exeption_with_negative_health_value()
        // {
        //     Assert.Throws<ArgumentOutOfRangeException>(()=>_healthBarController.UpdateHealthBar(-1,_uiElement));
        // }
    }

}
