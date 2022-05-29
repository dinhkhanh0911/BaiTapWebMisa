using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Exceptions;
using MISA.WEB02.Core.Helpers;
using MISA.WEB02.Core.Interfaces.Base;
using MISA.WEB02.Core.Resources;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        #region Fields
        private IBaseRepository<T> _baseRespository;
        #endregion
        #region Constructor
        public BaseService(IBaseRepository<T> tiem)
        {
            this._baseRespository = tiem;
        }
        #endregion

        #region Methods
        /// <summary>
        /// xóa 1 bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        public virtual int DeleteService(Guid entityId)
        {
            var entityName = typeof(T).Name;
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //kiểm tra bản ghi đã tồn tại
            var employeeData = _baseRespository.GetById(entityId);
            if (employeeData == null)
            {
                errorMsg.Add($"{entityName}Empty", $"{entityName} không tồn tại");
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions($"{Resource.VN_EntityNotFound}", errorMsg);
            }
            else
            {
                var result = _baseRespository.Delete(entityId);
                return result;
            }
        }
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// </returns>
        /// Author: Đinh Văn Khánh - MF1112 (13/04/2022)
        public virtual int InsertService(T entity)
        {
            var entityName = typeof(T).Name;
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(entity);
            foreach (var item in validateEmptyResult)
            {
                errorMsg.Add(item.Key, item.Value);
            }
            //lấy code của entity truyền vào
            var code = typeof(T).GetProperty($"{entityName}Code").GetValue(entity).ToString();
            //Lấy id của entity truyền vào
            var id = typeof(T).GetProperty($"{entityName}Id");
            //code đúng format
            var rx = new Regex(@"^[a-zA-Z]{3}-[0-9]{6}$");
            if (!rx.IsMatch(code.ToUpper()))
            {
                errorMsg.Add($"{entityName}Code", $"{Resource.VN_CodeWrongFormat}");
            }
            //kiểm tra mã code trùng
            var data = _baseRespository.FindByCode(code);
            //nếu có bản ghi trùng với mã hiện tại
            if (data != null)
            {
                
                errorMsg.Add($"{entityName}Code", $"Mã <{code}> {Resource.VN_IsHas}");

            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions($"{Resource.VN_DataIllegal}", errorMsg);
            }
            var newGuid = Guid.NewGuid();
            typeof(T).GetProperty($"{entityName}Id").SetValue(entity,newGuid);
            //thêm mới database
            return _baseRespository.Insert(entity);


        }
        /// <summary>
        /// sửa 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Đinh Văn Khánh - MF1112 - MF1095 (14/04/2022)
        public virtual int UpdateService(T entity, Guid id)
        {
            var entityName = typeof(T).Name;
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(entity);
            foreach (var item in validateEmptyResult)
            {
                errorMsg.Add(item.Key, item.Value);
            }
            //lấy code của entity truyền vào
            var code = typeof(T).GetProperty($"{entityName}Code").GetValue(entity).ToString();
            //code đúng format
            var rx = new Regex(@"^[a-zA-Z]{3}-[0-9]{6}$");
            if (!rx.IsMatch(code.ToUpper()))
            {
                errorMsg.Add($"{entityName}Code", $"{Resource.VN_CodeWrongFormat}");
            }
            //lấy Employee cũ trong data base
            var data = _baseRespository.GetById(id);
            //nếu không tìm thấy bản ghi / bản ghi đã bị xóa trước khi sửa 
            if (data == null)
            {
                errorMsg.Add($"{entityName}NotFound", $"{Resource.VN_EntityNotFound}");
                //throw new MISAExceptions($"{Resource.VN_DataIllegal}", errorMsg);
            }
            var newCode = typeof(T).GetProperty($"{entityName}Code").GetValue(data).ToString();
            //kiểm tra mã code trùng
            if (String.Compare(newCode, code, true) < 0 || String.Compare(newCode, code, true) > 0)
            {
                data = _baseRespository.FindByCode(code);
                //nếu có bản ghi trùng với mã hiện tại
                if (data != null)
                {

                    errorMsg.Add($"{entityName}Code", $"Mã <{code}> {Resource.VN_IsHas}");

                }
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions($"{Resource.VN_DataIllegal}", errorMsg);
            }
            //set lại data
            //lấy các prop trong entity
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                //lấy giá trị của entity đó
                var entityValue = prop.GetValue(entity);
                //lấy giá trị của data
                var dataValue = prop.GetValue(entity);
                if (entityValue == null)
                {
                    prop.SetValue(entity, dataValue);
                }
            }
            return _baseRespository.Update(entity);
        }
        /// <summary>
        /// xóa nhiều bản ghi trong bảng
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public virtual object MultiDelete(List<Guid> listId)
        {
            var entityName = typeof(T).Name;
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            foreach (var id in listId)
            {
                var entity = _baseRespository.GetById(id);
                if(entity == null)
                {
                    errorMsg.Add($"{entityName}Id", $"{Resource.VN_EntityHas}");
                    throw new MISAExceptions($"{Resource.VN_Error}",errorMsg);
                }
            }
            var result = _baseRespository.MultiDelete(listId);
            return result;
        }
        /// <summary>
        /// kiểm tra các trường required bị trống
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Dictionary<string, string> ValidateEmpty(T entity)
        {
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //kiểm tra các trường required có bị null không
            //lấy các prop trong entity
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                //kiểm tra xem có được định nghĩa là required không
                var isRequired = prop.IsDefined(typeof(MISARequired), false);
                if (isRequired)
                {
                    //lấy giá trị của prop đó
                    var propValue = prop.GetValue(entity);
                    //kiểm tra propValue null
                    if (propValue == null)
                    {
                        //lấy các thuộc tính được hiển thị hiển thị của prop
                        var displayName = (MISADisplay)Attribute.GetCustomAttribute(prop, typeof(MISADisplay));
                        errorMsg.Add($"{prop.Name}", $"{displayName.Name} không được để trống");

                    }
                    //kiểm tra là chuỗi rỗng
                    else if (string.IsNullOrEmpty(propValue.ToString()))
                    {
                        //lấy các thuộc tính được hiển thị hiển thị của prop
                        var displayName = (MISADisplay)Attribute.GetCustomAttribute(prop, typeof(MISADisplay));
                        errorMsg.Add($"{prop.Name}", $"{displayName.Name} không được để trống");

                    }
                }
            }
            return errorMsg;
        }

        public virtual IEnumerable<T> GetService()
        {
            var result = _baseRespository.Get();
            return result;
        }

        public virtual T GetByIdService(Guid id)
        {
            var result = _baseRespository.GetById(id);
            return result;
        }
        public virtual string GetNewCodeService()
        {
            var result = _baseRespository.GetNewCode();
            return result;
        }

        /// <summary>
        /// Tìm kiếm nhân viên theo điều kiện
        /// </summary>
        /// <param name="filterText">Điều kiện tìm kiếm</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <returns>
        /// Danh sách nhà cung cấp 
        /// Tổng số bản ghi
        /// Số lượng trang
        /// </returns>
        public Object FilterService(string filterText, int currentPage, int pageSize)
        {
            var result = _baseRespository.Filter(filterText, currentPage, pageSize);
            return result;
        }

        private async Task<Object> GetColumnTable()
        {
            var entityName = typeof(T).Name;
            string reservationList;
            using (var httpClient = new HttpClient())
            {
                var api = $"http://localhost:3003/table/{entityName}";
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
        public byte[] ExportService(string fileName)
        {
            dynamic table = GetColumnTable().Result;
            dynamic columns = table.Columns;
            //var propertyInfo = data.ElementAt(0).GetType().GetProperties();
            //var employees = (List<Employee>)propertyInfo.GetValue(data, null);
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = $"{fileName}";
                //thêm 1 sheet để làm việc với tệp excel
                excelPackage.Workbook.Worksheets.Add($"{fileName}");
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[0];
                //TIÊU đề của sheet
                workSheet.Cells["A1:K1"].Merge = true;//nối cottj
                workSheet.Cells["A1:K1"].Value = $"{fileName.ToUpper()}";//title của sheet
                workSheet.Cells["A1:K1"].Style.Font.Size = 18;//font size
                workSheet.Cells["A2:K2"].Merge = true;//nối cột


                int columnsCount = columns.Count;
                var payments = (List<T>)_baseRespository.Get();
                int paymentsCount = payments.Count;
                if (columnsCount > 0 && paymentsCount > 0)
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

                    for (int i = 0; i < paymentsCount; i++)
                    {
                        count = 1;
                        for (int j = 0; j < columnsCount; j++)
                        {
                            var IsShow = columns[j].IsShow.Value;
                            if (IsShow == "True")
                            {
                                var columnField = columns[j].ColumnField.Value;
                                var payment = payments[i].GetType().GetProperty(columnField);
                                var columnType = columns[j].ColumnType.Value;
                                if (payment != null)
                                {
                                    //var prop = payments[i].GetType().GetProperty("PaymentDate");
                                    if (columnType == "date")
                                    {
                                        var columnValue = payment.GetValue(payments[i]);
                                        workSheet.Cells[index, count++].Value = columnValue == null ? "" : ((DateTime)columnValue).ToString("dd/MM/yyyy");

                                    }
                                    else
                                    {
                                        var columnValue = payment.GetValue(payments[i]);
                                        workSheet.Cells[index, count++].Value = columnValue;
                                    }

                                }
                                else count++;
                            }


                        }
                        index++;
                    }
                    //format file
                    workSheet.Cells[1, 1, paymentsCount + 3, count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, 1, paymentsCount + 3, count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, 1, paymentsCount + 3, count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[1, 1, paymentsCount + 3, count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    //fomat dữ liệu

                    workSheet.Cells[3, 1, 3, count].Style.Font.Bold = true;//In đậm
                    workSheet.Cells[3, 1, 3, count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[3,1,3,count].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#BBB"));//background-color
                    workSheet.Row(3).Height = 20;//độ rộng của cột header
                    workSheet.Cells[1,1,paymentsCount + 3, count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//căn giữa chiều dọc tiêu dề
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells.AutoFitColumns();
                }




                excelPackage.Save();
                var file = excelPackage.GetAsByteArray();
                excelPackage.Dispose();
                return file;
            }
            return columns[0];
        }

        #endregion
    }
}
