using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkPitcher : Pickup
{
        private bool _isFull;
        private string _currentMilkInPitcher="none";

        public bool GetIsFull()
        {
                return _isFull;
        }

        public void PourMilkInPitcher(string milkInPitcher)
        {
                _currentMilkInPitcher = milkInPitcher;
                _isFull = true;
        }

        public string GetMilkInPitcher()
        {
                return _currentMilkInPitcher;
        }

        private void FixedUpdate()
        {
                Debug.Log(_currentMilkInPitcher);
        }
}
