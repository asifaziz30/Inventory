global using AutoMapper;
global using Inventory.Service.Contracts.Service;
global using Kendo.Mvc.Extensions;
global using Kendo.Mvc.UI;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Inventory.Service.Models.DTO.Request;
global using Inventory.Service.Models.DTO.Response;
global using Microsoft.Extensions.Localization;
global using System.Linq;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Inventory.Service.Models;
global using System.ComponentModel.DataAnnotations;
global using Inventory.Service.Services;
global using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;
global using Microsoft.Extensions.Configuration;
global using System.Data;
global using System.Reflection;
global using System.Threading;
global using Microsoft.AspNetCore.Localization;
using System.Security.Claims;
using Inventory.Services.Helper;

namespace Inventory.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _configuration;
        public BaseController(IConfiguration configuration) => _configuration = configuration;
        public int ShipperId
        {
            get
            {
                int shipperId = 300;
                //if (User.Identity.IsAuthenticated)
                //{
                //    var userData = ((ClaimsIdentity)User.Identity).Claims.Where(d => d.Type == ClaimTypes.UserData).FirstOrDefault().Value;
                //    var shipperResponse = System.Text.Json.JsonSerializer.Deserialize<ShipperCookieResponse>(userData);
                //    userId = shipperResponse is not null ? shipperResponse.AspNetUserId : 0;
                //}
                return shipperId;
            }
        }
        public string ShipperName
        {
            get
            {
                string shipper = "Sabir Ali";
                //if (User.Identity.IsAuthenticated)
                //{
                //    var userData = ((ClaimsIdentity)User.Identity).Claims.Where(d => d.Type == ClaimTypes.UserData).FirstOrDefault().Value;
                //    var shipperResponse = System.Text.Json.JsonSerializer.Deserialize<ShipperCookieResponse>(userData);
                //    userId = shipperResponse is not null ? shipperResponse.AspNetUserId : 0;
                //}
                return shipper;
            }
        }
        public int UserId
        {
            get
            {
                int userId = 8;
                //if (User.Identity.IsAuthenticated)
                //{
                //    var userData = ((ClaimsIdentity)User.Identity).Claims.Where(d => d.Type == ClaimTypes.UserData).FirstOrDefault().Value;
                //    var shipperResponse = System.Text.Json.JsonSerializer.Deserialize<ShipperCookieResponse>(userData);
                //    userId = shipperResponse is not null ? shipperResponse.AspNetUserId : 0;
                //}
                return userId;
            }
        }
        
        public short CountryId
        {
            get
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    var userData = ((ClaimsIdentity)User.Identity).Claims.Where(d => d.Type == ClaimTypes.UserData).FirstOrDefault().Value;
                //    var shipperResponse = System.Text.Json.JsonSerializer.Deserialize<ShipperCookieResponse>(userData);
                //    return (short)(shipperResponse is not null ? shipperResponse.CountryId : 0);
                //}
                return _configuration.GetValue<short>("AppSettings:Default:CountryId");
            }
        }
        public string CountryName
        {
            get
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    var userData = ((ClaimsIdentity)User.Identity).Claims.Where(d => d.Type == ClaimTypes.UserData).FirstOrDefault().Value;
                //    var shipperResponse = System.Text.Json.JsonSerializer.Deserialize<ShipperCookieResponse>(userData);
                //    return (short)(shipperResponse is not null ? shipperResponse.CountryId : 0);
                //}
                return _configuration.GetValue<string>("AppSettings:Default:CountryName");
            }
        }
       
        
        public string UserName
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return User.Identity.Name;
                }
                return "Inventory User";
            }
        }
        public List<int> UserRoles
        {
            get
            {
                return new List<int>();
            }
        }
     
    }
}
