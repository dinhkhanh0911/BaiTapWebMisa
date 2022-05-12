

const Validator = {

    required: function (value) {
        return value ? undefined : 'không được để trống'
    },
    email: function (value) {
        
        if(!value){
            return undefined
        }
        var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        return regex.test(value) ? undefined : "chưa đúng định dạng"
    },
    name: function(value){
        var regex = /^[a-zA-Z ]{2,}$/g
        return regex.test(removeAscent(value)) ? undefined : "chưa đúng định dạng"
    },
    min: function (min) {
        return function (value){
            return (value.length > min) ? undefined :`Trường dữ liệu phải chưa đủ ${min} ký tự`
        }

    },
    max: function (max) {
        return function (value){
            return (value.length > max) ? undefined :`Trường dữ liệu phải chưa đủ ${max} ký tự`
        }
    },
    removeAscent : function(str){
        if (str === null || str === undefined) return str;
         str = str.toLowerCase();
         str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
         str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
         str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
         str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
         str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
         str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
         str = str.replace(/đ/g, "d");
         return str;
     }
}
function test(){
    console.log("test")
}
export default Validator