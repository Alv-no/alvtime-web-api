﻿using AlvTime.Business.FlexiHours;
using AlvTimeWebApi.HelperClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AlvTimeWebApi.Controllers.FlexiHours
{
    [Route("api/user")]
    [ApiController]
    public class FlexiHourController : Controller
    {
        private readonly IFlexiHourStorage _storage;
        private RetrieveUsers _userRetriever;

        public FlexiHourController(RetrieveUsers userRetriever, IFlexiHourStorage storage)
        {
            _storage = storage;
            _userRetriever = userRetriever;
        }

        [HttpGet("FlexiHours")]
        [Authorize]
        public ActionResult<FlexHoursResponseDto> FetchTotalFlexiHours(DateTime startDate, DateTime endDate)
        {
            var user = _userRetriever.RetrieveUser();

            return Ok(_storage.GetTotalFlexiHours(user.Id, startDate, endDate));
        }
    }
}