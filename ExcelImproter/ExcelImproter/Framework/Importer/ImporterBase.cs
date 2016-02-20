//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : ImporterBase
//
// Created by : Baoxue at 2016/2/20 16:14:37
//
//
//========================================================================

using System;
using System.Collections.Generic;
using Thrift.Protocol;
public interface ImporterBase
{
    void Importer(string path,out TBase thriftOutput,out XmlConfigBase xmlOutput);
}

