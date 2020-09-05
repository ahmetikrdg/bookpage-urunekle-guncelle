using bookpage.business.Abstract;
using bookpage.entity;
using bookpage.webui.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookpage.webui.Controllers
{
    public class AdminController:Controller
    {
        private IProductServices _productServices;
        public AdminController(IProductServices productServices)
        {
            this._productServices=productServices;
        }
        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products=_productServices.GetAll(),
            });
        }
        [HttpGet]
        public IActionResult ProductCreate()
        {//ProductCreate içindeki bilgileri getirdim.
            return View();
        }
        [HttpPost]
        public IActionResult ProductCreate(ProductModel model)
        {
            var entity=new Product()
            {
                Name=model.Name,
                Url=model.Url,
                Author=model.Author,
                Pages=model.Pages,
                Description=model.Description,
                ImageUrl=model.ImageUrl,
            };
            _productServices.Create(entity);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult ProductEdit(int? id)//gelen idye göre sorgular gelen idye göre bilgiyi göstericezb
        {
            if(id==null)
            {
                return NotFound();
            }
            var entity=_productServices.GetById((int)id);//bunla gittim ürünü buldum entity içine atadım
            var model=new ProductModel()//buradada entity içinden modele atıyorum
            {
                ProductId=entity.ProductId,
                Name=entity.Name,
                Url=entity.Url,
                Author=entity.Author,
                Pages=entity.Pages,
                Description=entity.Description,
                ImageUrl=entity.ImageUrl
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ProductEdit(ProductModel model)
        {
            var entity=_productServices.GetById(model.ProductId);//modelin productıdsini buldum getirdim eski modeldekiyle yenisini değiştiricem
            if(entity==null)
            {
                return NotFound();
            }
            entity.Name=model.Name;
            entity.Author=model.Author;
            entity.Url=model.Url;
            entity.Pages=model.Pages;
            entity.Description=model.Description;
            entity.ImageUrl=model.ImageUrl;

            _productServices.Update(entity);
            //yukarıdan vtden aldı modelin içine attı, burada modelin içinden buldu yeni modelin içine attı update ile vtye gönderi kaydetti.
            return RedirectToAction("ProductList");
        }


    }
}