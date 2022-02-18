﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CoverTypeController : Controller
    {

    private readonly IUnitOfWork _unitOfWork;
    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
        public IActionResult Index()
        {
       IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }
    //Get
    public IActionResult Create()
    {
        return View();
    }

    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
       
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType created succesfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }



    //Get
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id==id);

        if (CoverTypeFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CoverTypeFromDbFirst);
    }

//post
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(CoverType obj)
{

    if (ModelState.IsValid)
    {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType edited succesfully";

            return RedirectToAction("Index");
    }
    return View(obj);
}


    //Get
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id==id);

        if (CoverTypeFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CoverTypeFromDbFirst);
    }

    //post
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
    var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (id == null || id==0)
        {
            return NotFound();
        }
        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType deleted Succesfully";
    return RedirectToAction("Index");
    }
}

