using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Authorization;

namespace Ozds.Server.Controllers.Api.V1;

[ApiVersion("1")]
[ApiKeyAuth]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public abstract class ApiV1ControllerBase : Controller
{
}
