using System;
using System.Collections.Generic;
using System.Linq;
using AutoFac.ServiceReference;
using System.Text;
using System.Threading.Tasks;

namespace AutoFac
{
	public class ControllerIII
	{
		private readonly IServiceClientFactory serviceClientFactory;

		public ControllerIII(IServiceClientFactory serviceClientFactory)
		{
			this.serviceClientFactory = serviceClientFactory;
		}

		public string MakeServiceCall()
		{
			var client = serviceClientFactory.CreateChannel();
			var response = "";
			try
			{
				client.Open();

				response = client.GetData();

				client.Close();

				//return response;
			}
			catch (Exception e)
			{
				client.Abort();

			}
			return response;

		}
	}
}



public interface IServiceClientFactory
{
	ServiceClient CreateChannel();
}

public class ServiceClientFactory : IServiceClientFactory
{
	public ServiceClient CreateChannel()
	{
		return new ServiceClient();
	}
}