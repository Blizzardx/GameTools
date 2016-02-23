using ExcelImproter.Configs;
//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : TrunkImporter
//
// Created by : Baoxue at 2016/2/20 16:49:22
//
//
//========================================================================
using System;
using System.Collections.Generic;

public class TrunkImporter : ExcelImporter
{    
    private Dictionary<string, int> m_ItemLengthMap;

    public override void HandlerData(List<string[][]> content, out Thrift.Protocol.TBase thriftOutput, out XmlConfigBase xmlOutput)
    {
        m_ItemLengthMap = new Dictionary<string, int>();

        thriftOutput = null;
        xmlOutput = null;
        
        //load item info
        LoadItemInfo(content[1]);

        //load trunk info
        LoadTrunk(content[0]);


    }
    private void LoadItemInfo(string[][] config)
    {
        for (int i = 0; i < config.Length; ++i)
        {
            // check
            if (IsSkipLine(config[i]))
            {
                continue;
            }

            LoadItemElement(config[i]);
        }
        
    }
    private void LoadItemElement(string[] config)
    {
        string name     = config[0];
        int length = int.Parse(config[1]);

        m_ItemLengthMap.Add(name, length);
    }
    private void LoadTrunk(string[][] config)
    {
        for(int i=0;i<config.Length;++i)
        {
            // check
            if(IsSkipLine(config[i]))
            {
                continue;
            }

            LoadTrunkElement(config[i]);
        }
        
    }
    private void LoadTrunkElement(string[] config)
    {
        RunnerTrunkElementConfig elem = new RunnerTrunkElementConfig();

        //set default 
        elem.TrunkLength = 50;
        elem.TrunkDesc = "auto-gen";
        elem.ItemList = new List<RunnerTrunkItemConfig>();

        int id = int.Parse(config[0]);
        int sceneId = int.Parse(config[1]);
        int diff = int.Parse(config[2]);

        elem.TrunkId = id;
        elem.TrunkDiff = diff;
        elem.SceneId = sceneId;

        int index = 3;
        int xoffset = 0;
        int lastLength = 0;
        int lastSkip = 0;
        while(index < config.Length &&  (! string.IsNullOrEmpty(config[index])))
        {
            string name = config[index];
            int skip = int.Parse(config[index+1]);
            RunnerTrunkItemConfig tmp = new RunnerTrunkItemConfig();

            //set data 
            tmp.ItemOffsetY = 0;
            tmp.ItemOffsetX = xoffset + lastSkip + lastLength;
            lastLength = m_ItemLengthMap[name];
            lastSkip = skip;
            tmp.ItemName = name;

            elem.ItemList.Add(tmp);
            index += 2;
            xoffset = tmp.ItemOffsetX;
        }

        //save to file 
//        string output = "D:/My Documents/Visual Studio 2013/Projects/ExcelImproter/ExcelImproter/config/xmloutput/TrunkElement_" + id.ToString() + ".xml";
        string output = SystemInfo.m_strXmlOutputPath + "/TrunkElement_" + id.ToString() + ".xml";
        string content = XmlConfigBase.Serialize(elem);

        FileUtils.WriteStringFile(output, content);
    }
}

