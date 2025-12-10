using SMApp.Models.ReportModels;

namespace SMApp.Services.Contracts;

public interface ISalesOrderReportService
{
    Task<List<GroupedFieldPriceModel>> GetEmployeePricePerMonthData();

    Task<List<GroupedFieldQtyModel>> GetQtyPerProductCategory();
}
