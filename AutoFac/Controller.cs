using AutoFac.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFac
{
	public class Controller
	{
		private IService client;
		public Controller(IService wcfClient)
		{
			client = wcfClient;
		}
		public Controller()
		{
			client = new ServiceClient();
		}
		public string CallMyServiceClient()
		{
			
			string result = client.GetData();
			return result;
		}
	}
}
