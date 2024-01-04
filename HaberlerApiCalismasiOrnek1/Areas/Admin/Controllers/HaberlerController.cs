﻿using HaberlerApiCalismasiOrnek1.Areas.Admin.Models;
using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]

    public class HaberlerController : Controller
    {

        static readonly ConnectDb connectDb = new();

        [Route("haberlistesi")]
        public async Task<IActionResult> Index()
        {
            List<HaberContent> values = await connectDb.HaberContent.OrderByDescending(x => x.Id).ToListAsync();
            return View(values);
        }

        [Route("haberlistesigroup")]
        public async Task<IActionResult> HaberListesiGroup()
        {
            List<GetHaberlerGroupByQueryResult> haberler = new List<GetHaberlerGroupByQueryResult>();

            //var values = connectDb
            //    .HaberContent.OrderByDescending(x => x.Title).GroupBy(y => y.Title)
            //    .Select(z => new { Title = z.Key, Toplam = z.Count() });

            //foreach (var value in values)
            //{
            //    haberler.Add(new GetHaberlerGroupByQueryResult()
            //    {
            //        Title = value.Title,
            //        TotalCount = value.Toplam,
            //    });
            //}

            //-------------------------------------------------------------------------------------------------------------------------------

            var query = connectDb.HaberContent
                .GroupBy(h => new { h.Title })
                .Select(g => new
                {
                    //Id = g.Key.Id,
                    Title = g.Key.Title,
                    Count = g.Count()
                });

            var results = await query.ToListAsync();

            foreach (var value in results)
            {
                haberler.Add(new GetHaberlerGroupByQueryResult()
                {
                    Title = value.Title,
                    TotalCount = value.Count,
                    //Id = value.Id,
                });
            }

            return View(haberler);
        }

        [Route("haber-guncelle")]
        public async Task<IActionResult> UpdateForm(int id)
        {
            return RedirectToAction("Index", "Default");
        }

        [Route("haber-sil")]
        public IActionResult DeleteForm(int id)
        {
            HaberContent values = connectDb.HaberContent.Find(id);

            if (values != null)
            {
                connectDb.Remove(values);
                connectDb.SaveChanges();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Haberler");
            }
        }
    }
}
