using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TalkApiClient.Ui.WebApp.Areas.Manager
{
	[Area("Manager")]
	[Authorize]
	public class ManagerBaseController: Controller
	{
	}
}
