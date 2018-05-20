using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFac.ServiceReference;

namespace AutoFac
{
	public interface IClientChannelFactory<TChannel>
		where TChannel : IServiceChannel
	{
		void Open();

		void Close();

		void Abort();

		TChannel CreateChannel();
	}

	public class ClientChannelFactory<TChannel> : IClientChannelFactory<TChannel>
	where TChannel : IServiceChannel
	{
		private System.ServiceModel.ChannelFactory<TChannel> factory;

		public ClientChannelFactory(string endpointConfigurationName)
		{
			this.factory = new System.ServiceModel.ChannelFactory<TChannel>(endpointConfigurationName);
		}

		public ClientChannelFactory()
		{
			this.factory = new System.ServiceModel.ChannelFactory<TChannel>("BasicHttpBinding_IService");
		}

		public void Open()
		{
			this.factory.Open();
		}

		public void Close()
		{
			this.factory.Close();
		}

		public void Abort()
		{
			this.factory.Abort();
		}

		public TChannel CreateChannel()
		{
			return this.factory.CreateChannel();
		}
	}

	public class ControllerII
	{
		private IClientChannelFactory<IServiceChannel> proxyFactory;

		public ControllerII(IClientChannelFactory<IServiceChannel> proxyFactory)
		{
			this.proxyFactory = proxyFactory;
		}

		public string MakeServiceCall()
		{
			// Validate the specified Email address

			IServiceChannel proxy= proxyFactory.CreateChannel(); ;
			var response = "";
			try
			{
				proxy.Open();
				response = proxy.GetData();
	
			// Do some processing on the results

				proxy.Close();

				return response;
			}
			catch (Exception e)
			{
				proxy.Abort();

			}
			return response;
		}
	}

}
