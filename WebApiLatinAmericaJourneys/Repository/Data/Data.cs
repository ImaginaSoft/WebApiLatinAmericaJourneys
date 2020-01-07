﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApiLatinAmericaJourneys.Repository.Data
{
    public class Data
    {
        const string strSecureAppSettings = "appSettings";


        public static string StrNomServ_WebsSql
        {

            get { return ((NameValueCollection)WebConfigurationManager.GetSection(strSecureAppSettings))[("bdturismo_SOURCE")]; }

        }

        public static string StrBD_WebsSql
        {

            get { return ((NameValueCollection)WebConfigurationManager.GetSection(strSecureAppSettings))[("bdturismo_BD")]; }

        }


        public static string StrUsuario_WebsSql
        {

            get { return ((NameValueCollection)WebConfigurationManager.GetSection(strSecureAppSettings))[("bdturismo_USER")]; }

        }


        public static string StrCnx_WebsSql
        {
            get
            {
                return
              "Data Source=" + StrNomServ_WebsSql +
              ";initial catalog =" + StrBD_WebsSql +
              ";User ID=" + StrUsuario_WebsSql +
              ";Password=" + ((NameValueCollection)WebConfigurationManager.GetSection(strSecureAppSettings))[("bdturismo_PASSWORD")];

            }

            //get
            //{

            //    return "Server=localhost;Database=BDTURISMO;Trusted_Connection=True;";

            //}
        }



    }
}