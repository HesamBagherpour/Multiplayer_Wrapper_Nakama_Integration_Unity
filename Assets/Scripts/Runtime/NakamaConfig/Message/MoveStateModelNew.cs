﻿using System;
using UnityEngine;

namespace Runtime.NakamaConfig.Message
{
    [Serializable]
    public class MoveStateModelNew
    {
        public float x;
        public float y;
        public float z;
        public Vector3 pos;

        public MoveStateModelNew(float x, float y, float z, Vector3 pos)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.pos = pos;
        }
    }
    public class PingPongMessage
    {
        public PingPongMessage(string stateMessage)
        {
            this.StateMessage = stateMessage;
        }

        public string StateMessage;

        public PingPongMessage()
        {
        }
    }
}