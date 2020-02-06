using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.Models;
using CandyShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CandyShop.Controllers
{
    public class CandyController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CandyController(ICandyRepository candyRepository,
            ICategoryRepository categoryRepository)
        {
            _candyRepository = candyRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category)
        {
            //ViewBag.CurrentCategory = "Bestsellers";
            //return View(_candyRepository.GetAllCandy);

            //vracanje svih slatkisa
            //var candyListViewModel = new CandyListViewModel();
            //candyListViewModel.Candies = _candyRepository.GetAllCandy;
            //candyListViewModel.CurrentCategory = "Bestssellers";
            //return View(candyListViewModel);
            var candyListViewodel = new CandyListViewModel();
            if (!string.IsNullOrEmpty(category))
                candyListViewodel.Candies = _candyRepository.GetAllCandy.Where(x => x.Category.CategoryName==category);
            else
                candyListViewodel.Candies = _candyRepository.GetAllCandy.OrderBy(x => x.Name);
            return View(candyListViewodel);
        }

        public IActionResult Details(int CandyId)
        {
            var candy = _candyRepository.GetCandyById(CandyId);
            if (candy == null)
                return NotFound();
            return View(candy);
        }

    }
}