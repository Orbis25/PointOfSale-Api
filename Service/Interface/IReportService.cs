using Model.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IReportService
    {
        IEnumerable<HomeVM> CountAll();
    }
}
