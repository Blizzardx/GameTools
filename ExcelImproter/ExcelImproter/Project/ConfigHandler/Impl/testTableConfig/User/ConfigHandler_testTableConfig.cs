using System;
using System.Collections.Generic;

partial class  ConfigHandler_testTableConfig
{
    private string ParserData(List<testTableConfig> data)
    {
        foreach(var line in data)
        {
            System.Text.StringBuilder res = new System.Text.StringBuilder();

            res.Append(line.id);
            res.Append(line.name);
            res.Append(line.costId);
            res.Append(line.position.x);
            res.Append(line.position.y);
            res.Append(line.position.z);
            res.Append(line.nameMessageId.id);
            foreach(var texture in line.textureName)
            {
                res.Append(texture);
            }
            foreach (var elem in line.vector4)
            {
                res.Append(elem.x);
                res.Append(elem.y);
                res.Append(elem.z);
                res.Append(elem.w);
            }
            foreach (var elem in line.package)
            {
                res.Append(elem.packageName);
            }
            LogQueue.Instance.Enqueue(res.ToString());
        }
        return null;
    }
}