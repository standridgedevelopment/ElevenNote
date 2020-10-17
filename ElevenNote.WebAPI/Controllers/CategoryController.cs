using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        CategoryService categoryService = new CategoryService();
        public IHttpActionResult Get()
        {
            var category = categoryService.GetCategories();
            return Ok(category);
        }
        public IHttpActionResult Get(int id)
        {
            var category = categoryService.GetCategoryById(id);
            return Ok(category);
        }
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!categoryService.CreateCategory(category))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Put(Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (!categoryService.UpdateCategory(category)) return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if (!categoryService.DeleteCategory(id)) return InternalServerError();

            return Ok();
        }
    }
}
