<template>
    <div class="body-right" >
        <div class="body__header">
            <div class="body__header-title">
                <h1>Danh sách nhân viên</h1>
            </div>
            <div class="body__header-button">
                <button class="m-btn" id="add-btn" @click="isShow = !isShow">
                    <i class="fa fa-user-plus" aria-hidden="true"></i>
                    <span>Thêm nhân viên</span>
                </button>
            </div>
            
        </div>
        <div class="body__row-2">
            <div class="body__select-container">
                <div class="m-input-container m-search-container">
                    <div class="m-icon-search">
                        <i class="fa fa-search" aria-hidden="true"></i>
                    </div>
                    <div class="m-input">
                        <input rules="required" name="IdentityNumber" class="input-input" type="text" placeholder="Tìm kiếm"
                        >
                    </div>
                    <button class="m-time disabled">
                        <i class="fa fa-times" aria-hidden="true"></i>
                    </button>
                </div>
                <!-- <label for="">Số CMTND/CCCD(<span>*</span>)</label>
                <div class="m-input-container">
                    <div class="m-input">
                        <input rules="required" name="IdentityNumber" class="input-input" type="text" placeholder="Số CMTND/CCCD"
                        v-model="employee.IdentityNumber">
                    </div>
                    <button class="m-time disabled">
                        <i class="fa fa-times" aria-hidden="true"></i>
                    </button>
                </div> -->
                <Combobox 
                    :placeholder="'Đang tải dữ liệu...'" 
                    :id="'DepartmentId'" 
                    :name="'DepartmentName'" 
                    :api="'http://amis.manhnv.net/api/v1/Departments'" 
                    v-model="department.DepartmentId" 
                    :modelValue="department.DepartmentId"
                />
                <!-- <div class="custom-select" style="width:200px;">
                    <select>
                        <option value="0">Chọn phòng ban</option>
                        <option value="1">Audi</option>
                        <option value="2">BMW</option>
                        <option value="3">Citroen</option>
                        <option value="4">Ford</option>
                        <option value="5">Honda</option>
                    </select>
                </div> -->
            </div>
            <div class="btn-container-right">
                <button class="m-icon-btn" @click="confirmDelete()">
                    <i class="fa fa-trash" aria-hidden="true"></i>
                </button>
                <button class="m-icon-btn" @click="reload()">
                    <img src="../assets/icon/refresh.png" alt="">
                </button>
            </div>
        </div>
        <TableStaffComponent @showModal="showModalEmployees" @selected ="updateEmployeeDelete" ref="employeesTable"/>
        <div class="m-pagination">
            <div class="m-pagination__view">
                <span>
                    Hiển thị 01-20/123 lao động
                </span>
            </div>
            <div class="m-pagination__select">
                <button class="m-icon-btn m-page-btn">
                    <img src="../assets/icon/btn-firstpage.svg" alt="" >
                </button>
                <button class="m-icon-btn m-page-btn">
                    <img src="../assets/icon/btn-prev-page.svg" alt="">
                </button>
                <button class="m-icon-btn m-number-btn selected">
                    1
                </button>
                <button class="m-icon-btn m-number-btn">
                    2
                </button>
                <button class="m-icon-btn m-number-btn">
                    3
                </button>
                <button class="m-icon-btn m-number-btn">
                    4
                </button>
                <button class="m-icon-btn m-page-btn">
                    <img src="../assets/icon/btn-next-page.svg" alt="" >
                </button>
                <button class="m-icon-btn m-page-btn">
                    <img src="../assets/icon/btn-lastpage.svg" alt="">
                </button>
            </div>
            <div class="m-pagination__view">
                <span>
                    20 lao động/trang
                </span>
            </div>
        </div>
        <div id="overlay" class="overlay" v-show="isShow" ></div>
        <ModalEmployeeComponent v-if="isShow" @closeModal="closeModal"/>
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

import TableStaffComponent from './TableStaffComponent.vue'
import ModalEmployeeComponent from './ModalEmployeeComponent.vue'
import {mapMutations,mapGetters} from 'vuex'
import axios from 'axios'
import Popup from './Popup.vue'
import Combobox from './Combobox.vue'
export default {
    data() {
        return{
            isShow : false,
            employee:null,
            dataDeleted:[

            ],
            popup:{
                isShow :false,
                isConfirm :false,
                body:{

                }
            },
            reloadTable:false,
            department:{
                DepartmentId:undefined,
                DepartmentName:undefined
            }
        }
        
    },
    created() {
        window.addEventListener('keydown',this.handleKeyUp)
    },
    beforeDestroy() {
        window.removeEventListener('keydown',this.handleKeyUp)
    },
    computed :{
        ...mapGetters(['toast'])

    },
    components:{
        TableStaffComponent,
        ModalEmployeeComponent,
        Popup,
        Combobox
    },
    methods:{
        ...mapMutations(['setToast']),
        closeModal: function(value) {
            this.isShow = value
            console.log(this.isShow)
        },
        updateEmployeeDelete: function(EmployeesDeleleted){
            this.dataDeleted = EmployeesDeleleted
        },
        confirmDelete(){
            if(this.dataDeleted.length > 0){
                this.setPopup({
                    title:"Xác nhận xóa",
                    name:"deleteConfirm",
                    type:'error',
                    contentPrev:'Bạn có muốn xóa người dùng không?',
                    confirmButtonContent:'Xóa',
                    closeButtonContent:'Hủy'
                })
            }
        },
        handleDelete(){
            const employeesApi = 'http://amis.manhnv.net/api/v1/Employees'
                while(this.dataDeleted.length >0){
                    const employeeCode = this.dataDeleted.shift()
                    axios.delete(`${employeesApi}/${employeeCode}`)
                        .then(response =>{
                            if(response.status === 200){
                                console.log(response)
                                this.setToast([{
                                    message:`Xóa nhân viên thành công`,
                                    type:'success'
                                }])
                                this.reload()
                            }
                        })
                        .catch(e => {
                            console.log(e)
                            this.setToast([{
                                    message:`Xóa nhân viên không thành công`,
                                    type:'error'
                                }])
                        })
                }
        },
        reload(){
            console.log(this.$refs.employeesTable)
            this.$refs.employeesTable.loadData()                  
        },
        showModalEmployees(value,employee){
            this.isShow = value
            this.employee = employee
            
        },
        setPopup(body){
            this.popup.isShow = true
            this.popup.body = body
        },
        handleConfirmPopup: function(e){
            console.log(e.isConfirm)
            switch(e.name){
                case 'deleteConfirm':
                    if(e.isConfirm === true){
                        this.handleDelete()
                    }
                    this.popup.isShow = false
                    break;
                
            }
        },
        handleKeyUp(e){
            if(e.key === 'Delete'){
                this.confirmDelete()
            }
            if(e.keyCode === 112){
                e.preventDefault();
                console.log(e)
                this.isShow = true
            }
        }
    },
    watch:{
        
    }
}
</script>

<style>

</style>