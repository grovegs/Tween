using UnityEngine;

namespace GroveGames.Tween.Unity.Editor.Tests
{
    public sealed class TweenTarget
    {
        private float _value;
        private Vector3 _position;
        private int _setCount;
        private int _completedCount;

        public float Value => _value;
        public Vector3 Position => _position;
        public int SetCount => _setCount;
        public int CompletedCount => _completedCount;

        public void SetValue(float value)
        {
            _value = value;
            _setCount++;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
            _setCount++;
        }

        public void Complete()
        {
            _completedCount++;
        }
    }
}
