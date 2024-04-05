using FinalProject.Interface;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPhotoService _photoService;


        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();
            ViewBag.Categories = await _categoryRepository.GetAll();
            ViewBag.Suppliers = await _supplierRepository.GetAll();
            return View(products);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository, IPhotoService photoService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductCreateEditViewModel
            {
                Categories = await _categoryRepository.GetAll(),
                Suppliers = await _supplierRepository.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageUrl;
                if (model.Image != null)
                {
                    var result = await _photoService.AddPhotoAsync(model.Image);
                    imageUrl = result.Url.ToString();
                }
                else
                {
                    // Đường dẫn mặc định nếu người dùng không nhập ảnh
                    imageUrl = "https://e7.pngegg.com/pngimages/980/697/png-clipart-empty-set-null-set-computer-icons-mathematics-mathematics-triangle-symbol-thumbnail.png";
                }

                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Image = imageUrl,
                    Quantity = model.Quantity,
                    CategoryId = model.CategoryId,
                    SupplierId = model.SupplierId
                };

                _productRepository.Add(product);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            model.Categories = await _categoryRepository.GetAll();
            model.Suppliers = await _supplierRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductCreateEditViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
                Categories = await _categoryRepository.GetAll(),
                Suppliers = await _supplierRepository.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productRepository.GetByIdAsync(model.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                // Kiểm tra xem người dùng đã chọn tập tin hình ảnh mới hay chưa
                if (model.Image != null)
                {
                    // Nếu người dùng đã chọn tập tin hình ảnh mới, thực hiện quá trình tải lên hình ảnh mới
                    var result = await _photoService.AddPhotoAsync(model.Image);
                    product.Image = result.Url.ToString();
                } else
                {
                    
                }
                // Nếu người dùng không chọn tập tin hình ảnh mới, giữ nguyên đường dẫn URL của sản phẩm

                // Cập nhật thông tin của sản phẩm
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.CategoryId = model.CategoryId;
                product.SupplierId = model.SupplierId;

                // Lưu thay đổi vào cơ sở dữ liệu
                _productRepository.Update(product);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Invalid data");
            }

            model.Categories = await _categoryRepository.GetAll();
            model.Suppliers = await _supplierRepository.GetAll();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _productRepository.GetByIdAsync(id);
            if (productDetails == null) return View("Error");
            return View(productDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productDetails = await _productRepository.GetByIdAsync(id);
            if (productDetails == null) return View("Error");

            _productRepository.Delete(productDetails);
            return RedirectToAction("Index");
        }

    }
}
