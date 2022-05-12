<template>
    <div class="m-table-container">
        <div class="loading-data" v-show="isloading">
            <img src="../assets/img/1496.gif" alt="" style="margin:auto">
        </div>
        <table class="m-table">
            <thead id="tThead">
                <tr>
                    <th>
                        <label class="container">
                            <input type="checkbox" v-model="selectAll" >
                            <span class="checkmark"></span>
                        </label>
                    </th>
                    <th propname="STT" type="EmployeeId">STT</th>
                    <th propname="EmployeeCode">
                        <span>Mã nhân viên</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="EmployeeName">
                        <span>Họ và tên</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="GenderName">
                        <span>Giới tính</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="DateOfBirth" type="date">
                        <span>Ngày sinh</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="PhoneNumber">
                        <span>Điện thoại</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="Email">
                        <span>Email</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="EmployeePosition">
                        <span>Chức vụ</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="departmentName">
                        <span>Phòng ban</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="wage">
                        <span>Mức lương cơ bản</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                    <th propname="status">
                        <span>Tình trạng công việc</span>
                        <button class="m-sort-btn">
                            <i class="fa fa-sort" aria-hidden="true"></i>
                        </button>
                    </th>
                </tr>
            </thead>
            <tbody id="tbody">
                
                
                
                <tr v-for="(employee,index) in employees" :key="index" v-show="!isloading" @dblclick="handleDbClickEmployees(index)">
                    <td>
                        <label class="container">
                            <input type="checkbox" v-model="selected" :value="employee.EmployeeId">
                            <span class="checkmark"></span>
                        </label>
                    </td>
                    <td >{{index+1}}</td>
                    <td >{{employee.EmployeeCode}}</td>
                    <td>{{employee.EmployeeName}}</td>
                    <td>{{employee.GenderName}}</td>
                    <td>{{convert(employee.DateOfBirth)}}</td>
                    <td>{{employee.PhoneNumber}}</td>
                    <td>{{employee.Email}}</td>
                    <td>{{employee.EmployeePosition}}</td>
                    <td>{{employee.DepartmentName}}</td>
                    <td>3.000.000</td>
                    <td>Mới gia nhập</td>
                    
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import axios from 'axios'
import {mapMutations,mapGetters} from 'vuex'
export default {
    data(){
        return{
            employee: null,
            employees :[],
            selected: [],
            deleteSelect:[],
            isShow : true,
            isloading:true,
        }
    },
    props:{
        reload:{
            type:Boolean,
            default:false
        }
    },
    watch:{
        selected: function(){
            this.$emit('selected',this.selected)
        },
        reload: function(){
            if(this.reload == true){
                console.log("123")
                this.loadData()
                this.reload == false
            }
        }
    },
    computed:{
        
    },
    mounted() {
        
    },
    created() {
        // // Gọi API lấy dữ liệu
        // const employeesApi = 'http://amis.manhnv.net/api/v1/Employees'
        // axios.get(employeesApi)
        //     .then((response)=>{
        //         this.employees = response.data
        //         setTimeout(() => {
        //             this.isloading = false
                    
        //         },500)
        //     })
        this.loadData()
    },
    computed :{
        // Chỉnh sửa select 
        selectAll: {
            get: function () {
                return this.employees ? this.selected.length == this.employees.length : false;
            },
            set: function (value) {
                var selected = [];
                console.log(value)
                if (value) {
                    this.employees.forEach(function (employee) {
                        selected.push(employee.EmployeeId);
                    });
                }

                this.selected = selected;
            }
        }
        
    },
    methods: {
        ...mapMutations(['setEmployee']),
        // Chỉnh lại định dạng ngày tháng
        convert(date){
            date = new Date(date)
            var year = date.getFullYear()
            var month = date.getMonth()+1
            var day = date.getDate()
            if(year == 1970 && month == 1 && day == 1) return ''
            return `${day}-${month}-${year}`
        },
        showEmployees(value){
            return this.deleteSelect.find(employee => employee.EmployeeId == value)
        },
        handleDbClickEmployees(index){
            this.employee = this.employees[index]
            this.setEmployee(this.employee)
            this.$emit('showModal',true,this.employee);
        },
        loadData(){
            this.isloading = true
            // Gọi API lấy dữ liệu
            const employeesApi = 'http://amis.manhnv.net/api/v1/Employees'
            axios.get(employeesApi)
                .then((response)=>{
                    this.employees = response.data
                    setTimeout(() => {
                        this.isloading = false
                        
                    },500)
                })
                .catch(e =>{
                    console.log(e)
                })
        }
    },
}
</script>

<style>
.m-table-container{
    position: relative;
}
.loading-data{
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: #fff;
}
.loading-data img{
    position: absolute;
    top: calc(50% - 64px);
    left: calc(50% - 64px);
}
</style>