using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetInformatika.Data;
using NetInformatika.Data.Tables;
using NetInformatika.Views.Models;
using System.Globalization;

namespace NetInformatika.Controllers;

[Authorize]
public class TripsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public TripsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    /// <summary>
    /// GET trips
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);
        return _context.Trips != null ? View(await _context.Trips.Where(u => u.UserId == user!.Id).ToListAsync()) : Problem("Entity set 'ApplicationDbContext.Trips' is null.");
    }

    /// <summary>
    /// POST trips
    /// </summary>
    /// <param name="trip"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("trips")]
    public async Task<IActionResult> Create([FromBody] TripViewModel trip)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.FindByNameAsync(User!.Identity!.Name!);

        // Convert StartedOn string to DateTime
        DateTime startedOn;
        if (!DateTime.TryParseExact(trip.StartedOn, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startedOn))
        {
            // Handle invalid datetime format
            return BadRequest("Invalid StartedOn format");
        }

        // Create Trip
        _context.Trips.Add(new Trip
        {
            UserId = user!.Id,
            StartLocation = trip.StartLocation,
            DestinationLocation= trip.DestinationLocation,
            StartedOn = startedOn
        });
        await _context.SaveChangesAsync();

        return new OkObjectResult(true);
    }
}
