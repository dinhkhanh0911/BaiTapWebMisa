<template>
  <div class="table-container">
    <table>
        <thead>
            <tr>
                <th >Tên cột dữ liệu</th>
                <th :class="{'column-none' : !isShowDetailSettingOption}"><span>Tên cột trên giao diện</span></th>
                <th style="width:100px" :class="{'column-none' : !isShowDetailSettingOption}"><span>Độ rộng</span></th>
                <th style="width:140px" :class="{'column-none' : !isShowDetailSettingOption}"><span>Cố định cột</span></th>
            </tr>
        </thead>
        <tbody>
            <tr 
                @click="handleClickRow(index,column.columnId)"
                
            >
                <td v-for="(value,index) in colNumber" :key="index">
                    <BaseInput />
                </td>
            </tr>
        </tbody>
    </table>
    
  </div>
</template>

<script>
/**
 * Mô tả:  Bảng thông tin nhân viên
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 07/04/2022
 */
import axios from "axios";
import BasePopup from "@/components/base/BasePopup.vue";
import BaseInput from "@/components/base/BaseInput.vue"
import Api from "@/assets/js/api.js";
export default {
    components: {
        BasePopup,
        BaseInput,
        },
    data() {
        return {
        
        //Popup
        contentPopupInfo: "",
        isShowPopupInfo: false,
        typePopupInfo: "error",

        //checkbox
        checkboxs: [],
        checked: false,

        //option
        indexOption: -1,

        columnsDescription:[],
        };
    },
    props: {
        api:{
            type:String,
            default:""
        },
        tableName:{
        type:String,
        default:""
        },
        colNumber:{
            type:Number,
            default:1
        }
    },
    created() {
        
        this.loadColumnTable()
    },
    mounted() {
        window.addEventListener("click", this.handleKeyEventClick);
    },
    computed: {
        // Chỉnh sửa select
        selectAll: {
        get: function () {
            return this.employees.length
            ? this.checkboxs.length == this.employees.length
            : false;
        },
        set: function (value) {
            var checkboxs = [];
            if (value) {
            this.employees.forEach(function (employee) {
                checkboxs.push(employee.EmployeeId);
            });
            }

            this.checkboxs = [...checkboxs];
        },
        },
    },
    methods:{
        loadColumnTable(){
            
        }
    }
    
};
</script>

<style scoped>
table {
  border-spacing: 0;
  width: 100%;
}
table tr {
  width: 100%;
}
table thead tr th {
  background-color: #e5e8ec;
}

table tr:hover > td {
  background: #f2f9ff;
}

table tr th {
  text-align: left;
  text-transform: uppercase;
  border-right: 1px solid #c7c7c7;
}
table tr td {
  border-right: 1px dotted #c7c7c7;
}
table tr td,
table tr th {
  height: var(--grid-body-line-height);
  padding: 0 10px;
  border-bottom: 1px solid #c7c7c7;

  margin: 0;
  white-space: nowrap;
  min-width: 130px;
  /* text-align: left;
    border-bottom: 1px solid #111;
    border-right: 1px solid #111;
    min-width: 150px;
    padding: 0 10px;
    white-space: nowrap; */
}
table tr .option-column-td {
  border-right: none;
  border-left: 1px dotted #c7c7c7;
  min-width: 120px;
}
.border-right-solid {
  border: 1px solid #c7c7c7 !important ;
}
table tr .border-right-none {
  border-right: none;
}
table tr .checkbox-column {
  min-width: unset !important;
}
table tbody .active td {
  background: #f2f9ff;
}
table tr .padding-td {
  background-color: #fff !important;
  position: sticky;
  z-index: 4;
  border: none;
  left: 0;
  min-width: unset;
  
}
table tr .padding-td:first-child {
  left: 0;
  padding: 0 8px;
  width: 16px;
}
.padding-right-td {
  right: 0px;
  width: 20px;
}
table tr:hover > .padding-td {
  background-color: #fff;
}
.hidden-td {
  min-width: unset;
  
  position: sticky;
  position: -webkit-sticky;
  right: -30px;
  background-color: #eceef1 !important;
  border: none;
  padding: 0 15px;
}
table tr:hover > .hidden-td {
  background-color: #eceef1;
}

/*giới hạn kí tự hiển thị*/

.overflow-content {
  white-space: unset;
}
.overflow-content div {
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  /* number of lines to show */
  line-clamp: 2;
  -webkit-box-orient: vertical;
  min-width: 170px;
  max-width: 200px;
}
.edit-btn {
  width: 26px;

  color: #0075c0;
  font-size: 13px;
  cursor: pointer;
  font-family: MISA notosans bold;
}
.option-btn {
  width: 26px;
  /* border: 1p75c0x solid #00; */
  margin-left: 10px;
}
.option-btn:hover {
  border: 1px solid #0075c0;
}
.option-column-icon {
  background-position: -896px -359px;
}
.option-item-container {
  position: fixed;
  /* left: calc(894px - 100px);
    top: calc(289px + 10px); */

  width: 100px;
  background-color: #fff;
  border: 1px solid black;
  z-index: 10000;
}
.option-item-container li {
  padding: 5px 10px;
}
.option-item-container li:hover {
  background-color: #ebedf0;
  color: #35bf22;
}

@keyframes moving-gradient {
  0% {
    background-position: -250px 0;
  }
  100% {
    background-position: 250px 0;
  }
}
.m-tbody {
  width: 100%;
}
.m-tbdoy .padding-td {
  padding: 0 8px;
}
.loading-1 {
  height: 18px;
  background: linear-gradient(to right, #eee 20%, #ddd 50%, #eee 80%);
  background-size: 500px 100px;
  animation-name: moving-gradient;
  animation-duration: 0.5s;
  animation-iteration-count: infinite;
  animation-timing-function: linear;
  animation-fill-mode: forwards;
  width: 150px;
  background-color: #bbb;
}
.loading-body tr td {
  background-color: #ebedf0;
}
.loading-body tr .padding-td {
  background-color: #fff;
}
.text-center {
  text-align: center !important;
}

/* Checkbox */
@keyframes rotate {
  from {
    transform: rotate(90deg);
  }
  to {
    transform: rotate(0deg);
  }
}
.checkbox-container {
  width: 20px;
  height: 20px;
  position: relative;
}
.checkbox {
  position: absolute;
  top: 0;
  left: 0;
  width: 20px;
  height: 20px;
  z-index: 2;
  opacity: 0;
}
.checkbox-border {
  width: 100%;
  height: 100%;
  background-color: #fff;
  border-radius: 2px;
  display: flex;
  justify-content: center;
  align-items: center;
  border: 1px solid #c7c7c7;
}
.checkbox-border-active {
  border: 1px solid #2ca01c;
  animation: rotate linear 0.1s;
}
.checkbox-active {
  background-position: -1224px -360px;
  margin-bottom: 2px;
}
</style>
