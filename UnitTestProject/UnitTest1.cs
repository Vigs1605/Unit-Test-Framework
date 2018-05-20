using System;
using AutoFac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using AutoFac.ServiceReference;

namespace UnitTestProject
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			IService mock = MockRepository.GenerateMock<IService>(); //fake the IService class and call it mock
			mock.Expect(t => t.GetData()).Return("Hello"); //Set the expectation when the call is made

			Controller classUnderTest = new Controller(mock);//pass the fake object

			Assert.AreEqual("Hello", classUnderTest.CallMyServiceClient());
			mock.VerifyAllExpectations();
		}

		[TestMethod]
		public void TestMethod2()
		{
			var stubProxy = MockRepository.GenerateStub<IServiceChannel>();
			//var stubProxy1 = MockRepository.GenerateMock<ServiceClient>();
			// Stubs the service operation invoked by the class under test
			stubProxy
				.Stub(m => m.GetData())
				.Return("Hello");

			// Fakes out the WCF proxy factory
			var stubProxyFactory = MockRepository.GenerateMock<IClientChannelFactory<IServiceChannel>>();

			// Stubs the factory method to return the mocked proxy
			stubProxyFactory
				.Stub(s => s.CreateChannel())
				.Return(stubProxy);

			var testObject = new ControllerII(stubProxyFactory);

			Assert.AreEqual("Hello", testObject.MakeServiceCall());
			stubProxyFactory.VerifyAllExpectations();


		}
	}
}
