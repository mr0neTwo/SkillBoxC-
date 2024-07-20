using Microsoft.AspNetCore.Mvc;

namespace PhoneNotes.ViewComponents;

public sealed class NavigationViewComponent : ViewComponent
{
	public IViewComponentResult Invoke()
	{
		return View();
	}
}