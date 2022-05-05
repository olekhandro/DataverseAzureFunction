using System;
using System.Collections.Generic;
using System.Text;

namespace DataverseAzureFunction.Tests
{
    internal class AppConstants
    {
        // TODO: move to app config for tests
        public const string CONNECTIONSTRING = @"AuthType=OAuth;
                         Url=https://org9f7928c0.crm4.dynamics.com/;
                         UserName=alexanderzakharov@ontrial.onmicrosoft.com;
                         Password=Iskander1!;
                         ClientId=;
                         RedirectUri=;
                         Prompt=Auto;
                         RequireNewInstance=True";
    }
}
