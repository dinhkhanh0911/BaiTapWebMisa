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

        
        
        
        #endregion
    }
}
