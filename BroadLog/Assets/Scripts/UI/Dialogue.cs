using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class Dialogue
    {
        [SerializeField] private List<string> lines;

        public List<string> Lines => lines;
    }
}