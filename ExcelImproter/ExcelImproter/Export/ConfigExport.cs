using System;
using System.Collections.Generic;
using System.IO;
using Common.Config;
using Common.Config.Table;
using Communication;
using ExcelImproter.Configs;
using Thrift.Protocol;

namespace ExcelImproter.Export
{
    class ConfigExport
    {
        private GameTestConfigTable config1;
        private GameTestConfigTable config2;
        private const string m_strOutPutPath = "D:/My Documents/Visual Studio 2013/Projects/ExcelImproter/ExcelImproter/config/output/";
        public void ExportConfig1(List<string[][]> content)
        {
            DecodeConfig(content, ref config1);
            SaveFile(m_strOutPutPath + "level1.byte", config1);
        }
        public void ExportConfig2(List<string[][]> content)
        {
            DecodeConfig(content, ref config2);
            SaveFile(m_strOutPutPath + "level2.byte", config2);
        }
        private void DecodeConfig(List<string[][]> content, ref GameTestConfigTable config)
        {
            config = new GameTestConfigTable();

            config.FeedConfigList           = new List<FeedConfig>();
            config.ExchangeConfigList       = new List<ExchangeConfig>();
            config.ExchangeCDConfigList     = new List<ExchangeCDConfig>();

            DecodeFeedConfig(content[0], config.FeedConfigList);
            DecodeExchangeConfig(content[1], config.ExchangeConfigList);
            DecodeExchangeCdConfig(content[2], config.ExchangeCDConfigList);
        }
        private void DecodeFeedConfig(string[][] content, List<FeedConfig> config)
        {
            for (int i = 0; i < content.Length; ++i)
            {
                string[] line = content[i];
                if (IsSkipLine(line))
                {
                    continue;
                }

                string name = line[0];
                int initData = int.Parse(line[1]);
                int maxData = int.Parse(line[2]);
                int dropSpeed = int.Parse(line[3]);
                int feedSpeed = int.Parse(line[4]);
                int feedTime = int.Parse(line[5]);

                FeedConfig elem = new FeedConfig();
                elem.Name = name;
                elem.InitData = initData;
                elem.MaxData = maxData;
                elem.DropSpeed = dropSpeed;
                elem.FeedSpeed = feedSpeed;
                elem.FeedTime = feedTime;

                config.Add(elem);
            }
        }
        private void DecodeExchangeConfig(string[][] content, List<ExchangeConfig> config)
        {
            for (int i = 0; i < content.Length; ++i)
            {
                string[] line = content[i];
                if (IsSkipLine(line))
                {
                    continue;
                }

                int level = int.Parse(line[0]);
                int id = int.Parse(line[1]);
                List<double> rateList = new List<double>();
                for (int j = 2; j < line.Length; ++j)
                {
                    if (!string.IsNullOrEmpty(line[j]))
                    {
                        rateList.Add(GetRate(line[j]));
                    }
                }

                ExchangeConfig elem = new ExchangeConfig();
                elem.Level = level;
                elem.TradersId = id;
                elem.ExchangeRate = rateList;
                config.Add(elem);
            }
        }
        private void DecodeExchangeCdConfig(string[][] content, List<ExchangeCDConfig> config)
        {
            for (int i = 0; i < content.Length; ++i)
            {
                string[] line = content[i];
                if (IsSkipLine(line))
                {
                    continue;
                }

                int type = int.Parse(line[0]);
                int initData = int.Parse(line[1]);
                int maxData = int.Parse(line[2]);
                int cd = int.Parse(line[3]);

                ExchangeCDConfig elem = new ExchangeCDConfig();
                elem.ItemType = type;
                elem.InitData = initData;
                elem.MaxData = maxData;
                elem.Cd = cd;

                config.Add(elem);
            }
        }
        private double GetRate(string data)
        {
            int a = 0;
            int b = 1;

            string tmp = string.Empty;

            for (int i = 0; i < data.Length; ++i)
            {
                if (data[i] == ':' || data[i] == '：')
                {
                    a = int.Parse(tmp);
                    tmp = string.Empty;
                }
                else
                {
                    tmp += data[i];
                }
            }
            b = int.Parse(tmp);

            double res  =  ((double) (a))/((double) (b));
            return res;
        }
        private void SaveFile(string path, TBase source)
        {
            byte[] data = ThriftSerialize.Serialize(source);
            FileUtils.WriteByteFile(path, data);
        }
        private bool IsSkipLine(string[] line)
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
}
