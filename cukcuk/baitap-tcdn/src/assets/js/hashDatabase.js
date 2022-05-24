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
    filterObject:{
        FilterTypeId:2,
        FilterDebtId:2,
        FilterStatusId:2,
      },
      filterType:[
        {Id:2,Value:"Tất cả"},
        {Id:0,Value:"Tổ chức"},
        {Id:1,Value:"Cá nhân"},
      ],
      filterDebt:[
        {Id:2,Value:"Tất cả"},
        {Id:0,Value:"Còn nợ"},
        {Id:1,Value:"Hết nợ"},
      ],
      filterStatus:[
        {Id:2,Value:"Tất cả"},
        {Id:0,Value:"Đang sử dụng"},
        {Id:1,Value:"Ngưng sử dụng"},
      ],

      /**
       * Tài khoản nợ,có
       */
      accounts:[
          {Id:0,Code: 1111111,Value:"Toán"},
          {Id:1,Code: 2222222,Value:"Khánh"},
          {Id:2,Code: 3333333,Value:"Lộc"},
          {Id:3,Code: 4444444,Value:"Hải"},
          {Id:4,Code: 5555555,Value:"Thái"},
          {Id:5,Code: 6666666,Value:"Mạnh"},
      ],

      /**
       * Type Money
       */
       typeMoney:[
        {Id:0,Value:"VND"},
        {Id:1,Value:"USD"}
    ],
}
export default DB