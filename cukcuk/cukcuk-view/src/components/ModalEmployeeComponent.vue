<template>
    
    <div id="modalInfo" class="modal" tabindex="-1" role="dialog">
        
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <div class="modal-title-box">
                <h1 class="modal-title">THÔNG TIN NHÂN VIÊN</h1>
              </div>
              <button id="close-modal-btn" type="button" class="close m-icon-btn" 
              data-dismiss="modal" aria-label="Close" @click="handleCloseModal()">
                <img src="../assets/icon/x.svg" alt="" width="24" height="24">
              </button>
            </div>
            <div class="modal-body">
                <div class="modal-body-import">
                    <div class="import-container">
                        <div class="img-box">
                            <img src="../assets/img/default-avatar.jpg" alt="Tải ảnh">
                        </div>
                    </div>
                    <span>(Vui lòng chọn ảnh có định dạng .jpg,.png,.jpeg,.gif)</span>
                </div>
                <div class="modal-body__form">
                    <form class="form" id="addStaffForm">
                        <div class="form-container ">
                            <div class="modal-form__content">
                                <div class="grid">
                                    <div class="row modal-body__form-row">
                                        <div class="col c-12 modal-body__form-col">
                                            <div class="modal-col__infor">
                                                <h2 class="title">A. THÔNG TIN CHUNG:</h2>
                                                <div class="border-bottom"></div>
                                            </div>
                                            <div class="modal-col__content">
                                                <div class="grid">
                                                    <div class="row">
                                                        <div class="form-group col c-6">
                                                            <label for="">Mã nhân viên(<span>*</span>)</label>
                                                            <div class="m-input-container">
                                                                <div class="m-input">
                                                                    <input rules="required" name="EmployeeCode" class="input-input input-date" 
                                                                    type="text" placeholder="Đang tải dữ liệu..." v-model="employee.EmployeeCode"
                                                                    ref="employeeCode">
                                                                </div>
                                                                <button class="m-time disabled">
                                                                    <i class="fa fa-times" aria-hidden="true"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col c-6">
                                                            <label for="">Họ và tên(<span>*</span>)</label>
                                                            <div class="m-input-container">
                                                                <div class="m-input">
                                                                    <input rules="required|name" name="EmployeeName" class="input-input" type="text" 
                                                                    placeholder="Tên nhân viên" v-model="employee.EmployeeName">
                                                                </div>
                                                                <button class="m-time disabled">
                                                                    <i class="fa fa-times" aria-hidden="true"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        
                                                        <div class="form-group col c-6">
                                                            <label for="">Ngày sinh(<span>*</span>)</label>
                                                            <input class="input-text input-date" type="date" name="DateOfBirth" v-model="formatBrithDay">
                                                        </div>
                                                        <div class="form-group col c-6">
                                                            <label for="">Giới tính</label>
                                                            <Combobox 
                                                                :placeholder="'Giới tính'" 
                                                                :id="'Gender'" 
                                                                :modelValue="employee.Gender" 
                                                                :name="'GenderName'"
                                                                :default="genders" 
                                                                v-model="employee.Gender"/>
                                                        </div>
                                                        
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="form-group col c-6">
                                                            <label for="">Số CMTND/CCCD(<span>*</span>)</label>
                                                            <div class="m-input-container">
                                                                <div class="m-input">
                                                                    <input rules="required" name="IdentityNumber" class="input-input" type="text" placeholder="Số CMTND/CCCD"
                                                                    v-model="employee.IdentityNumber">
                                                                </div>
                                                                <button class="m-time disabled">
                                                                    <i class="fa fa-times" aria-hidden="true"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col c-6">
                                                            <label for="">Ngày cấp</label>
                                                            <input name="IdentityDate" class="input-text input-date" type="date" v-model="formatIdentityDate">
                                                        </div>
                                                        
                                                        
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col c-6">
                                                            <label for="">Nơi cấp</label>
                                                            <div class="m-input-container">
                                                                <div class="m-input">
                                                                    <input name="IdentityPlace"  class="input-input" type="text" placeholder="Nơi cấp" 
                                                                    v-model="employee.IdentityPlace">
                                                                </div>
                                                                <button class="m-time disabled">
                                                                    <i class="fa fa-times" aria-hidden="true"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col c-6">
                                                            <label for="">Email(<span>*</span>)</label>
                                                            <div class="m-input-container">
                                                                <div class="m-input">
                                                                    <input rules="required|email" name="Email" class="input-input" type="text" placeholder="Email"
                                                                    v-model="employee.Email">
                                                                </div>
                                                                <button class="m-time disabled">
                                                                    <i class="fa fa-times" aria-hidden="true"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col c-6">
                                                            <label for="">Số điện thoại</label>
                                                            <div class="m-input-container">
                                                                <div class="m-input">
                                                                    <input name="PhoneNumber" class="input-input" type="text" placeholder="Số điện thoại"
                                                                    v-model="employee.PhoneNumber">
                                                                </div>
                                                                <button class="m-time disabled">
                                                                    <i class="fa fa-times" aria-hidden="true"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col c-12 modal-body__form-col" style="margin-top:10px">
                                            <div class="modal-col__infor">
                                                <h2 class="title">B. THÔNG TIN CÔNG VIỆC:</h2>
                                                <div class="border-bottom"></div>
                                            </div>
                                            <div class="modal-col__content">
                                                <div class="grid">
                                                    
                                                    <div class="row">
                                                        <div class="form-group col c-6">
                                                            <label for="">Vị trí</label>
                                                            <div class="m-combobox">
                                                                <div class="combobox">
                                                                    <input
                                                                        class="m-input-combobox combobox-input"
                                                                        value="Vị trí"
                                                                        type="text"
                                                                        placeholder="Giới tính"
                                                                    />
                                                                    <span class="combobox-icon"
                                                                        ><i class="fa-solid fa-chevron-down"></i
                                                                    ></span>
                                                                    <div class="item-box">
                                                                        <div class="item">Khánh</div>
                                                                        <div class="item">Khánh</div>
                                                                        <div class="item">Khánh</div>

                                                                    </div>
                                                                </div>
                                                                
                                                            </div>
                                                        </div>
                                                        <div class="form-group col c-6">
                                                            <label for="">Phòng ban</label>
                                                            <Combobox 
                                                                :placeholder="'Đang tải dữ liệu...'" 
                                                                :id="'DepartmentId'" :name="'DepartmentName'" 
                                                                :api="'http://amis.manhnv.net/api/v1/Departments'" 
                                                                v-model="employee.DepartmentId" 
                                                                :modelValue="employee.DepartmentId"/>
                                                        </div>
                                                        
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="form-group col c-6">
                                                            <label for="">Mã số thuế cá nhân</label>
                                                            <div class="m-input-container">
                                                                <div class="m-input">
                                                                    <input name="PersonalTaxCode" class="input-input" type="text" placeholder="Mã số thuế"
                                                                    v-model="employee.PersonalTaxCode">
                                                                </div>
                                                                <button class="m-time disabled">
                                                                    <i class="fa fa-times" aria-hidden="true"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col c-6">
                                                            <label for="">Mức lương cơ bản</label>
                                                            <input class="input-text input-date input-number" placeholder="Mức lương cơ bản">
                                                        </div>
                                                        
                                                        
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col c-6">
                                                            <label for="">Ngày giá nhập công ty</label>
                                                            <input class="input-text input-date" type="date">
                                                        </div>
                                                        <div class="form-group col c-6">
                                                            <label for="">Tình trạng công việc</label>
                                                            <div class="m-combobox">
                                                                <div class="combobox">
                                                                    <input
                                                                        class="m-input-combobox combobox-input"
                                                                        value="Vị trí"
                                                                        type="text"
                                                                        placeholder="Giới tính"
                                                                    />
                                                                    <span class="combobox-icon"
                                                                        ><i class="fa-solid fa-chevron-down"></i
                                                                    ></span>
                                                                    <div class="item-box">
                                                                        <div class="item">Khánh</div>
                                                                        <div class="item">Khánh</div>
                                                                        <div class="item">Khánh</div>

                                                                    </div>
                                                                </div>
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </form>
                    
                </div>
            </div>
            <div class="modal-footer">
                <button class="m-btn m-btn-default" @click="closeModal()">
                    <span>Hủy</span>
                </button>
                <button id="addStaffBtn" class="m-btn" @click="handleSubmitBtnClick()">
                    <i class="fa fa-floppy-o" aria-hidden="true"></i>
                    <span>Lưu và thêm mới</span>
                </button>
                
            </div>
          </div>
        </div>
        <Popup 
            :title="popup['body']['title']" 
            :name="popup['body']['name']"
            :type="popup['body']['type']"
            :contentPrev="popup['body']['contentPrev']"
            :contentStrong="popup['body']['contentStrong']"
            :contentLast="popup['body']['contentLast']"
            :closeButtonContent="popup['body']['closeButtonContent']"
            :confirmButtonContent="popup['body']['confirmButtonContent']"
            :isShow="popup.isShow" 
            @Confirm="handleConfirmPopup" 
        />
    </div>
