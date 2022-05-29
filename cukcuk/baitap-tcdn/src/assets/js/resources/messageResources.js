const popupMsg = {
    empNotSelectedMsg: "Bạn chưa chọn nhân viên để xóa",
    empManyNotSelectedMsg: "Chưa chọn hàng loạt nhân viên để xóa",
    confirmChangeDataMsg: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
    confirmMultipleDeleteMsg:"Bạn có muốn xóa những nhân viên đã chọn?",
    maintainMsg:"Tính năng đang phát triển",

    //func
    confirmDeleteEmpMsg(empCode){
        return `Bạn có thực sự muốn xóa nhân viên <${empCode}> không?`
    },
    deleteSuccessEmpMany(empCount){
        return `Đã xóa thành công ${empCount} nhân viên`
    },

    //payment
    paymentDetailNotFound:"Bạn phải nhập chứng từ chi tiết."
    
}

const toastMsg = {
    updateSuccessMsg: "Sửa dữ liệu thành công",
    addSuccessMsg: "Thêm mới dữ liệu thành công",
    deleteEmpSuccessMsg: "Đã xóa nhân viên thành công",
    deleteVendorSuccessMsg: "Đã xóa nhà cung cấp thành công",
    deletePaymentSuccessMsg: "Đã xóa phiếu thu,chi thành công",
    
}
const validateMsg = {
    interger: 'không đúng dạng số nguyên',
    numeric: 'không đúng dạng số thực',
    required: 'không được để trống',
    email: 'không đúng định dạng',
    name: 'không đúng định dạng',
    min: function (min) {
        return ` phải lớn hơn ${min} ký tự`

    },
    max: function (max) {
        return ` phải nhỏ hơn ${max} ký tự`
    },
}
const errorMsg = {
    exceptionMessage:"Có lỗi xảy ra.Vui lòng liên hệ Misa để được hỗ trợ."
}

const typePopupName = {
    error :'error',
    warningConfirm:'warningConfirm',
    warningNotify:'warningNotify',
    success:'success',
    infor:'infor'
}
const typePopup = {
    error:{
        className:'error',
        closeBtn:'',
        noBtn:'',
        confirmBtn:'Đóng'
    },
    warningConfirm: {
        className:'warning',
        closeBtn:'Hủy',
        noBtn:'',
        confirmBtn:'Có'
    },
    warningNotify: {
        className:'warning warning-notify',
        closeBtn:'',
        noBtn:'',
        confirmBtn:'Đồng ý'
    },
    success:{
        className:'success',
        closeBtn:'',
        noBtn:'',
        confirmBtn:'Đóng'
    },
    info:{
        className:'infor',
        closeBtn:'Hủy',
        noBtn:'Không',
        confirmBtn:'Có'
    },
}
export default {
    popupMsg, toastMsg , errorMsg, typePopup , typePopupName,validateMsg
}