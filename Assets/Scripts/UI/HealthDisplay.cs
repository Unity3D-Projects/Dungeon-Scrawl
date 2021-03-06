﻿// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HealthDisplay.cs" author="Lars" company="None">
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
// and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// // </copyright>
// // <summary>
// //   TODO - Insert file description
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

namespace UI
{
    using System.Collections.Generic;

    using ECS.Components.Type;
    using ECS.Entities.Blueprint;

    using UnityEngine;

    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField]
        private List<GameResource> _hearts = new List<GameResource>();

        private void Start()
        {
            SetHealth(Player.GetSavedStat(ComponentType.Health));
        }

        public void SetHealth(int health)
        {
            if (health <= 0)
            {
                for (var i = 0; i < _hearts.Count; i++)
                {
                    _hearts[i].Disable();
                }
                return;
            }

            for (var i = 0; i < health; i++)
            {
                _hearts[i].Enable();
            }

            for (var i = health; i < _hearts.Count; i++)
            {           
                _hearts[i].Disable();
            }
        }
    }
}