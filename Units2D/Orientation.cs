using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Units2D
{
    public class Orientation
    {
        private float _CurrentDegrees = 0;
        public float CurrentDegrees 
        {
            get=>_CurrentDegrees;
            set 
            {
                _CurrentDegrees = value % 360;
                if (_CurrentDegrees < 0) _CurrentDegrees += 360;
                if (_CurrentDegrees >= 360) _CurrentDegrees -= 360;
            } 
        }

        public float DegreesStep { get; protected set; } = 360;


        private int _StepCount=1;
        public int StepCount 
        {
            get=>_StepCount;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("StepCount");
                _CurrentDegrees = 0;
                _CurrentStep = 0;
                _StepCount = value;
                DegreesStep = 360 / _StepCount;
            }
        }

        private int _CurrentStep;
        public int CurrentStep 
        {
            get => _CurrentStep;
            set
            {
                _CurrentStep = value % _StepCount;
                if (_CurrentStep < 0) _CurrentStep += _StepCount;
                if (_CurrentStep >= _StepCount) _CurrentStep -= _StepCount;
                _CurrentDegrees = DegreesStep * _CurrentStep;
            }
        }
    }
}
