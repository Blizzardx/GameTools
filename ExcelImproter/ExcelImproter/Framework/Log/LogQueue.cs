//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : LogQueue
//
// Created by : Baoxue at 2016/2/25 18:48:12
//
//
//========================================================================
using System;
using System.Collections.Generic;
using Common.Tool;

public class LogQueue:Singleton<LogQueue>
{
    private readonly Queue<string> queue = new Queue<string>();

    public void Enqueue(string log)
    {
        lock (queue)
        {
            queue.Enqueue(DateTime.Now + " - " + log + "\r\n");
        }
    }

    public string Dequeue()
    {
        lock (queue)
        {
            if (queue.Count > 0)
            {
                return queue.Dequeue();
            }
            return null;
        }
    }
}
