using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Exceptions;
using MISA.WEB02.Core.Helpers;
using MISA.WEB02.Core.Interfaces;
using MISA.WEB02.Core.Resources;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Services
{
    public class VendorService:BaseService<Vendor>,IVendorService
    {
        #region Fields
        IVendorRepository _vendorRepository;
        #endregion

        #region Constructor
        public VendorService(IVendorRepository vendorRepository) : base(vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xuất file excel
        /// </summary>
        /// <param name="filterText">Điều kiện đầu vào</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi 1 trang</param>
        /// <returns>File excel dạng byte</returns>
        /// created by: Đinh Văn Khánh - MF1112

        //public byte[] ExportService()
        //{
        //    var data = _employeeRepository.Get();
        //    //var propertyInfo = data.ElementAt(0).GetType().GetProperties();
        //    //var employees = (List<Employee>)propertyInfo.GetValue(data, null);
        //    MemoryStream stream = new MemoryStream();
        //    using (ExcelPackage excelPackage = new ExcelPackage(stream))
        //    {
        //        // Tạo title cho file Excel
        //        excelPackage.Workbook.Properties.Title = "Danh sách nhân viên";
        //        //thêm 1 sheet để làm việc với tệp excel
        //        excelPackage.Workbook.Worksheets.Add("Danh sách nhân viên");
        //        ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[0];
        //        //TIÊU đề của sheet
        //        workSheet.Cells["A1:K1"].Merge = true;//nối cottj
        //        workSheet.Cells["A1:K1"].Value = "DANH SÁCH NHÂN VIÊN";//title của sheet
        //        workSheet.Cells["A1:K1"].Style.Font.Size = 18;//font size
        //        workSheet.Cells["A2:K2"].Merge = true;//nối cột
        //        //header cho bảng
        //        workSheet.Cells["A3"].Value = "STT".ToUpper();
        //        workSheet.Cells["B3"].Value = "Mã nhân viên".ToUpper();
        //        workSheet.Cells["C3"].Value = "Tên nhân viên".ToUpper();
        //        workSheet.Cells["D3"].Value = "Giới tính".ToUpper();
        //        workSheet.Cells["E3"].Value = "Ngày sinh".ToUpper();
        //        workSheet.Cells["F3"].Value = "Chức danh".ToUpper();
        //        workSheet.Cells["G3"].Value = "Tên đơn vị".ToUpper();
        //        workSheet.Cells["H3"].Value = "Số tài khoản".ToUpper();
        //        workSheet.Cells["I3"].Value = "Tên ngân hàng".ToUpper();
        //        workSheet.Cells["J3"].Value = "Trạng thái".ToUpper();
        //        workSheet.Cells["K3"].Value = "Chi nhánh".ToUpper();


        //        int count = 0;//stt
        //        int index = 3;//vị trí bắt đầu của dữ liệu
        //        //thêm các sheet chi tiết
        //        foreach (var item in data)
        //        {
        //            count++;
        //            index++;
        //            //stt
        //            workSheet.Cells[index, 1].Value = count;
        //            //mã Nhân viên
        //            workSheet.Cells[index, 2].Value = item.EmployeeCode == null ? "" : item.EmployeeCode.Trim();
        //            //tên nhân viên
        //            workSheet.Cells[index, 3].Value = item.FullName == null ? "" : item.FullName.Trim();
        //            //giới tính
        //            workSheet.Cells[index, 4].Value = item.GenderName == null ? "" : item.GenderName.Trim();
        //            //ngày sinh
        //            workSheet.Cells[index, 5].Value = item.DateOfBirth == null ? "" : ((DateTime)item.DateOfBirth).ToString("dd/MM/yyyy");
        //            //chức danh
        //            workSheet.Cells[index, 6].Value = item.PositionName == null ? "" : item.PositionName.Trim();
        //            //tên đơn vị
        //            workSheet.Cells[index, 7].Value = item.DepartmentName == null ? "" : item.DepartmentName.Trim();
        //            //số tài khoản
        //            workSheet.Cells[index, 8].Value = item.BankAccount == null ? "" : item.BankAccount.Trim();
        //            //tên ngân hàng
        //            workSheet.Cells[index, 9].Value = item.BankName == null ? "" : item.BankName.Trim();
        //            //trạng thái
        //            workSheet.Cells[index, 10].Value = item.WorkStatusName == null ? "" : item.WorkStatusName.Trim();
        //            //chi nhánh
        //            workSheet.Cells[index, 11].Value = item.BankBranch == null ? "" : item.BankBranch.Trim();

        //        }
        //        //format file
        //        workSheet.Cells[$"A1:K{index}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //        workSheet.Cells[$"A1:K{index}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //        workSheet.Cells[$"A1:K{index}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //        workSheet.Cells[$"A1:K{index}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //        //fomat dữ liệu
        //        workSheet.Cells[$"A1:K{index}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;//căn trái
        //        workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//  căn giữa
        //        //set căn giữa hàng cho các cột
        //        workSheet.Cells[$"A1:K{index}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//căn giữa dòng
        //        //thêm dữ liệu các cột
        //        workSheet.Cells["A3:K3"].Style.Font.Bold = true;//In đậm
        //        workSheet.Cells["A3:K3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        workSheet.Cells["A3:K3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#BBB"));//background-color
        //        workSheet.Row(3).Height = 20;//độ rộng của cột header
        //        workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;//căn giữa chiều dọc tiêu dề
        //        workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//căn giữa chiều ngang tiêu đề
        //        //tự động dãn cột
        //        workSheet.Cells.AutoFitColumns();



        //        excelPackage.Save();
        //        var file = excelPackage.GetAsByteArray();
        //        excelPackage.Dispose();
        //        return file;
        //    }
        //}

        /// <summary>
        /// Tìm kiếm nâng cao nhà cung cấp theo điều kiện
        /// </summary>
        /// <param name="filterText">Điều kiện tìm kiếm</param>
        /// <param name="vendorType">Loại nhà cung cấp</param>
        /// <param name="vendorGroupId">Mã nhà cung cấp</param>
        /// <param name="isOwed">Tình trạng ghi nợ</param>
        /// <param name="isUsed">Trạng thái sử dụng</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <returns>
        /// Danh sách nhà cung cấp 
        /// Tổng số bản ghi
        /// Số lượng trang
        /// </returns>
        public Object FilterService(string filterText, int? vendorType, bool? isOwed,
            bool? isUsed, int currentPage, int pageSize)
        {
            var result = this._vendorRepository.Filter(filterText,vendorType, isOwed,isUsed, currentPage, pageSize);
            var jObj = JObject.Parse(result.ToString());
            jObj.Capitalize();
            dynamic resultFilter = JObject.Parse(jObj.ToString());
            return resultFilter;
        }

        public override int DeleteService(Guid entityId)
        {
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //kiểm tra bản ghi đã tồn tại
            var vendorData = _vendorRepository.GetById(entityId);
            if (vendorData == null)
            {
                
                errorMsg.Add($"VendorEmpty", $"Nhà cung cấp {vendorData.VendorName} không tồn tại");
            }
            else if (_vendorRepository.checkExitsPayment(entityId))
            {
                var style = "style=" + "\"font-weight: 700;\" ";
                errorMsg.Add($"PaymentExit", $"Nhà cung cấp {vendorData.VendorName} đã có phát sinh. Bạn phải xóa các phát sinh liên quan trước khi xóa nhà cung cấp.");
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions($"{Resource.VN_EntityNotFound}", errorMsg);
            }
            
            else
            {
                var result = _vendorRepository.Delete(entityId);
                return result;
            }
            return 1;
        }


        public override object MultiDelete(List<Guid> listId)
        {
            //validate dữ liệu
            Dictionary<string, string> deleteMsg = new Dictionary<string, string>();
            int success = 0;
            int failed = 0;
            int totalRecord = listId.Count();
            //kiểm tra bản ghi đã tồn tại
            foreach (var entityId in listId)
            {
                var vendorData = _vendorRepository.GetById(entityId);
                
                if (vendorData == null)
                {
                    deleteMsg.Add($"{entityId}", $"{Resource.VN_VendorNotFound}");
                    failed++;
                }
                else if (_vendorRepository.checkExitsPayment(entityId))
                {
                    deleteMsg.Add($"{entityId}", $"{Resource.VN_VendorHasPayment}");
                    failed++;
                }
                else
                {
                    success++;
                    _vendorRepository.Delete(entityId);

                }
            }
            return new { TotalRecord = totalRecord, Success=success,Failed = failed,DeleteMsg = deleteMsg};
        }

        private async Task<Object> GetColumnTable()
        {
            string reservationList;
            using (var httpClient = new HttpClient())
            {
                var api = $"http://localhost:3003/table/Vendor";
                using (var response = await httpClient.GetAsync(api))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = apiResponse;
                }
            }

            var jObj = JObject.Parse(reservationList.ToString());
            jObj.Capitalize();
            return JObject.Parse(jObj.ToString());
        }
        public byte[] ExportService(string filterText, int? vendorType, Guid? vendorGroupId, bool? isOwed,
            bool? isUsed, int currentPage, int pageSize)
        {
            dynamic table = GetColumnTable().Result;
            dynamic columns = table.Columns;
            //var propertyInfo = data.ElementAt(0).GetType().GetProperties();
            //var employees = (List<Employee>)propertyInfo.GetValue(data, null);
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = $"Danh sach nhà cung cấp";
                //thêm 1 sheet để làm việc với tệp excel
                excelPackage.Workbook.Worksheets.Add($"Danh sach nhà cung cấp");
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[0];
                //TIÊU đề của sheet
                workSheet.Cells["A1:K1"].Merge = true;//nối cottj
                workSheet.Cells["A1:K1"].Value = $"Danh sach nhà cung cấp)".ToUpper();//title của sheet
                workSheet.Cells["A1:K1"].Style.Font.Size = 18;//font size
                workSheet.Cells["A2:K2"].Merge = true;//nối cột


                int columnsCount = columns.Count;
                dynamic resultObject = FilterService(filterText, vendorType, isOwed, isUsed, currentPage, pageSize);
                var vendors = (List<Vendor>)resultObject.List.ToObject<List<Vendor>>();
                int vendorsCount = vendors.Count;
                if (columnsCount > 0 && vendorsCount > 0)
                {
                    int index = 3;
                    int count = 1;
                    for (int i = 0; i < columnsCount; i++)
                    {
                        var IsShow = columns[i].IsShow.Value;
                        if (IsShow == "True")
                        {
                            var columnType = columns[i].ColumnType.Value;
                            if (columnType == "date")
                            {
                                workSheet.Column(count).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                            else if (columnType == "number")
                            {
                                workSheet.Column(count).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            else
                            {
                                workSheet.Column(count).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            workSheet.Cells[index, count++].Value = columns[i].ColumnName.Value;
                        }
                    }
                    index++;

                    for (int i = 0; i < vendorsCount; i++)
                    {
                        count = 1;
                        for (int j = 0; j < columnsCount; j++)
                        {
                            var IsShow = columns[j].IsShow.Value;
                            if (IsShow == "True")
                            {
                                var columnField = columns[j].ColumnField.Value;
                                var vendor = vendors[i].GetType().GetProperty(columnField);
                                var columnType = columns[j].ColumnType.Value;
                                if (vendor != null)
                                {
                                    //var prop = payments[i].GetType().GetProperty("PaymentDate");
                                    if (columnType == "date")
                                    {
                                        var columnValue = vendor.GetValue(vendors[i]);
                                        workSheet.Cells[index, count++].Value = columnValue == null ? "" : ((DateTime)columnValue).ToString("dd/MM/yyyy");

                                    }
                                    else
                                    {
                                        var columnValue = vendor.GetValue(vendors[i]);
                                        workSheet.Cells[index, count++].Value = columnValue;
                                    }

                                }
                                else count++;
                            }


                        }
                        index++;
                    }
                    //format file
                    workSheet.Cells[1, 1, vendorsCount + 3, count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, 1, vendorsCount + 3, count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, 1, vendorsCount + 3, count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, 1, vendorsCount + 3, count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    //fomat dữ liệu

                    workSheet.Cells[3, 1, 3, count].Style.Font.Bold = true;//In đậm
                    workSheet.Cells[3, 1, 3, count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[3, 1, 3, count].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#BBB"));//background-color
                    workSheet.Row(3).Height = 20;//độ rộng của cột header
                    workSheet.Cells[1, 1, vendorsCount + 3, count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//căn giữa chiều dọc tiêu dề
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells.AutoFitColumns();
                }




                excelPackage.Save();
                var file = excelPackage.GetAsByteArray();
                excelPackage.Dispose();
                return file;
            }
        }
        #endregion
    }
}
