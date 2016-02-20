//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : XmlImporter
//
// Created by : Baoxue at 2016/2/20 16:06:19
//
//
//========================================================================

using ExcelImproter.Configs;
using System;
using System.Collections.Generic;
using Thrift.Protocol;
public abstract class XmlImporter : ImporterBase
{
    protected string m_Content;


    public void Importer(string path, out TBase thriftOutput, out XmlConfigBase xmlOutput)
    {
        string content = FileUtils.ReadStringFile(path);
        if(!string.IsNullOrEmpty(content))
        {
            HandlerData(content, out thriftOutput, out xmlOutput);
        }
        else
        {
            thriftOutput = null;
            xmlOutput = null;
        }
    }
    public abstract void HandlerData(string content, out TBase thriftOutput, out XmlConfigBase xmlOutput);
}

