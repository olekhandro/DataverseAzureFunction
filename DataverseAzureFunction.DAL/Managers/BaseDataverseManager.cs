using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace DataverseAzureFunction.DAL.Managers
{
    public class BaseDataverseManager:IDisposable
    {
        protected readonly ServiceClient _serviceClient;

        public BaseDataverseManager(string connectionString)
        {
            _serviceClient = new ServiceClient(connectionString);
        }

        public void Dispose()
        {
            _serviceClient.Dispose();
        }
    }
}
