using FinalProject.Interface;
using FinalProject.Models;
using FinalProject.Repository;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Supplier> suppliers = await _supplierRepository.GetAll();
            return View(suppliers);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Supplier supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var createSupplierViewModel = new SupplierCreateEditViewModel();
            return View("CreateEdit", createSupplierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    Name = model.Name,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };
                _supplierRepository.Add(supplier);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to add supplier.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var editSupplierViewModel = new SupplierCreateEditViewModel
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Address = supplier.Address,
                PhoneNumber = supplier.PhoneNumber
            };

            return View("CreateEdit", editSupplierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplierCreateEditViewModel model)
        {
            if (id != model.SupplierId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier == null)
                {
                    return NotFound();
                }

                supplier.Name = model.Name;
                supplier.Address = model.Address;
                supplier.PhoneNumber = model.PhoneNumber;

                _supplierRepository.Update(supplier);
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var supplierDetails = await _supplierRepository.GetByIdAsync(id);
            if (supplierDetails == null) return View("Error");
            return View(supplierDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplierDetails = await _supplierRepository.GetByIdAsync(id);
            if (supplierDetails == null) return View("Error");

            _supplierRepository.Delete(supplierDetails);
            return RedirectToAction("Index");
        }



    }
}
