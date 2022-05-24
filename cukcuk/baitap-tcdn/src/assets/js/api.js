const Api =  {
    addEmployee: "http://localhost:5158/api/v1/Employees",
    updateEmployee: "http://localhost:5158/api/v1/Employees",
    getEmployeeById: "http://localhost:5158/api/v1/Employees",
    getNewEmployeeCode: "http://localhost:5158/api/v1/Employees/NewEmployeeCode",
    deleteEmployee: "http://localhost:5158/api/v1/Employees",
    deleteMultiEmployee: "http://localhost:5158/api/v1/Employees/multiple",
    filterEmployee: "http://localhost:5158/api/v1/Employees/filter",
    exportEmployee: "http://localhost:5158/api/v1/Employees/export",

    filterVendor: "http://localhost:5158/api/v1/Vendors/filterAdvanced",
    deleteVendor: "http://localhost:5158/api/v1/Vendors",
    vendors:"http://localhost:5158/api/v1/Vendors",
    newCodeVendor:"http://localhost:5158/api/v1/Vendors/NewCode",
    deleteMultiVendor: "http://localhost:5158/api/v1/Vendors/multiple",

    payments:"http://localhost:5158/api/v1/Payments",
    deleteMultiPayment:"http://localhost:5158/api/v1/Payments/multiple",

    paymentDetailsByPaymentId:"http://localhost:5158/api/v1/Payments/PaymentDetails/getByPaymentId",

    getDepartment: "http://localhost:5158/api/v1/Departments",
    getVendorGroup:"http://localhost:5158/api/v1/VendorGroups",

    getColumnOption:"http://localhost:3003/table",

    
    provincies:"https://localhost:5001/api/v1/Provincies",
    districts:"https://localhost:5001/api/v1/Districts",
    ward:"https://localhost:5001/api/v1/Wards"
}
export default Api