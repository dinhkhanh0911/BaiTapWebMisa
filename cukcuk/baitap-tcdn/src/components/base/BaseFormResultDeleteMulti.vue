<template>
  <div class="popup-container">
    <div class="popup" ref="popup" id="popup">
      <div class="popup-dialog" role="document">
        <div class="popup-content">
          <div class="popup-header d-flex justify-space-between">
            <div class="popup-title-box d-flex">
              <div class="header-popup">
                <h1 class="popup-title">Kết quả xóa {{description}}</h1>
              </div>
                <div class="result-container">
                    <div class="result">
                        <span >Số nhà cung cấp được xử lý: {{result.TotalRecord}}</span>
                    </div>
                    <div class="result">
                        <span >Xóa thành công: {{result.Success}}</span>
                    </div>
                    <div class="result">
                        <span >Xóa không thành công: {{result.Failed}}</span>
                    </div>
                </div>
            </div>
            <div class="close-popup">
              <div class="help mi mi-24"></div>
              <div class="close mi mi-24" @click="handleClosePopup()" :title="Esc"></div>
            </div>
          </div>
          <div class="popup-body">
            <div class="grid">
                <div class="table-container">
                    <table id="table" class="tableEmployee">
                        <thead >
                            <tr class="sticky-top--87">
                            <th v-for="(column, index) in columnsDescription" :key="index" :class="column.columnClass" class="resizeable" 
                            v-bind:style="{'min-width': column.columnWidth + 'px'}" > {{column.viewColumnName}}</th>
                            </tr>
                        </thead>
                        <tbody class="bg-while">
                            <tr
                            v-for="(entity, index) in listEntity"
                            :key="index"
                            >
                            <td v-for="(column, index) in columnsDescription" :key="index" :class="column.columnClass"
                                > 
                                {{entity[column.columnField]}}
                            </td>
                            </tr>
                        </tbody>
                        </table>
                    </div>
            </div>
            <div class="border-bottom"></div>
          </div>
          <div class="popup-footer">
            <div class="btn-container">
              <div class="btn-left d-flex">
                
              </div>
              <div class="btn-right d-flex">
                
                <BaseButton
                  :classBtn="'btn-default btn-sq btn-no-op btn-primary'"
                  :content="'Đóng'"
                  @click="handleClosePopup()"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <BasePopup
      v-if="isShowPopupInfo"
      :contentPrev="contentPopupInfo"
      @confirm="handleConfirmBtn"
      :type="typePopupInfo"
      :keyCombobox="keyCombobox"
    />
  </div>
</template>
<script>
/**
 * Mô tả: Form thêm nhân viên
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 12/04/2022
 */

import BaseInput from "@/components/base/BaseInput.vue";
import BaseButton from "@/components/base/BaseButton.vue";
import BaseRadioButton from "@/components/base/BaseRadioButton.vue";
import BaseCombobox from "@/components/base/BaseCombobox.vue";
import BaseComboboxMulti from "@/components/base/BaseComboboxMulti.vue";
import BaseEditableTable from "@/components/base/BaseEditableTable.vue";
import axios from "axios";
import Validator from "@/assets/js/validator.js";
import BasePopup from "@/components/base/BasePopup.vue";
import DatePicker from "vue-datepicker-next";
import "vue-datepicker-next/index.css";
import Helpers from "@/assets/js/helpers.js";
import Api from "@/assets/js/api.js";
import DB from "@/assets/js/hashDatabase";
import BaseTable from "@/components/base/BaseTable.vue"
export default {
  components: {
    BaseInput,
    BaseButton,
    BaseRadioButton,
    BaseCombobox,
    BasePopup,
    DatePicker,
    BaseComboboxMulti,
    BaseEditableTable,
    BaseTable
},
  props: {
    modelPopup: {
      type: Object,
      default: {},
    },
    apiColumn:{
        type:String,
        default:""
    },
    listEntity:{
        type:Array,
        default:[]
    },
    result:{
        type:String,
        default:""
    },
    tableName:{
        type:String,
        default:""
    },
    description:{
      type:String,
      default:""
    },

  },
  data() {
      return{
          columnsDescription:[],
      }
  },

  mounted() {
    //lắng nghe sự kiện bàn phím
    window.addEventListener("keyup", this.handleKeyEvent);
    window.addEventListener("keydown", this.handleKeyEventDown);

    //set draggable popup
    Helpers.draggable("#popup");
  },
  //Bỏ lắng nghe xự kiện
  beforeUnmount() {
    window.removeEventListener("keyup", this.handleKeyEvent);
    window.removeEventListener("keydown", this.handleKeyEventDown);
  },
  created() {
      this.loadColumnTable()
  },
  methods:{
    handleClosePopup(){
      this.$emit('close',true)
    },
      /**
    * Mô tả: Load cột của bảng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 09/05/2022
    */
    loadColumnTable(){
      axios
        .get(`${this.apiColumn}/${this.tableName}`)
        .then((response) => {
          if (response.status === 200) {
            this.columnsDescription = response.data.columns.filter(column => column.isShow === "True")
            console.log(this.columnsDescription)
          }
        })
        .catch((e) => {
          console.log(e);
        });
    }
  }
};
</script>
<style scoped>
@import url("@/assets/css/commom/datepicker.css");

@keyframes moveUpToDown {
  from {
    transform: translateY(-100%);
    opacity: 0;
  }
  to {
    transform: translateY(0%);
    opacity: 1;
  }
}
.col {
  padding: 0 4px;
}
.padding-right-8 {
  padding-right: 8px;
}
.padding-left-8 {
  padding-left: 8px;
}
.popup-container {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.6);
  z-index: 100;
  display: flex;
  align-items: center;
  justify-content: center;
}
.popup {
  
  background-color: #fff;
  
  position: relative;
  animation: moveUpToDown linear 0.4s;
}

/* Popup header */

.popup-title-box {
  padding: 12px 32px;
  flex-direction: column;
  align-items: flex-start;
}
.close-popup {
  display: flex;
  padding: 12px;
}
.help {
  background-position: -89px -144px;
  cursor: pointer;
}
.result-container{
    display: flex;
}
.result + .result {
    margin-left: 10px;
}
.close {
  background-position: -144px -144px;
  cursor: pointer;
  margin-left: 10px;
}

/* popup body */
.popup-body {
  padding: 0px 32px 0px 32px;
  max-height: 425px;
  overflow-y: auto;
  width: 913px;
  position: relative;
}
.popup-body .row{
  margin: 0px;
}
.border-bottom {
  border-bottom: 1px solid #bbb;
}
/* footer */
.popup-footer {
  padding: 24px 32px;
}
.btn-container {
  display: flex;
  justify-content: space-between;
}
.sub-input-container{
  border: 1px solid #c7c7c7;
  padding: 5px 10px;
  min-height: 186px;
  max-height: 186px;
  overflow-y: auto;
  position: relative;
  padding-bottom: 0;
}
.add-btn-content{
  position: sticky;
  bottom: 0;
  background-color: #fff;
}
table {
  border-spacing: 0;
  width: 100%;
}
table tr {
  width: 100%;
}
table thead{
  position: sticky;
  top: 0;
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
</style>
      