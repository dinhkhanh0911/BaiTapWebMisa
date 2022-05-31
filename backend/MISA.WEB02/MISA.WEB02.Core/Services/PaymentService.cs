using MISA.WEB02.Core.Entities;
using MISA.WEB02.Core.Exceptions;
using MISA.WEB02.Core.Helpers;
using MISA.WEB02.Core.Interfaces;
using MISA.WEB02.Core.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WEB02.Core.Services
{
    public class PaymentService:BaseService<Payment>,IPaymentService
    {
        #region Field
        IPaymentRepository _paymentRepository;
        #endregion
        #region Contructor
        public PaymentService(IPaymentRepository paymentRepository) : base(paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        #endregion
        #region Methods
        public Object FilterService(string filterText, int? paymentType, bool? isRecord,
           DateTime? startDate, DateTime? endDate, int currentPage, int pageSize)
        {
            return _paymentRepository.Filter(filterText, paymentType, isRecord, startDate, endDate, currentPage, pageSize);
        }
        public IEnumerable<PaymentDetail> GetPaymentDetailByPaymentIdService(Guid paymentId)
        {
            return _paymentRepository.GetPaymentDetailByPaymentId(paymentId);
        }

        public override int InsertService(Payment payment)
        {
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(payment);
            if (validateEmptyResult.Count() > 0)
            {
                throw new MISAExceptions($"{Resource.VN_DataNull}", errorMsg);
            }
            foreach (var item in validateEmptyResult)
            {
                errorMsg.Add(item.Key, item.Value);
            }
            //code đúng format
            var rx = new Regex(@"^[a-zA-Z]{3}-[0-9]{6}$");
            if (!rx.IsMatch(payment.PaymentCode.ToUpper()))
            {
                errorMsg.Add($"PaymentCode", $"{Resource.VN_CodeWrongFormat}");
            }
            //kiểm tra mã code trùng
            var data = _paymentRepository.FindByCode(payment.PaymentCode);
            //nếu có bản ghi trùng với mã hiện tại
            if (data != null)
            {

                errorMsg.Add($"PaymentCode", $"Mã <{payment.PaymentCode}> {Resource.VN_IsHas}");

            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions($"{Resource.VN_DataIllegal}", errorMsg);
            }
            var newGuid = Guid.NewGuid();
            payment.PaymentId = newGuid;
            List<PaymentDetail> PaymentDetails = (List<PaymentDetail>)payment.PaymentDetails;
            var length = PaymentDetails != null ? PaymentDetails.Count : 0;
            for(int i = 0;i<length;++i)
            {
                var paymentDetail = PaymentDetails[i];
                if (ValidatePaymentDetail(paymentDetail,i))
                {
                    var newGuidPaymentDetail = Guid.NewGuid();
                    paymentDetail.PaymentDetailId = newGuidPaymentDetail;
                    paymentDetail.PaymentId = newGuid;
                }
            }
            var ok = _paymentRepository.Insert(payment);
            //thêm mới database
            if (ok == 1) return 1;
            else throw new MISAExceptions($"{Resource.VN_MisaExceptionMsg}", errorMsg);
        }

        private bool ValidatePaymentDetail(PaymentDetail paymentDetail,int index)
        {
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            if (paymentDetail.DebitAccountId == null)
            {
                errorMsg.Add("DebitAccountIdError", $"Tài khoản nợ số {index + 1}  không được để trống");
                throw new MISAExceptions($"{Resource.VN_MisaExceptionMsg}", errorMsg);
            }
            return true;

        }

        /// <summary>
        /// thêm mới phiếu chi chi tiết
        /// </summary>
        /// <param name="paymentDetail"> Phiếu chi chi tiết </param>
        /// <returns></returns>
        /// Author: Đinh Văn Khánh (22/04/2022)
        public int InsertPaymentDetailService(PaymentDetail paymentDetail)
        {
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            

            var newGuid = Guid.NewGuid();
            paymentDetail.PaymentDetailId = newGuid;
            //thêm mới database
            return _paymentRepository.InsertPaymentDetail(paymentDetail);
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
                var vendorData = _paymentRepository.GetById(entityId);

                if (vendorData == null)
                {
                    deleteMsg.Add($"{entityId}", $"{Resource.VN_PaymentNotFound}");
                    failed++;
                }
                else
                {
                    success++;
                    _paymentRepository.Delete(entityId);

                }
            }
            return new { TotalRecord = totalRecord, Success = success, Failed = failed, DeleteMsg = deleteMsg };
        }
        private async Task<Object> GetColumnTable()
        {
            string reservationList;
            using (var httpClient = new HttpClient())
            {
                var api = $"http://localhost:3003/table/Payment";
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

        public byte[] ExportService(string filterText, int? paymentType, bool? isRecord,
           DateTime? startDate, DateTime? endDate, int currentPage, int pageSize)
        {
            dynamic table = GetColumnTable().Result;
            dynamic columns = table.Columns;

            MemoryStream stream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = $"Danh sách nhà cung cấp";
                //thêm 1 sheet để làm việc với tệp excel
                excelPackage.Workbook.Worksheets.Add($"Danh sách nhà cung cấp");
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[0];
                //TIÊU đề của sheet
                workSheet.Cells["A1:K1"].Merge = true;//nối cottj
                workSheet.Cells["A1:K1"].Value = $"Danh sách nhà cung cấp)".ToUpper();//title của sheet
                workSheet.Cells["A1:K1"].Style.Font.Size = 18;//font size
                workSheet.Cells["A2:K2"].Merge = true;//nối cột


                int columnsCount = columns.Count;
                var result = _paymentRepository.Search(filterText, paymentType, isRecord, startDate, endDate, currentPage, pageSize);

                //Chuyển dạng json về du liêu thường camelCase
                var jObj = JObject.Parse(result.ToString());
                jObj.Capitalize();
                dynamic resultObject = JObject.Parse(jObj.ToString());

                var payments = (List<Payment>)_paymentRepository.Get();
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
                                    else if (columnType == "number")
                                    {
                                        
                                        var columnValue = payment.GetValue(payments[i]);
                                        workSheet.Cells[index, count].Value = columnValue;
                                        workSheet.Cells[index, count++].Style.Numberformat.Format = "#,##0.0";

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
                    workSheet.Cells[3, 1, 3, count].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#BBB"));//background-color
                    workSheet.Row(3).Height = 20;//độ rộng của cột header
                    workSheet.Cells[1, 1, paymentsCount + 3, count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//căn giữa chiều dọc tiêu dề
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
