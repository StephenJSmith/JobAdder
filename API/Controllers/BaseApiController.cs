using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BaseApiController : ControllerBase
  {
    protected IConfiguration _config;

    protected void SetConfiguredPageDefaults(PageSpecParams pageParams)
    {
      if (_config == null) { return; }

      if (!int.TryParse(_config["Pagination:MaxPageSize"], out int maxPageSize))
      {
        maxPageSize = 50;
      }

      if (!int.TryParse(_config["Pagination:DefaultPageSize"], out int defaultPageSize))
      {
        defaultPageSize = 10;
      }

      pageParams.ApplyConfigurationDefaults(maxPageSize, defaultPageSize);
    }
  }
}