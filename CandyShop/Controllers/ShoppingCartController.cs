using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.Models;
using CandyShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CandyShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(ICandyRepository candyRepository, ShoppingCart shoppingCart)
        {
            _candyRepository = candyRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            var shoppingCardViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal=_shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCardViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int candyId)
        {
            var selectedCandy = _candyRepository.GetAllCandy.First(x => x.CandyId == candyId);
            if (selectedCandy != null)
            {
                _shoppingCart.AddToCart(selectedCandy,1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int candyId)
        {
            var selectedCandy = _candyRepository.GetAllCandy.First(x => x.CandyId == candyId);
            if (selectedCandy != null)
            {
                _shoppingCart.RemoveFromCart(selectedCandy);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearShoppingCart()
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }
    }
}