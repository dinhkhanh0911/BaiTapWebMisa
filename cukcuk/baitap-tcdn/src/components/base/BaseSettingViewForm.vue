<template>
  <div class="popup-container">
    <div class="popup" ref="popup" id="popup">
      <div class="popup-dialog" role="document">
        <div class="popup-content">
          <div class="popup-header d-flex justify-space-between">
            <div class="popup-title-box">
              <div class="header-popup d-flex">
                <h1 class="popup-title">Chỉnh sửa giao diện</h1>
              </div>
            </div>
            <div class="close-popup">
              <div class="help mi mi-24"></div>
              <div class="close mi mi-24" @click="handleClosePopup()"></div>
            </div>
          </div>
          <div class="popup-body">
            <div class="popup-option">
                <div class="popup-search">
                    <BaseInput
                    :placeholder="'Tìm kiếm theo mã,tên nhân viên'"
                    v-model="searchValue"
                    :timer="1000"
                    :searchClass="'search-container'"
                  />
                </div>
                <div class="popup-edit" @click="isShowDetailSettingOption = !isShowDetailSettingOption">
                    <span>Sửa tên cột hiển thị và độ rộng</span>
                </div>
            </div>
            <div class="popup-table">
                <table>
                    <thead>
                        <tr>
                            <th class="d-flex alignt-center justify-space-center">
                              <div class="checkbox-container">
                                <input
                                  type="checkbox"
                                  class="checkbox"
                                  v-model="selectAll"
                                  
                                />

                                <span
                                  class="checkbox-border"
                                  :class="{
                                    'checkbox-border-active': selectAll,
                                  }"
                                >
                                  <span
                                    class="mi mi-16"
                                    :class="{
                                      'checkbox-active': selectAll,
                                    }"
                                  ></span>
                                </span>
                              </div>
                            </th>
                            <th >Tên cột dữ liệu</th>
                            <th :class="{'column-none' : !isShowDetailSettingOption}"><span>Tên cột trên giao diện</span></th>
                            <th style="width:100px" :class="{'column-none' : !isShowDetailSettingOption}"><span>Độ rộng</span></th>
                            <th style="width:140px" :class="{'column-none' : !isShowDetailSettingOption}"><span>Cố định cột</span></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(column, index) in columnsDescription"
                            :key="index"
                            @click="handleClickRow(index,column.columnId)"
                            
                        >
                            <td class="d-flex alignt-center justify-space-center height-34">
                              <div class="checkbox-container">
                                <input
                                  type="checkbox"
                                  class="checkbox"
                                  v-model="checkboxs"
                                  :value="column.columnId"
                                  @click="handleClickCheckbox($event,index,column.columnId)"
                                />

                                <span
                                  class="checkbox-border"
                                  :class="{
                                    'checkbox-border-active': checkboxs.includes(column.columnId),
                                  }"
                                >
                                  <span
                                    class="mi mi-16"
                                    :class="{
                                      'checkbox-active': checkboxs.includes(column.columnId),
                                    }"
                                  ></span>
                                </span>
                              </div>
                            </td>
                            <td >
                              {{column.columnName}}
                            </td>
                            <td @click="handleClickRow(index,column.columnId,`viewColumnName-${column.columnId}`)" style="width:200px"
                              :class="{'column-none' : !isShowDetailSettingOption}"
                            >
                              <BaseInput v-model="column.viewColumnName" 
                                :ref="`viewColumnName-${column.columnId}`" 
                                v-show="column.columnId == columnActive" 
                              />
                              <span v-show="!(column.columnId == columnActive)">{{column.viewColumnName}}</span>
                            </td>
                            <td @click="handleClickRow(index,column.columnId,`columnWidth-${column.columnId}`)"
                              :class="{'column-none' : !isShowDetailSettingOption}"
                            >
                              <BaseInput v-model="column.columnWidth" 
                                :ref="`columnWidth-${column.columnId}`" 
                                v-show="column.columnId == columnActive" 
                                style="width:100px"/>
                              <span v-show="!(column.columnId == columnActive)">{{column.columnWidth}}</span>
                            </td>
                            <td class="d-flex alignt-center justify-space-center" style="width:140px"
                              :class="{'column-none' : !isShowDetailSettingOption}"
                            >
                              <div class="checkbox-container">
                                <input
                                  type="checkbox"
                                  class="checkbox"
                                  v-model="checkboxs"
                                  
                                />

                                <span
                                  class="checkbox-border"
                                  :class="{
                                    'checkbox-border-active': true,
                                  }"
                                >
                                  <span
                                    class="mi mi-16"
                                    :class="{
                                      'checkbox-active': true,
                                    }"
                                  ></span>
                                </span>
                              </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!-- <div class="border-bottom"></div> -->
          </div>
          <div class="popup-footer">
            <div class="btn-container">
              <div class="btn-left d-flex">
                <BaseButton
                  :classBtn="'btn-default btn-sq btn-no-op'"
                  :content="'Hủy'"
                  @click="closeForm(false)"
                  @keyup.tab="handleTabEvent($event)"
                />
              </div>
              <div class="btn-right d-flex">
                <BaseButton
                  :classBtn="'btn-default btn-sq btn-no-op'"
                  :content="'Lấy mẫu ngầm định'"
                  @click="handleSave()"
                />
                <BaseButton
                  :classBtn="'btn-default btn-sq btn-no-op btn-primary'"
                  :content="'Cất'"
                  @click="handleSave()"
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
import BasePopup from "@/components/base/BasePopup.vue";
import "vue-datepicker-next/index.css";
import Helpers from "@/assets/js/helpers.js";
import axios from 'axios';
export default {
  components: {
    BaseInput,
    BaseButton,
    BasePopup,
  },
  props: {
      api:{
          type:String,
          default:""
      },
      tableName:{
        type:String,
        default:""
      }
  },
  data(){
      return{
          tableDescription:{},
          columnsDescription:[],
          checkboxs:[],
          columnActive:"",
          isShowDetailSettingOption:false,
      }
  },
  computed:{
    // Chỉnh sửa select
    selectAll: {
      get: function () {
        return this.columnsDescription.length
          ? this.checkboxs.length == this.columnsDescription.length
          : false;
      },
      set: function (value) {
        var checkboxs = [];
        if (value) {
          this.columnsDescription.forEach(function (column) {
            checkboxs.push(column.columnId);
          });
        }

        this.checkboxs = [...checkboxs];
      },
    }
  },
  watch:{
    checkboxs:function(){
      console.log(this.checkboxs)
    },
  },
  mounted(){
      if(!!this.api){
        axios.get(`${this.api}/${this.tableName}`)
        .then((response) => {
          if (response.status === 200) {
            this.tableDescription = response.data
            this.columnsDescription = this.tableDescription.columns
            this.columnsDescription.filter(column => {
              if(column.isShow === 'True'){
                this.checkboxs.push(column.columnId)
              }
            })
          }
        })
        .catch((e) => {
          console.log(e);
        });
      }
  },
  methods:{
    handleClickRow(index,columnId,refInput){
      this.columnActive = columnId
      console.log(this.columnActive)
      this.$nextTick(() => {
        // The whole view is rendered,
        // so I can safely access or query the DOM. ¯\_(ツ)_/¯
        if(!!refInput){
          console.log(refInput)
          console.log(this.$refs[`${refInput}`])
          this.$refs[`${refInput}`][0].select();
        }
      })
      
      // if(!!refInput){
      //   console.log(this.$refs['1'][0])
      //   this.$refs['1'][0].focus();
      // }
    },
    handleClickCheckbox(e,index,columnId){
        e.stopPropagation()
        this.$nextTick(() => {
        // The whole view is rendered,
        // so I can safely access or query the DOM. ¯\_(ツ)_/¯
        // this.columnActive = columnId
      })
        // this.columnActive = columnId
        
    },
    handleSave(){
      this.tableDescription.columns = this.columnsDescription.map(column => {
        var ok = this.checkboxs.includes(column.columnId)
        console.log(column)
        if(ok) column.isShow = 'True'
        else column.isShow = 'False'
        return column
      })
      console.log(this.tableDescription.columns)
      axios
          .put(
            `${this.api}/${this.tableDescription._id}`,
            this.tableDescription
          )
          .then(response => {
            console.log(response)
            this.closeForm(true)
          })
          .catch(e => {
            this.closeForm(true)
            console.log(e)
          })
    },
    closeForm(isLoad){
      this.$emit('close',isLoad)
    }
  }
}
</script>
<style scoped>
@keyframes moveUpToDown {
  from {
    transform: translateY(-100%) translateX(-50%);
    opacity: 0;
  }
  to {
    transform: translateY(-50%) translateX(-50%);
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
}
.popup {
  width: 800px;
  background-color: #fff;
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  /* transform: translateX(-50%) translateY(-50%); */
  /* animation: moveUpToDown linear 0.3s; */
}

/* Popup header */

.popup-title-box {
  padding: 12px 32px;
}
.close-popup {
  display: flex;
  padding: 12px;
}
.help {
  background-position: -89px -144px;
  cursor: pointer;
}
.close {
  background-position: -144px -144px;
  cursor: pointer;
  margin-left: 10px;
}
.popup-body {
  padding: 0px 32px 0px 32px;
}
.border-bottom {
  border-bottom: 1px solid #bbb;
}

/* popup-option */
.popup-option{
    display: flex;
    justify-content: space-between;
}
.popup-edit span{
    color: #0075c0;
    cursor: pointer;
}

/* footer */
.popup-footer {
  padding: 24px 32px;
}
.btn-container {
  display: flex;
  justify-content: space-between;
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
  width: 18px;
  height: 18px;
  position: relative;
}
.checkbox {
  position: absolute;
  top: 0;
  left: 0;
  width: 18px;
  height: 18px;
  z-index: 2;
  opacity: 0;
}
.checkbox-border {
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

/*Table */
.popup-table{
  margin-top: 10px;
}
table{
  width: 100%;
  border-collapse:separate;
  border-spacing: 0;
}
td,th{
  border-bottom: 1px solid #c7c7c7;
  min-width: 38px;
  
  padding: 0 20px;
  white-space: nowrap;
}
table thead tr th {
  height: 33px;
  text-align: left;
  background-color: #e5e8ec;
  text-transform: uppercase;
}
table tbody tr td{
  height: 44px;
}
table tbody tr:hover>td{
  background: #f2f9ff!important;
}
.column-none *{
  opacity: 0;
  pointer-events: none;
}

</style>
