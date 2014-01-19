﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPushkin.BetterRobokassa
{
    public static class RobokassaConfig
    {
        public static string Login
        {
            get
            {
                return ConfigurationManager.AppSettings["RobokassaLogin"];
            }
        }

        public static string Pass1
        {
            get
            {
                return ConfigurationManager.AppSettings["RobokassaPass1"];
            }
        }

        public static string Pass2
        {
            get
            {
                return ConfigurationManager.AppSettings["RobokassaPass2"];
            }
        }

        public static RobokassaMode Mode
        {
            get
            {
                string mode = ConfigurationManager.AppSettings["RobokassaMode"];
                switch(mode.ToLower())
                {
                    case "test":
                        return RobokassaMode.Test;
                    case "production":
                        return RobokassaMode.Production;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}
