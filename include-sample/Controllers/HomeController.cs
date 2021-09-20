using include_sample.Models;
using include_sample.Views.Home;
using JuniorTennis.Infrastructure.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace include_sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SampleDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            SampleDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var model = new PrivacyViewModel();
            model.StartDateTime = DateTime.Now;

            _context.Database.SetCommandTimeout(120);
            await _context.FirstLayers
                .Include(o => o.SecondLayers)
                .ThenInclude(o => o.ThirdLayers)
                .ThenInclude(o => o.FourthLayers)
                .ToListAsync();

            model.EndDateTime = DateTime.Now;
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/CreateInitialData")]
        public async Task<IActionResult> CreateInitialData()
        {
            // 1層目登録
            var firstLayers = Enumerable.Range(1, 10000)
                .Select(_ => new FirstLayer())
                .ToList();
            await _context.FirstLayers.AddRangeAsync(firstLayers);
            await _context.SaveChangesAsync();

            // 2層目登録
            firstLayers = await _context.FirstLayers.ToListAsync();
            firstLayers.ForEach(async firstLayer =>
            {
                firstLayer.SecondLayers = Enumerable.Range(1, 10)
                    .Select(_ => new SecondLayer())
                    .ToList();

                await _context.SecondLayers.AddRangeAsync(firstLayer.SecondLayers);
            });
            await _context.SaveChangesAsync();

            // 3層目登録
            var secondLayers = await _context.SecondLayers.ToListAsync();
            secondLayers.ForEach(async secondLayer =>
            {
                secondLayer.ThirdLayers = Enumerable.Range(1, 10)
                .Select(_ => new ThirdLayer())
                .ToList();

                await _context.ThirdLayers.AddRangeAsync(secondLayer.ThirdLayers);
            });
            await _context.SaveChangesAsync();

            // 4層目登録
            var thirdLayers = await _context.ThirdLayers.ToListAsync();
            thirdLayers.ForEach(async thirdLayer =>
            {
                thirdLayer.FourthLayers = Enumerable.Range(1, 10)
                    .Select(_ => new FourthLayer())
                    .ToList();

                await _context.FourthLayers.AddRangeAsync(thirdLayer.FourthLayers);
            });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
