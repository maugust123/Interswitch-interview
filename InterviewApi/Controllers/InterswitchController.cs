using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;
using InterviewApi.BusinessLogic.Services.PaymentNotification;
using InterviewApi.BusinessLogic.Services.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewApi.Controllers
{
    /// <summary>
    /// InterSwitch end points
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InterSwitchController : BaseController
    {
        private readonly ICustomerValidationService _customerValidation;
        private readonly IPaymentNotificationService _notificationService;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="customerValidation"></param>
        /// <param name="notificationService"></param>
        public InterSwitchController(ICustomerValidationService customerValidation, IPaymentNotificationService notificationService)
        {
            _customerValidation = customerValidation;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("validate-customer", Name = "ValidateCustomer")]
        [HttpPost]
        [ProducesResponseType(typeof(ValidateCustomerResponse), 200)]
        public async Task<IActionResult> ValidateCustomer([FromBody]ValidateCustomerModel model)
        {
            var result = await _customerValidation.ValidateCustomer(model);
            return Ok(result);
        }

        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("send-payment-notification", Name = "SendPaymentNotification")]
        [HttpPost]
        [ProducesResponseType(typeof(PaymentNotificationResponse), 200)]
        public async Task<IActionResult> SendPaymentNotification([FromBody]PaymentNotificationModel model)
        {
            var result = await _notificationService.SendPaymentNotification(model);
            return Ok(result);
        }
    }
}