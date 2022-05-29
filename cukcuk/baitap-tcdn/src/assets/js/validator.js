

const Validator = {
    interger: function(value){
        var regex = /^(\d*)?$/;
        if(regex.test(value)) return true
        else return false
    },
    numeric: function(value){
        var regexDouble = /^(\d*)([,]\d{0,1})?$/;
        if(regexDouble.test(value)) return true
        else return false
    },
    required: function (value) {
        return !!value ? true : false
    },
    email: function (value) {
        
        if(!value){
            return true
        }
        var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        return regex.test(value) ? true : false
    },
    name: function(value){
        var regex = /^[a-zA-Z ]{2,}$/g
        return regex.test(removeAscent(value)) ? true : false
    },
    min: function (min) {
        return function (value){
            return (value.length > min) ? true :false
        }

    },
    max: function (max) {
        return function (value){
            return (value.length > max) ? true :false
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