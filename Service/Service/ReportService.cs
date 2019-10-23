using Model.Persistence;
using Model.ViewModels.Home;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Service
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _dbContext;
        public ReportService(AppDbContext dbContext) => _dbContext = dbContext;
        public IEnumerable<HomeVM> CountAll()
        {
            try
            {
                int employees = _dbContext.Employes.Count(x => x.State ==Model.Enums.Shared.State.active);
                int suppliers = _dbContext.Suppliers.Count(x => x.State == Model.Enums.Shared.State.active); ;
                int products = _dbContext.Products.Count(x => x.State == Model.Enums.Shared.State.active); ;
                int clients = _dbContext.Clients.Count(x => x.State == Model.Enums.Shared.State.active); ;
                int sales = _dbContext.Sales.Count(x => x.State == Model.Enums.Shared.State.active);
                decimal salesTotalAmmout = _dbContext.Sales.Sum(x => x.Total);

                var model = new List<HomeVM>
                {
                    new HomeVM { Name = "Empleados", Quantity = employees },
                    new HomeVM { Name = "Suplidores", Quantity = suppliers },
                    new HomeVM { Name = "Productos", Quantity = products },
                    new HomeVM { Name = "Clientes", Quantity = clients },
                    new HomeVM { Name = "Ventas" , Quantity = sales},
                    new HomeVM {Name = "Total Vendido", Quantity = salesTotalAmmout}

                };

                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }



    }
}
