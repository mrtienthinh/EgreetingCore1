using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Egreeting.Web.Controllers.Admin
{
    public class DummyDataController : BaseController
    {
        // GET: DummyData
        public ActionResult Index()
        {
            var faker = new Faker("en");
            using (var context = new EgreetingContext())
            {
                //20 category;
                var categories = new List<Category>();
                categories.Add(new Category { CategoryID = 1, CategoryName = "Birthday", CategorySlug = "birthday" });
                categories.Add(new Category { CategoryID = 2, CategoryName = "Wedding", CategorySlug = "wedding" });
                categories.Add(new Category { CategoryID = 3, CategoryName = "New year", CategorySlug = "new-year" });
                categories.Add(new Category { CategoryID = 4, CategoryName = "Festivals", CategorySlug = "festivals" });

                for (int i = 5; i < 20; i++)
                {
                    categories.Add(new Category
                    {
                        CategoryID = i,
                        CategoryName = "Category " + i,
                        CategorySlug = "Category-" + i,
                        CreatedDate = faker.Date.Past(),
                    });
                }
                context.Set<Category>().AddRange(categories);
                context.SaveChanges();
            }

            //3 role
            using (var context = new EgreetingContext())
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole
                {
                    Name = "Admin",
                    EgreetingRole = new EgreetingRole
                    {
                        EgreetingRoleID = 1,
                        EgreetingRoleName = "Admin",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(role);
                role = new ApplicationRole
                {
                    Name = "User",
                    EgreetingRole = new EgreetingRole
                    {
                        EgreetingRoleID = 2,
                        EgreetingRoleName = "User",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(role);
                role = new ApplicationRole
                {
                    Name = "Subcriber",
                    EgreetingRole = new EgreetingRole
                    {
                        EgreetingRoleID = 3,
                        EgreetingRoleName = "Subcriber",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(role);
            }

            //20 user
            using (var context = new EgreetingContext())
            {
                var image = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Admin/dist/img/avatar.png"));
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser
                {
                    UserName = "mrtienthinh@gmail.com",
                    Email = "mrtienthinh@gmail.com",
                    EgreetingUser = new EgreetingUser
                    {
                        EgreetingUserID = 1,
                        Email = "mrtienthinh@gmail.com",
                        FirstName = "Tien Thinh",
                        LastName = "Nguyen",
                        Avatar = image,
                        EgreetingUserSlug = generateSlug(),
                        BirthDay = DateTime.Now,
                        PaymentDueDate = DateTime.Now,
                        CreditCardNumber = "123456789012",
                        CreditCardCVG = "123",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(user, "123456aA@");
                manager.AddToRole(user.Id, "Admin");

                user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EgreetingUser = new EgreetingUser
                    {
                        EgreetingUserID = 2,
                        Email = "admin@gmail.com",
                        FirstName = "Tien Thinh",
                        LastName = "Nguyen",
                        Avatar = image,
                        EgreetingUserSlug = generateSlug(),
                        BirthDay = DateTime.Now,
                        PaymentDueDate = DateTime.Now,
                        CreditCardNumber = "123456789012",
                        CreditCardCVG = "123",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(user, "123456aA@");
                manager.AddToRole(user.Id, "Admin");

                user = new ApplicationUser
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                    EgreetingUser = new EgreetingUser
                    {
                        EgreetingUserID = 3,
                        Email = "user@gmail.com",
                        FirstName = "Tien Thinh",
                        LastName = "Nguyen",
                        Avatar = image,
                        EgreetingUserSlug = generateSlug(),
                        BirthDay = DateTime.Now,
                        PaymentDueDate = DateTime.Now,
                        CreditCardNumber = "123456789012",
                        CreditCardCVG = "123",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(user, "123456aA@");
                manager.AddToRole(user.Id, "User");

                user = new ApplicationUser
                {
                    UserName = "subcriber@gmail.com",
                    Email = "subcriber@gmail.com",
                    EgreetingUser = new EgreetingUser
                    {
                        EgreetingUserID = 4,
                        Email = "subcriber@gmail.com",
                        FirstName = "Tien Thinh",
                        LastName = "Nguyen",
                        Avatar = image,
                        EgreetingUserSlug = generateSlug(),
                        BirthDay = DateTime.Now,
                        PaymentDueDate = DateTime.Now,
                        CreditCardNumber = "123456789012",
                        CreditCardCVG = "123",
                        CreatedDate = faker.Date.Past(),
                    }
                };
                manager.Create(user, "123456aA@");
                manager.AddToRole(user.Id, "Subcriber");

                for (int i = 5; i <= 20; i++)
                {
                    user = new ApplicationUser
                    {
                        UserName = "subcriber"+i+"@gmail.com",
                        Email = "subcriber" + i + "@gmail.com",
                        EgreetingUser = new EgreetingUser
                        {
                            EgreetingUserID = 4,
                            Email = "subcriber" + i + "@gmail.com",
                            FirstName = "Tien Thinh "+i,
                            LastName = "Nguyen",
                            Avatar = image,
                            EgreetingUserSlug = generateSlug(),
                            BirthDay = DateTime.Now,
                            PaymentDueDate = DateTime.Now,
                            CreditCardNumber = "123456789012",
                            CreditCardCVG = "123",
                            CreatedDate = faker.Date.Past(),
                        }
                    };
                    manager.Create(user, "123456aA@");
                    manager.AddToRole(user.Id, "Subcriber");
                }
            }
  

            // 100 Ecard
            using (var context = new EgreetingContext())
            {
                var ecards = new List<Ecard>();
                for (int i = 1; i <= 10; i++)
                {
                    var categoriesID = new List<int> { faker.Random.Number(1, 20), faker.Random.Number(1, 20) };
                    var ecard = new Ecard {
                        EcardID = i,
                        EcardName = faker.Commerce.ProductName(),
                        EcardSlug = faker.Lorem.Slug() + i,
                        EcardType = 4,
                        EcardUrl = "Ecard_" + faker.Random.Number(1,10)+".mp4",
                        ThumbnailUrl = "Thumbnail_" + faker.Random.Number(24, 39)+ ".png",
                        Price = faker.Random.Double() * 100,
                        Categories = context.Set<Category>().Where(x => categoriesID.Contains(x.CategoryID)).ToList(),
                        EgreetingUser = context.Set<EgreetingUser>().Where(x => x.EgreetingUserID == 1).FirstOrDefault(),
                        CreatedDate = faker.Date.Past(),
                    };
                    ecards.Add(ecard);
                }
                for (int i = 11; i <= 30; i++)
                {
                    var categoriesID = new List<int> { faker.Random.Number(1, 20), faker.Random.Number(1, 20) };
                    var ecard = new Ecard
                    {
                        EcardID = i,
                        EcardName = faker.Commerce.ProductName(),
                        EcardSlug = faker.Lorem.Slug() + i,
                        EcardType = 2,
                        EcardUrl = "Ecard_" + faker.Random.Number(11, 23) + ".gif",
                        ThumbnailUrl = "Thumbnail_" + faker.Random.Number(24, 39) + ".png",
                        Price = faker.Random.Double() * 100,
                        Categories = context.Set<Category>().Where(x => categoriesID.Contains(x.CategoryID)).ToList(),
                        EgreetingUser = context.Set<EgreetingUser>().Where(x => x.EgreetingUserID == 1).FirstOrDefault(),
                        CreatedDate = faker.Date.Past(),
                    };
                    ecards.Add(ecard);
                }
                for (int i = 31; i <= 100; i++)
                {
                    var categoriesID = new List<int> { faker.Random.Number(1, 20), faker.Random.Number(1, 20) };
                    var ecard = new Ecard
                    {
                        EcardID = i,
                        EcardName = faker.Commerce.ProductName(),
                        EcardSlug = faker.Lorem.Slug() + i,
                        EcardType = 1,
                        EcardUrl = "Ecard_" + faker.Random.Number(24, 39) + ".png",
                        ThumbnailUrl = "Thumbnail_" + faker.Random.Number(24, 39) + ".png",
                        Price = faker.Random.Double() * 100,
                        Categories = context.Set<Category>().Where(x => categoriesID.Contains(x.CategoryID)).ToList(),
                        EgreetingUser = context.Set<EgreetingUser>().Where(x => x.EgreetingUserID == 1).FirstOrDefault(),
                        CreatedDate = faker.Date.Past(),
                    };
                    ecards.Add(ecard);
                }
                context.Set<Ecard>().AddRange(ecards);
                context.SaveChanges();
            }

            // 500 Feedback
            using (var context = new EgreetingContext())
            {
                var feedbacks = new List<Feedback>();
                for (int i = 1; i <= 500; i++)
                {
                    var ecardID = faker.Random.Number(1, 100);
                    var userID = faker.Random.Number(1, 4);
                    var feedback = new Feedback
                    {
                        FeedbackID = i,
                        Subject = faker.Lorem.Sentence(10),
                        Message = faker.Lorem.Sentences(4),
                        Ecard = context.Set<Ecard>().Where(x => x.EcardID == ecardID).FirstOrDefault(),
                        EgreetingUser = context.Set<EgreetingUser>().Where(x => x.EgreetingUserID == userID).FirstOrDefault(),
                        CreatedDate = faker.Date.Past(),
                    };
                    feedbacks.Add(feedback);
                }
                context.Set<Feedback>().AddRange(feedbacks);
                context.SaveChanges();
            }

            // 16 subcrieber
            using (var context = new EgreetingContext())
            {
                var subcribers = new List<Subcriber>();
                for (int i = 5; i <= 20; i++)
                {
                    var subcriber = new Subcriber
                    {
                        SubcriberID = i,
                        Email = "subcriber" + i + "@gmail.com",
                        EgreetingUser = context.Set<EgreetingUser>().Find(i),
                        CreatedDate = faker.Date.Past(),
                    };
                    subcribers.Add(subcriber);
                }
                context.Set<Subcriber>().AddRange(subcribers);
                context.SaveChanges();
            }

            // 100 Payment
            using (var context = new EgreetingContext())
            {
                var payments = new List<Payment>();
                for (int i = 1; i <= 100; i++)
                {
                    var userID = faker.Random.Number(5, 20);
                    var payment = new Payment
                    {
                        PaymentID = i,
                        Month = i % 12 == 0 ? 1 : i % 12,
                        Year = faker.Random.Number(2000, 2019),
                        PaymentStatus = faker.Random.Bool(),
                        EgreetingUser = context.Set<EgreetingUser>().Find(userID),
                        CreatedDate = faker.Date.Past(),
                    };
                    payments.Add(payment);
                }
                context.Set<Payment>().AddRange(payments);
                context.SaveChanges();
            }

            //200 Order
            using (var context = new EgreetingContext())
            {
                var orders = new List<Order>();
                for (int i = 1; i <= 200; i++)
                {
                    var ecardIDs = new List<int>() { faker.Random.Number(1, 100), faker.Random.Number(1, 100), faker.Random.Number(1, 100) };
                    var orderDetails = new List<OrderDetail>();
                    var sendStatusOder = true;
                    foreach (var item in ecardIDs)
                    {
                        bool sendStatus = faker.Random.Bool();
                        var orderDetail = new OrderDetail
                        {
                            SendStatus = sendStatus,
                            Ecard = context.Set<Ecard>().Find(item),
                            CreatedDate = faker.Date.Past(),
                        };
                        if (sendStatus)
                        {
                            orderDetail.SendTime = faker.Date.Soon();
                        }
                        orderDetails.Add(orderDetail);
                        sendStatusOder = sendStatusOder && sendStatus;
                    }
                    var userID = faker.Random.Number(5, 20);

                    var order = new Order
                    {
                        OrderID = i,
                        SenderName = faker.Lorem.Sentence(3),
                        RecipientEmail = "mrtienthinh@gmail.com",
                        SendSubject = faker.Lorem.Sentence(4),
                        SendMessage = faker.Lorem.Sentences(4),
                        ScheduleTime = faker.Date.Past(),
                        SendStatus = sendStatusOder,
                        TotalPrice = faker.Random.Double(),
                        User = context.Set<EgreetingUser>().Find(userID),
                        OrderDetails = orderDetails,
                        CreatedDate = faker.Date.Past(),
                    };
                    orders.Add(order);
                }
                context.Set<Order>().AddRange(orders);
                context.SaveChanges();
            }
            return View();
        }

        public string generateSlug()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}