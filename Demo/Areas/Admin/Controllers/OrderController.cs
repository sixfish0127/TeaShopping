using Demo.DataAccess.Repository;
using Demo.DataAccess.Repository.IRepository;
using Demo.Models;
using Demo.Models.ViewModels;
using Demo.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.Areas.Admin.Controllers
{   
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Details(int orderId)
        {
            OrderVM  = new OrderVM
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };
            return View(OrderVM);   
        }
        [HttpPost]
        [Authorize(Roles =SD.Role_Admin+","+SD.Role_Employee+","+SD.Role_Manager)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name= OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber= OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.Address= OrderVM.OrderHeader.Address;
            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "訂購人資訊更新成功";
            return RedirectToAction(nameof(Details),new {orderId= orderHeaderFromDb.Id});
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult StartProcessing()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusInProcess);
            _unitOfWork.Save();
            TempData["Success"] = "訂單更新成功";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult OrderReady()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusReady);
            _unitOfWork.Save();
            TempData["Success"] = "訂單更新成功";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult OrderCompleted()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusCompleted);
            _unitOfWork.Save();
            TempData["Success"] = "訂單更新成功";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult CancelOrder()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusCancelled);
            _unitOfWork.Save();
            TempData["Success"] = "訂單更新成功";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetALL(string status)
        {
            IEnumerable<OrderHeader> objOrderHerders;
            if(User.IsInRole(SD.Role_Admin)|| User.IsInRole(SD.Role_Employee) || User.IsInRole(SD.Role_Manager) )
            {
                objOrderHerders= _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId =
                claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                objOrderHerders = _unitOfWork.OrderHeader.GetAll(u=>u.ApplicationUserId==userId ,includeProperties: "ApplicationUser").ToList();
            }
            switch (status)
            {
                case "Pending":
                    objOrderHerders = objOrderHerders.Where(u => u.OrderStatus == SD.StatusPending);
                    break;
                case "Processing":
                    objOrderHerders = objOrderHerders.Where(u => u.OrderStatus == SD.StatusInProcess);
                    break;
                case "Ready":
                    objOrderHerders = objOrderHerders.Where(u => u.OrderStatus == SD.StatusReady);
                    break;
                case "Completed":
                    objOrderHerders = objOrderHerders.Where(u => u.OrderStatus == SD.StatusCompleted);
                    break;
                case "Cancelled":
                    objOrderHerders = objOrderHerders.Where(u => u.OrderStatus == SD.StatusCancelled);
                    break;
                default:
                    break;
            }
            return Json(new { data = objOrderHerders });
        }
        #endregion
    }
}
