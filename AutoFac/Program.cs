using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFac.ServiceReference;

namespace AutoFac
{
	public class Program
	{
		static void Main(string[] args)
		{
			IService client = new ServiceClient();
			//IServiceClientFactory cfr = new ServiceClientFactory();
			IClientChannelFactory<IServiceChannel> cfr = new ClientChannelFactory<IServiceChannel>() ;
			//ServiceClient client = new ServiceClient();
			//Controller ctrl = new Controller(client);
			ControllerII ctrl = new ControllerII(cfr);
			//ControllerIII ctrl = new ControllerIII(cfr);
			
			Console.WriteLine(ctrl.MakeServiceCall());
			Console.ReadKey();
		}
	}
}
