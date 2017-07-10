using System;
using System.Collections.Generic;

partial class  ConfigHandler_testConfig
{
    private string ParserData(List<testConfig> data)
    {
        foreach (var line in data)
        {
            System.Text.StringBuilder res = new System.Text.StringBuilder();

            res.Append(line.id);
            res.Append(line.nameMessageId);
            res.Append(line.position.x);
            res.Append(line.position.y);
            res.Append(line.position.z);
            foreach(var elem in line.attachNamePosition)
            {
                res.Append(elem);
            }
            foreach (var elem in line.rotation)
            {
                res.Append(elem.x);
                res.Append(elem.x);
                res.Append(elem.z);
            }
            foreach (var elem in line.attachNamePosition)
            {
                res.Append(elem);
            }
            res.Append(line.resource.textureName);
            LogQueue.Instance.Enqueue(res.ToString());
        }
        return null;
    }
}