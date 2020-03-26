using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;
using InterviewApi.BusinessLogic.Services.HttpWraper;
using InterviewApi.BusinessLogic.Services.PaymentNotification;
using InterviewApi.BusinessLogic.Services.PaymentNotification.Validation;
using InterviewApi.Common.Exceptions;
using InterviewApi.Common.Extensions;
using Moq;
using NUnit.Framework;

namespace InterviewApi.Tests.Services
{
    [TestFixture]
    public class PaymentNotificationTests
    {
        private Mock<IPaymentValidationService> _mockPValidationService;
        private Mock<IHttpClientWrapper> _mockHttpClientWrapperService;
        private IPaymentNotificationService _paymentNotification;

        [SetUp]
        public void SetUp()
        {
            _mockPValidationService = new Mock<IPaymentValidationService>();
            _mockHttpClientWrapperService = new Mock<IHttpClientWrapper>();
            _paymentNotification = new PaymentNotificationService(_mockHttpClientWrapperService.Object, _mockPValidationService.Object);

        }

        [TearDown]
        public void TearDown()
        {
            _mockPValidationService = null;
            _paymentNotification = null;
            _mockHttpClientWrapperService = null;
        }


        [Test]
        public async Task Test_SendPaymentNotification_Should_Call_IPaymentValidationService()
        {
            HttpContent content = new StringContent(new PaymentNotificationModel().ToJson());
            _mockPValidationService.Setup(service => service.ValidatePaymentNotification(It.IsAny<PaymentNotificationModel>(), It.IsAny<List<string>>())).Returns(true);
            _mockHttpClientWrapperService.Setup(service =>
                service.Post(It.IsAny<string>(), It.IsAny<PaymentNotificationModel>(), It.IsAny<string>())
                ).Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content });

            await _paymentNotification.SendPaymentNotification(new PaymentNotificationModel());
            _mockPValidationService.Verify(service => service.ValidatePaymentNotification(It.IsAny<PaymentNotificationModel>(), It.IsAny<List<string>>()), Times.Once);
            _mockHttpClientWrapperService.Verify(service => service.Post(It.IsAny<string>(), It.IsAny<PaymentNotificationModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Test_SendPaymentNotification_Should_Throw_ApplicationValidationException()
        {
            Assert.ThrowsAsync<ApplicationValidationException>(() => _paymentNotification.SendPaymentNotification(new PaymentNotificationModel()));
        }

        [Test]
        public void Test_SendPaymentNotification_Does_Not_Throw_ApplicationValidationException()
        {

            HttpContent content = new StringContent(new PaymentNotificationModel().ToJson());
            _mockPValidationService.Setup(service => service.ValidatePaymentNotification(It.IsAny<PaymentNotificationModel>(), It.IsAny<List<string>>())).Returns(true);
            _mockHttpClientWrapperService.Setup(service =>
                service.Post(It.IsAny<string>(), It.IsAny<PaymentNotificationModel>(), It.IsAny<string>())
            ).Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content });

            Assert.DoesNotThrowAsync(() => _paymentNotification.SendPaymentNotification(new PaymentNotificationModel()));
        }

        [Test]
        public async Task Test_SendPaymentNotification_Should_Return_PaymentNotificationResponse_Object()
        {
            HttpContent content = new StringContent(new ValidateCustomerResponse().ToJson());
            _mockPValidationService.Setup(service => service.ValidatePaymentNotification(It.IsAny<PaymentNotificationModel>(), It.IsAny<List<string>>())).Returns(true);
            _mockHttpClientWrapperService.Setup(service =>
                service.Post(It.IsAny<string>(), It.IsAny<PaymentNotificationModel>(), It.IsAny<string>())
            ).Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content });

            var result = await _paymentNotification.SendPaymentNotification(new PaymentNotificationModel());

            Assert.IsInstanceOf(typeof(PaymentNotificationResponse), result);
        }
    }
}