</template>

<script>
import axios from 'axios'
import Validator from '../assets/js/validation'
import Combobox from './Combobox.vue'
import {mapMutations,mapGetters} from 'vuex'
import Popup from './Popup.vue'
export default {
    data(){
        return {
            employeesCode:'',
            genders:[
                { Gender:0,GenderName:"Nữ"},
                { Gender:1,GenderName:"Nam"},
                { Gender:2,GenderName:"Khác"},
            ],
            departments:[],
            popup:{
                isShow :false,
                isConfirm :false,
                body:{

                }
            },
        }
    },
    components:{
        Combobox,
        Popup
    },
    created(){

        //Sau khi khởi tao
        // Nếu dữ liệu nhân viên trống thì lấy dữ liệu nhân viên mới
        const newCodeEmployee = 'http://amis.manhnv.net/api/v1/Employees/NewEmployeeCode'
        if(this.employee.EmployeeCode == undefined){
            axios.get(newCodeEmployee)
            .then((response)=>{
                
                if(response.status === 200){
                    console.log(response.data)
                    this.employee.EmployeeCode = response.data
                }
            })
        }
            
        window.addEventListener('keydown', this.handleKeyUp);

    },
    beforeDestroy() {
      window.removeEventListener('keydown', this.handleKeyUpsdsd);
    },
    computed:{
        ...mapGetters(['employee','toast','popup']),
        
        formatBrithDay:{
            get(){
                var date = new Date(this.employee.DateOfBirth)
                var year = date.getFullYear()
                var month = date.getMonth()+1
                var day = date.getDate()
                if(year == 1970 && month == 1 && day == 1) return ''
                
                return day > 9 ? `${year}-0${month % 10}-${day}` : `${year}-0${month % 10}-0${day}`
            },
            set(date){
                this.employee.DateOfBirth = date
            }
        },
        formatIdentityDate:{
            get(){
                var date = new Date(this.employee.IdentityDate)
                var year = date.getFullYear()
                var month = date.getMonth()+1
                var day = date.getDate()
                if(year == 1970 && month == 1 && day == 1) return ''
                return day > 9 ? `${year}-0${month % 10}-${day}` : `${year}-0${month % 10}-0${day}`
            },
            set(date){
                this.employee.IdentityDate = date
            }
        },
        // convertGender(){
        //     switch(this.employee.Gender){
        //         case 0:
        //             return 'Nữ'
        //         case 1: 
        //             return 'Nam'
        //         default:
        //             return 'Khác'
        //     }
        // }
    },
    mounted(){

        var form = new Validator('#addStaffForm')
        form.onSubmit = function(dataForm){
            console.log(dataForm)
        }
        this.$refs.employeeCode.focus()
        
    },
    methods: {
        ...mapMutations(['setEmployee','setToast']),
        closeModal(){
            this.$emit('closeModal',false)
            this.setEmployee({})
        
        },
        handleSubmitBtnClick(){
            var form = document.querySelector('#addStaffForm')
            const employeesApi = 'http://amis.manhnv.net/api/v1/Employees'
            if(this.employee.EmployeeId != undefined){
                axios.put(`${employeesApi}/${this.employee.EmployeeId}`,this.employee)
                    .then(response => {
                        if(response.data == 1){

                            this.setToast([{
                                message:'Chỉnh sửa nhân viên thành công',
                                type:'success'
                            }])
                        }
                    })
                    .catch(e => {
                        console.log(e)
                    })
            }
            else{
                axios.post(`${employeesApi}`,this.employee)
                    .then(response => {
                        console.log(response)
                        if(response.data == 1){
                            this.setToast([{
                                message:'Thêm nhân viên thành công',
                                type:'success'
                            }])
                        }
                    })
                    .catch(e => {
                        console.log(e.response.data.userMsg)
                        this.setToast([{
                                message:`${e.response.data.userMsg}`,
                                type:'error'
                            }])
                    })
            }
        },
        handleCloseModal(){
            this.setPopup({
                    title:"Xác nhận đóng modal",
                    name:"createConfirm",
                    type:'error',
                    contentPrev:'Bạn có muốn tắt thêm người dùng không?',
                    confirmButtonContent:'Tắt',
                    closeButtonContent:'Hủy'
                })
        },
        setPopup(body){
            this.popup.isShow = true
            this.popup.body = body
        },
        handleConfirmPopup: function(e){
            console.log(e.isConfirm)
            switch(e.name){
                case 'createConfirm':
                    if(e.isConfirm === true){
                        this.closeModal()
                    }
                    this.popup.isShow = false
                    break;
                
            }
        },
        handleKeyUp(e){
            if(e.keyCode === 83 && e.ctrlKey){
                e.preventDefault();
                this.handleSubmitBtnClick()
            }
            if(e.key === 'Escape'){
                this.handleCloseModal()
            }
        }
        
    },  
}
</script>

<style>
.form-group .m-combobox{
    margin-top: 4px;
}
</style>