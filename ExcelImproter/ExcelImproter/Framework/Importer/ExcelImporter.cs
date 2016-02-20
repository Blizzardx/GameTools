//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : ExcelImporter
//
// Created by : Baoxue at 2016/2/20 16:06:09
//
//
//========================================================================

using ExcelImproter;
using System;
using System.Collections.Generic;
using Thrift.Protocol;
public abstract class ExcelImporter : ImporterBase
{
    protected ExcelReader       m_Reader;
    protected List<string[][]>  m_Content;

    public void Importer(string path,out TBase thriftOutput,out XmlConfigBase xmlOutput)
    {
        ExcelReader reader = new ExcelReader();
        if(reader.ReadExcel(path, ref m_Content))
        {
            HandlerData(m_Content,out thriftOutput,out xmlOutput);
        }
        else
        {
            thriftOutput = null;
            xmlOutput = null;
        }
    }
    public abstract void HandlerData(List<string[][]> content, out TBase thriftOutput, out XmlConfigBase xmlOutput);
    protected bool IsSkipLine(string[] line)
    {
        for (int j = 0; j < line.Length; ++j)
        {
            if (!string.IsNullOrEmpty(line[j]))
            {
                return false;
            }
        }
        return true;
    }
}

