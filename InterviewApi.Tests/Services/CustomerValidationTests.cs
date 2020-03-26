using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;
using InterviewApi.BusinessLogic.Services.HttpWraper;
using InterviewApi.BusinessLogic.Services.Validation;
using InterviewApi.BusinessLogic.Services.Validation.ValidateCustomerModel;
using InterviewApi.Common.Exceptions;
using InterviewApi.Common.Extensions;
using Moq;
using NUnit.Framework;

namespace InterviewApi.Tests.Services
{
    [TestFixture]
    public class CustomerValidationTests
    {
        private Mock<IValidateCustomerModelService> _mockValidationService;
        private Mock<IHttpClientWrapper> _mockHttpClientWrapperService;
        private ICustomerValidationService _customerService;

        [SetUp]
        public void SetUp()
        {
            _mockValidationService = new Mock<IValidateCustomerModelService>();
            _mockHttpClientWrapperService = new Mock<IHttpClientWrapper>();
            _customerService = new CustomerValidationService(_mockHttpClientWrapperService.Object, _mockValidationService.Object);

        }

        [TearDown]
        public void TearDown()
        {
            _mockValidationService = null;
            _customerService = null;
            _mockHttpClientWrapperService = null;
        }

        [Test]
        public async Task Test_ValidateCustomer_Should_Call_IValidateCustomerModelService()
        {
            HttpContent content = new StringContent(new ValidateCustomerResponse().ToJson());
            _mockValidationService.Setup(service => service.ValidateCustomer(It.IsAny<ValidateCustomerModel>(), It.IsAny<List<string>>())).Returns(true);
            _mockHttpClientWrapperService.Setup(service =>
                service.Post(It.IsAny<string>(), It.IsAny<ValidateCustomerModel>(), It.IsAny<string>())
                ).Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content });

            await _customerService.ValidateCustomer(new ValidateCustomerModel());
            _mockValidationService.Verify(service => service.ValidateCustomer(It.IsAny<ValidateCustomerModel>(), It.IsAny<List<string>>()), Times.Once);
            _mockHttpClientWrapperService.Verify(service => service.Post(It.IsAny<string>(), It.IsAny<ValidateCustomerModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Test_ValidateCustomer_Should_Throw_ApplicationValidationException()
        {
            Assert.ThrowsAsync<ApplicationValidationException>(() => _customerService.ValidateCustomer(new ValidateCustomerModel()));
        }

        [Test]
        public void Test_ValidateCustomer_Does_Not_Throw_ApplicationValidationException()
        {
            HttpContent content = new StringContent(new ValidateCustomerResponse().ToJson());
            _mockValidationService.Setup(service => service.ValidateCustomer(It.IsAny<ValidateCustomerModel>(), It.IsAny<List<string>>())).Returns(true);
            _mockHttpClientWrapperService.Setup(service =>
                service.Post(It.IsAny<string>(), It.IsAny<ValidateCustomerModel>(), It.IsAny<string>())
            ).Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content });

            Assert.DoesNotThrowAsync(() => _customerService.ValidateCustomer(new ValidateCustomerModel()));
        }

        [Test]
        public async Task Test_ValidateCustomer_Should_Return_ValidateCustomerResponse_Object()
        {
            HttpContent content = new StringContent(new ValidateCustomerResponse().ToJson());
            _mockValidationService.Setup(service => service.ValidateCustomer(It.IsAny<ValidateCustomerModel>(), It.IsAny<List<string>>())).Returns(true);
            _mockHttpClientWrapperService.Setup(service =>
                service.Post(It.IsAny<string>(), It.IsAny<ValidateCustomerModel>(), It.IsAny<string>())
            ).Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content });

            var result = await _customerService.ValidateCustomer(It.IsAny<ValidateCustomerModel>());

            Assert.IsInstanceOf(typeof(ValidateCustomerResponse), result);
        }

    }
}
