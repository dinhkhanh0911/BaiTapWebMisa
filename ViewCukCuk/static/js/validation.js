

function Validator(formName){

    formRules = {}
    function getParent(element,parentSelector){

        while(element.parentElement){
            if(element.parentElement.matches(parentSelector)){
                
                return element.parentElement
            }
            element = element.parentElement
        }
        
    }

    const validateRules = {
        required: function (value) {
            return value ? undefined : 'Vui lòng nhập trường này'
        },
        email: function (value) {
            var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            return regex.test(value) ? undefined : "Email chưa đúng định dạng"
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
    }

    const formElement = document.querySelector(formName)
    if(formElement){
        var inputs = formElement.querySelectorAll('[name][rules]')
        for(var input of inputs){

            var rules = input.getAttribute('rules').split('|')
            
            for(var rule of rules){
                var ruleFun;
                var ishasAttr = rule.includes(':')
                var ruleInfo
                if(ishasAttr){
                    ruleInfo = rule.split(':')
                    rule = ruleInfo[0]
                    console.log(ruleInfo)
                }
                ruleFun = validateRules[rule]
                if(ishasAttr){
                    ruleFun = validateRules[rule](ruleInfo[1])
                }
                
                if(Array.isArray(formRules[input.name])){
                    formRules[input.name].push(ruleFun)
                }
                else {
                    formRules[input.name]= [ruleFun]
                }
            }
            input.onblur = handleValidate
            input.oninput = handleClearError
        }
        function handleValidate(event){
            var rules = formRules[event.target.name]
            var errorMessage;
            for(var rule of rules){
                errorMessage = rule(event.target.value)
                if(errorMessage) break;
            }
            if(errorMessage){
                var parentElement = getParent(event.target,'.form-group')
                parentElement.classList.add("input-error");
                console.log(errorMessage)
                return errorMessage
            }

        }
        function handleClearError(event){
            var parentElement = getParent(event.target,'.form-group')
            parentElement.classList.remove("input-error");

        }

        formElement.onsubmit = (event) => {
            event.preventDefault()
            
            var inputs = formElement.querySelectorAll('[name][rules]')
            var isValid = true
            for(var input of inputs){
                var ok = handleValidate({ target:input })
                if(ok){
                    isValid = false
                }
            }
            console.log(isValid)
            if(isValid){
                if(typeof this.onSubmit === 'function' ){
                    var enableInputs = formElement.querySelectorAll('[name]')
                    var dataform = Array.from(enableInputs).reduce(function(values,input){
                        // console.log(values)
                        // console.log(input.name)
                        // console.log(!input.value)
                        // var value = input.value
                        // if(!value) value = ""
                        values[input.name] = input.value
                        return values
                    },{})
                    
                    this.onSubmit(dataform)
                }
                else{
                    console.log("2")
                }
            }
            else console.log("not oke")
        }
    }
}