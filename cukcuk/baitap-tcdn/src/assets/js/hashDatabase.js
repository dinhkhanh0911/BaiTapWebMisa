const DB = {

  /**
   * Bảng dữ liệu lý do tạo phiếu thu chi
   */
  filterTypePayments:[
      {Id:0,Value:"Trả tiền nhà cung cấp",Des:"Trả tiền cho"},
      {Id:1,Value:"Tạm ứng cho nhân viên",Des:"Tạm ứng cho"},
      {Id:2,Value:"Chi mua ngoài hóa đơn",Des:"Chi mua cho"},
      {Id:3,Value:"Trả lương nhân viên",Des:"Trả lương cho"},
      {Id:7,Value:"Chi khác",Des:"Chi tiền cho"},
      {Id:8,Value:"Tất cả"},
  ],
  /**
   * Bảng dữ liệu trạng thái ghi sổ
   */
  filterRecordPayments:[
      {Id:3,Value:"Đã ghi sổ"},
      {Id:1,Value:"Chưa ghi sổ"},
      {Id:2,Value:"Tất cả"},
  ],

  /**
   * Bảng dữ liệu tùy chọn ngày tháng thanh toán phiếu thu chi
   */
  filterOptionPayments:[
      {Id:5,Value:"Năm nay"},
      {Id:1,Value:"Tháng này"},
      {Id:3,Value:"Hôm nay"},
      {Id:4,Value:"Tùy chọn"},
  ],
  //Combobox phân trang
  valueOption: [
      { Id: 10, Value: "10 bản ghi trên 1 trang" },
      { Id: 20, Value: "20 bản ghi trên 1 trang" },
      { Id: 30, Value: "30 bản ghi trên 1 trang" },
      { Id: 50, Value: "50 bản ghi trên 1 trang" },
      { Id: 100, Value: "100 bản ghi trên 1 trang" },
  ],

    //Nhà cung cấp
    
  filterType:[
    {Id:3,Value:"Tất cả"},
    {Id:2,Value:"Tổ chức"},
    {Id:1,Value:"Cá nhân"},
  ],
  filterDebt:[
    {Id:2,Value:"Tất cả"},
    {Id:3,Value:"Còn nợ"},
    {Id:1,Value:"Hết nợ"},
  ],
  filterStatus:[
    {Id:2,Value:"Tất cả"},
    {Id:3,Value:"Đang sử dụng"},
    {Id:1,Value:"Ngưng sử dụng"},
  ],

  /**
   * Tài khoản nợ,có
   */
  accounts:[
      {Id:1,Code: 11112,Value:"Khánh"},
      {Id:2,Code: 111112,Value:"Lộc"},
      {Id:3,Code: 111123,Value:"Hải"},
      {Id:4,Code: 111122,Value:"Thái"},
      {Id:5,Code: 111135,Value:"Mạnh"},
      {Id:6,Code: 111132,Value:"Toán"},
  ],

  /**
   * Type Money
   */
    typeMoney:[
    {Id:2,Value:"VND"},
    {Id:1,Value:"USD"}
  ],
  country:[
    {
        Id:1,Value:'Việt Nam'
    }
  ] ,

  //Liên hệ
  contact:[
    {ContactId:1,ContactName:"Ông"},
    {ContactId:2,ContactName:"Bà"},
    {ContactId:3,ContactName:"Cha"},
    {ContactId:4,ContactName:"Mẹ"}
  ],
  //Điều khoản
  contract:[
    {ContractId:1,ContractCode:"ĐK1",ContractName:"Điều khoản 1"},
    {ContractId:2,ContractCode:"ĐK2",ContractName:"Điều khoản 2"},
    {ContractId:3,ContractCode:"ĐK3",ContractName:"Điều khoản 3"},
    {ContractId:4,ContractCode:"ĐK4",ContractName:"Điều khoản 4"},
  ],
  //Tài khoản thu
  debitPaymentAccount:[
    {DebitPaymentAccountId:1,BankAccount:"123456",BankName:"Phải thu của khách hàng"},
    {DebitPaymentAccountId:2,BankAccount:"1234562",BankName:"Thuê GTGT được khấu trừ"},
    {DebitPaymentAccountId:3,BankAccount:"1234563",BankName:"Đầu tư chứng khoán"},
    {DebitPaymentAccountId:4,BankAccount:"1234565",BankName:"Ngoại tệ"},
  ],
  //Tài khoản nhận
  debitReceiptAccount:[
    {DebitReceiptAccountId:1,BankAccount:"123456",BankName:"Phải thu của khách hàng"},
    {DebitReceiptAccountId:2,BankAccount:"1234562",BankName:"Thuê GTGT được khấu trừ"},
    {DebitReceiptAccountId:3,BankAccount:"1234563",BankName:"Đầu tư chứng khoán"},
    {DebitReceiptAccountId:4,BankAccount:"1234565",BankName:"Ngoại tệ"},
  ],
}
export default DB