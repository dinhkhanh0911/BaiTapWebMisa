
const mutations = {
    setEmployee(state,employee){
        state.employee = employee
    },
    setToast(state,toast){
        state.toast = [...toast]
    },
    setPopup(state,body){
        state.popup = {
            isShow:true,
            isConfirm:false,
            body: body
        }
    }
}

export default mutations