//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : IImporter
//
// Created by : Baoxue at 2016/2/20 16:14:37
//
//
//========================================================================

using System;
using System.Collections.Generic;
using ExcelImproter.Framework.Importer;
using Thrift.Protocol;

namespace ExcelImproter.Framework.Importer
{
    public interface IImporter
    {
        void Import(string path,out ImporterPkg outPkg);
        string GetPath();
    }

}
