using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        // o service do departamento está aqui para que eu possa associar o departamento com o vendedor
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            /* 1 - pego a lista de departamentos e armazeno na variável "departments"
             * 2 - pego a variável "viewModel" e instancio meu objeto SellerFormViewModel alimento o objeto
             *  com a minha lista de departamento salvo na var "departments".
             *  Retorno para minha view a var "viewModel" com o valor do meu objeto SellerFormViewModel armazenado
             * 
             * */
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // foi necessário colocar o .Value pq p id acima está como opcional
            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

      
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // foi necessário colocar o .Value pq p id acima está como opcional
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }



    }
}