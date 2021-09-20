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
        private readonly SampleDbContext context;

        public HomeController(
            ILogger<HomeController> logger,
            SampleDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Measure1()
        {
            var model = new Measure1ViewModel();
            model.StartDateTime = DateTime.Now;

            context.Database.SetCommandTimeout(120);
            var firstLayers = await context.FirstLayers
                .Include(o => o.SecondLayersA)
                .ThenInclude(o => o.ThirdLayers)
                .Include(o => o.SecondLayersB)
                .ThenInclude(o => o.ThirdLayers)
                .ToListAsync();

            model.EndDateTime = DateTime.Now;
            model.FirstLayerCount = firstLayers.Count();

            var secondLayersA = firstLayers.SelectMany(o => o.SecondLayersA).ToList();
            var thirdLayersA = secondLayersA.SelectMany(o => o.ThirdLayers).ToList();
            model.SecondLayerACount = secondLayersA.Count();
            model.ThirdLayerACount = thirdLayersA.Count();

            var secondLayersB = firstLayers.SelectMany(o => o.SecondLayersB).ToList();
            var thirdLayersB = secondLayersB.SelectMany(o => o.ThirdLayers).ToList();
            model.SecondLayerBCount = secondLayersB.Count();
            model.ThirdLayerBCount = thirdLayersB.Count();

            return View(model);
        }

        public async Task<IActionResult> Measure2()
        {
            var model = new Measure2ViewModel();
            model.StartDateTime = DateTime.Now;

            context.Database.SetCommandTimeout(120);
            var firstLayers = await context.FirstLayers.ToListAsync();
            var firstLayerIds = firstLayers.Select(o => o.Id).ToList();

            var secondLayersA = await context.SecondLayersA
                .Where(o => firstLayerIds.Any(p => p == o.FirstLayerId))
                .ToListAsync();
            var secondLayerAIds = secondLayersA.Select(o => o.Id).ToList();

            var thirdLayersA = await context.ThirdLayersA
                .Where(o => secondLayerAIds.Any(p => p == o.SecondLayerAId))
                .ToListAsync();

            var secondLayersB = await context.SecondLayersB
                .Where(o => firstLayerIds.Any(p => p == o.FirstLayerId))
                .ToListAsync();
            var secondLayerBIds = secondLayersB.Select(o => o.Id).ToList();

            var thirdLayersB = await context.ThirdLayersB
                .Where(o => secondLayerBIds.Any(p => p == o.SecondLayerBId))
                .ToListAsync();

            model.EndDateTime = DateTime.Now;
            model.FirstLayerCount = firstLayers.Count();
            model.SecondLayerACount = secondLayersA.Count();
            model.ThirdLayerACount = thirdLayersA.Count();
            model.SecondLayerBCount = secondLayersB.Count();
            model.ThirdLayerBCount = thirdLayersB.Count();

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
            var firstLayers = Enumerable.Range(1, 1000)
                .Select(_ => new FirstLayer())
                .ToList();
            await context.FirstLayers.AddRangeAsync(firstLayers);
            await context.SaveChangesAsync();

            // 2層目 - A 登録
            firstLayers = await context.FirstLayers.ToListAsync();
            firstLayers.ForEach(async firstLayer =>
            {
                firstLayer.SecondLayersA = Enumerable.Range(1, 11)
                    .Select(_ => new SecondLayerA())
                    .ToList();

                await context.SecondLayersA.AddRangeAsync(firstLayer.SecondLayersA);
            });
            await context.SaveChangesAsync();

            // 3層目 - A 登録
            var secondLayersA = await context.SecondLayersA.ToListAsync();
            secondLayersA.ForEach(async secondLayer =>
            {
                secondLayer.ThirdLayers = Enumerable.Range(1, 2)
                    .Select(_ => new ThirdLayerA())
                    .ToList();

                await context.ThirdLayersA.AddRangeAsync(secondLayer.ThirdLayers);
            });
            await context.SaveChangesAsync();

            // 2層目 - B 登録
            firstLayers = await context.FirstLayers.ToListAsync();
            firstLayers.ForEach(async firstLayer =>
            {
                firstLayer.SecondLayersB = Enumerable.Range(1, 12)
                    .Select(_ => new SecondLayerB())
                    .ToList();

                await context.SecondLayersB.AddRangeAsync(firstLayer.SecondLayersB);
            });
            await context.SaveChangesAsync();

            // 3層目 - B 登録
            var secondLayersB = await context.SecondLayersB.ToListAsync();
            secondLayersB.ForEach(async secondLayer =>
            {
                secondLayer.ThirdLayers = Enumerable.Range(1, 2)
                    .Select(_ => new ThirdLayerB())
                    .ToList();

                await context.ThirdLayersB.AddRangeAsync(secondLayer.ThirdLayers);
            });
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
