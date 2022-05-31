<template>
  <div class="router-view payment">
    <div class="router-content">
      <div class="content-area-content">
        <div class="layout-dictionary">
          <div class="title-distance" :style="{ top: topTitle + 'px' }">
            <div class="header-di">
              <div class="title-di d-flex alignt-center">
                <span>Thu chi tiền mặt</span>
              </div>
              <div class="btn-container">
                <BaseButton
                  :addButton="'addButton'"
                  :classBtn="'btn-default btn-primary'"
                  :content="'Thêm mới phiếu chi'"
                  @click="handleAddClick()"
                />
              </div>
            </div>
          </div>
          <div class="counter grid" :style="{ top: topTitle + 'px' }">
            <div class="row" style="width:100%">
              <div class="view-total  col c-4">
                <div class="total-receipt">
                  <div class="total">100,0</div>
                  <div class="text">Tổng thu đầu năm đến hiện tại</div>

                </div>
              </div>
              <div class="view-total col c-4">
                <div class="total-payment">
                  <div class="total">100,0</div>
                  <div class="text">Tổng chi đầu năm đến hiện tại</div>

                </div>
              </div>
              <div class="view-total  col c-4">
                <div class="total-count">

                  <div class="total">100,0</div>
                  <div class="text">Tổng quỹ đầu năm đến hiện tại</div>
                </div>
              </div>
            </div>
          </div>
          <div class="tab-table-container" style="width:100%" :style="{ top: topTitle + 'px' }">
            <div class="tab-item">
              <div class="text">Tất cả</div>
            </div>
            <div class="tab-item">
              <div class="text">Thu tiền</div>
            </div>
            <div class="tab-item">
              <div class="text">Chi tiền</div>
            </div>
          </div>
          <div class="layout-dictionary-body" @scroll="handleScroll($event)">
            <div class="table-option">
              <div class="table-option-left d-flex">
                <div class="icon-table-option mi mi-24">

                </div>
                <div class="btn-delete-multi">
                  <BaseButton
                    :classBtn="'btn-default'"
                    :content="'Thực hiện hàng loạt'"
                    :disabled="isDisableMultipleDeleteBtn"
                    @click="handleMultipleDeleteClick()"
                  />
                </div>
                <div class="btn-filter">
                  <BaseButton
                    :classBtn="'btn-default'"
                    :content="'Lọc'"
                    ref="filter"
                    @click="handlefilterClick('filter')"
                  />
                  
                </div>
              </div>

              <div class="table-option-right d-flex alignt-center">
                <div class="input-search">
                  <BaseInput
                    :placeholder="'Nhập từ khóa tìm kiếm'"
                    v-model="searchValue"
                    :timer="1000"
                    :searchClass="'search-container'"
                  />
                </div>
                <div
                  class="reload btn-icon mi mi-24"
                  @click="reloadData()"
                  :title="'Lấy lại dữ liệu'"
                ></div>
                <div
                  class="export btn-icon mi mi-24"
                  
                  @click="handleExport()"
                  :title="'Xuất dữ liệu'"
                ></div>
                <div class="setting btn-icon mi mi-24 "
                   :title="'Chỉnh sửa giao diện'"
                    @click="isShowSettingViewForm = true"
                ></div>
              </div>
            </div>
            <BaseTable
              ref="tableVendor"
              @ObjectValue="handleEdit"
              :searchValue="searchValue"
              :currentPage="currentPage"
              :pageSize="pageSize"
              :loadHandle="loadHandle"
              :loadColumnHandle="loadColumnHandle"
              @showToast="showToast"
              @loading="isNoData = false"
              @check="handleCheckDataTable"
              :api="apiColumn"
              tableName="Payment"
              entityId="PaymentId"
              entityCode="PaymentCode"
              entityName="PaymentName"
              :apiFilter="paymentsFilterApi"
              :apiDelelte="paymentsApi"
              :showBtn="'true'"
              :className="'payment'"
            >
              <template #footer>
                <tfoot v-if="isShowFooter" class="m-tfoot">
                  <tr class="">
                      <td class="padding-td"></td>
                      <td
                        class="checkbox-column"
                      >
                      </td>
                      <td v-for="(column, index) in columns" :key="index" :class="column.columnClass" class="resizeable tr-bind"
                      v-bind:style="{'min-width':column.ColumnWidth + 'px'}" >
                        <span v-if="column.columnField == 'TotalMoney' && column.isShow == 'True'">{{formatNumber(totalMoneyTable)}}</span>
                        <span v-else-if="index == 0 && column.isShow == 'True'">{{ "Tổng tiền" }}</span>
                        <span v-else></span>
                      
                      </td>
                      
                      <td
                        class="option-column-td sticky-right-30 justify-space-center alignt-center text-center border-right-solid"
                      >
                        
                      </td>
                      <td class="padding-td padding-right-td"></td>
                      <td class="hidden-td"></td>
                  </tr>
                </tfoot>
              </template>
              </BaseTable>
            <div class="router-pagination">
              <div
                class="pagination-no-data d-flex justify-space-center alignt-center"
                v-if="isNoData"
              >
                <div class="no-data-info">
                  <img
                    src="https://actappg1.misacdn.net/img/bg_report_nodata.76e50bd8.svg"
                    alt="Không có dữ liệu"
                  />
                  <span>Không có dữ liệu</span>
                </div>
              </div>
              <div
                class="pagination d-flex justify-space-between alignt-center"
                v-else
              >
                <div class="left-pagination d-flex alignt-center">
                  <div class="total-page">
                    Tổng số:<span style="font-weight: 700">{{
                      totalRecord
                    }}</span>
                    bản ghi
                  </div>
                </div>
                <div class="right-pagination d-flex alignt-center">
                  <BaseCombobox
                    :isDropDown="false"
                    :valueOption="valueOption"
                    :id="'Id'"
                    :name="'Value'"
                    :code="'Id'"
                    :isOneColumn="'true'"
                    v-model="pageSize"
                  />
                  <paginate
                    v-model="currentPage"
                    :page-count="totalPage"
                    :page-range="4"
                    :click-handler="clickCallback"
                    :prev-text="'Trước'"
                    :next-text="'Sau'"
                    :container-class="'page-container'"
                    :page-class="'page-number-btn'"
                  >
                  </paginate>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <BaseTooltip
      :top="topTooltip"
      :left="leftTooltip"
      v-if="isShowTooltip"
      :valueTooltip="valueTooltip"
    />
    <div id="toast">
      <BaseToast
        v-for="(toast, index) in toasts"
        :key="index"
        :message="toast.message"
        :type="toast.type"
      />
    </div>
    <BasePopup
      v-if="isShowPopupInfo"
      :contentPrev="contentPopupInfo"
      @confirm="handleConfirmBtn"
      :type="typePopupInfo"
    />
    <BaseSettingViewForm 
      :api="apiColumn"
      tableName="Payment"
      v-if="isShowSettingViewForm"
      @close="handleCloseSettingViewForm"
    />
    <ThePaymentDetail
      v-if="isShowPopupPayment"
      :model="payment"
      @showPopup="togglePopupPayment"
      @showToast="showToast"
      @addSuccess="reloadData()"
    />
    <BaseFormResultDeleteMulti 
      v-if="isShowFormResultDeleteMulti" 
      :listEntity="listEntity"
      :result="resultDeleteMulti"
      :apiColumn="apiColumn"
      :tableName="'PaymentResult'"
      :description="'phiếu chi'"
      @close="handleCloseFormResult"
    />
    <div class="filter-option-container" v-if="isShowFilter" v-bind:style="{ top: filterBtnTop + 'px', left: filterBtnLeft + 'px' }">
      <div class="filter-option grid">
          <!-- Tổ chức -->
        <div class="primary-input">
          <div class="row">
            <div class="col c-12">
              <div class="form-group">
                <label for=""
                  >Lý do</label
                >
                <BaseCombobox
                  :valueOption="filterTypePayments"
                  :id="'Id'"
                  :name="'Value'"
                  :code="'Id'"
                  :isOneColumn="'true'"
                  v-model="filterObject.FilterTypePaymentId"
                />
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col c-12">
              <div class="form-group">
                <label for=""
                  >Trạng thái ghi nợ</label
                >
                <BaseCombobox
                  :valueOption="filterRecordPayments"
                  :id="'Id'"
                  :name="'Value'"
                  :code="'Id'"
                  :isOneColumn="'true'"
                  v-model="filterObject.FilterRecordPaymentId"
                />
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col c-4">
              <div class="form-group">
                <label for=""
                  >Tùy chọn</label
                >
                <BaseCombobox
                  :valueOption="filterOptionPayments"
                  :id="'Id'"
                  :name="'Value'"
                  :code="'Id'"
                  :isOneColumn="'true'"
                  v-model="filterObject.FilterOptionPaymentId"
                  @change="handleChangeOptionFilter"
                />
              </div>
            </div>
            <div class="col c-4">
              <div class="form-group">
                <label for="">Từ ngày</label>
                <DatePicker
                  v-model:value="formatStartDate"
                  :format="'DD/MM/YYYY'"
                  :placeholder="'DD/MM/YYYY'"
                  :lang="'vi'"
                  :clearable="false"
                  title-format="DD/MM/YYYY"
                />
              </div>
            </div>
            <div class="col c-4">
              <div class="form-group">
                <label for="">Đến ngày</label>
                <DatePicker
                  v-model:value="formatEndDate"
                  :format="'DD/MM/YYYY'"
                  :placeholder="'DD/MM/YYYY'"
                  :lang="'vi'"
                  :clearable="false"
                  title-format="DD/MM/YYYY"
                />
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col c-6">
              <div class="btn-default-filter">
                  <BaseButton
                    :classBtn="'btn-default btn-sq btn-no-op'"
                    :content="'Đặt lại'"
                    @click="setDefaultFilter()"
                  />
              </div>
            </div>
            <div class="col c-6">
              <div class="btn-filter-form">
                  <BaseButton
                    :classBtn="'btn-default btn-sq btn-no-op btn-primary'"
                    :content="'Lọc'"
                    @click="reloadData()"
                  />
              </div>
            </div>
          </div>
        </div>
      </div>            
    </div>
  </div>
</template>

<script>
/**
 * Mô tả:  Giao diện trang nhân viên
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 07/04/2022
 */
import BaseButton from "@/components/base/BaseButton.vue";
import BaseTable from "@/components/base/BaseTable.vue";
import BaseInput from "@/components/base/BaseInput.vue";
import TheEmployeePopup from "@/components/layout/TheEmployeePopup.vue";
import BaseTooltip from "@/components/base/BaseTooltip.vue";
import Paginate from "vuejs-paginate-next";
import BaseToast from "@/components/base/BaseToast.vue";
import BasePopup from "@/components/base/BasePopup.vue";
import BaseCombobox from "@/components/base/BaseCombobox.vue";
import BaseSettingViewForm from '@/components/base/BaseSettingViewForm.vue'
import ThePaymentDetail from "@/components/layout/ThePaymentDetail.vue";
import BaseFormResultDeleteMulti from '@/components/base/BaseFormResultDeleteMulti.vue'
import Api from "@/assets/js/api";
import axios from "axios";
import DB from '@/assets/js/hashDatabase'
import DatePicker from "vue-datepicker-next";
import "vue-datepicker-next/index.css";
export default {
  components: {
    BaseButton,
    BaseTable,
    BaseInput,
    TheEmployeePopup,
    BaseTooltip,
    Paginate,
    BaseToast,
    BasePopup,
    BaseCombobox,
    BaseSettingViewForm,
    ThePaymentDetail,
    DatePicker,
    BaseFormResultDeleteMulti
},
  data() {
    return {
      isNoData: false,
      PaymentIds: [],
      isDisableMultipleDeleteBtn: true,
      isShowOptionItem: false,

      isShowPopupPayment: false,
      payment: {},

      //filter
      searchValue: "",
      currentPage: 1,
      pageSize: 20,
      totalRecord: 0,
      totalPage: 0,

      //Toast
      toasts: [],
      topTitle: 0,
      toastMessage: "",
      toastType: "success",
      isShowToast: false,

      //Popup
      contentPopupInfo: "",
      isShowPopupInfo: false,
      typePopupInfo: "error",

      //Combobox phân trang
      valueOption: DB.valueOption,

      apiColumn:Api.getColumnOption,
      isShowSettingViewForm:false,
      paymentsFilterApi:`${Api.payments}/filterAdvanced`,
      paymentsApi:Api.payments,

      //Filter
      filterBtnLeft:0,
      filterBtnTop:0,
      isShowFilter:false,
      filterObject:{},
      //Dữ liệu hash data filter
      filterTypePayments:DB.filterTypePayments,
      filterRecordPayments:DB.filterRecordPayments,
      filterOptionPayments:DB.filterOptionPayments,

      //table
      //danh sách cột
      columns:[],
      totalMoneyTable:0,
      isShowFooter:false,

      //Xóa nhiều
      isShowFormResultDeleteMulti:false,
      listEntity:[],
      resultDeleteMulti:{}
    };

  },
  computed:{

    /**
     * Mô tả:  Chuyển định dạng bắt đầu filter
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 22/05/2022
     */
    formatStartDate: {
      get() {
        if(this.filterObject.StartDate != null){
          var date = new Date(this.filterObject.StartDate);
          return date;
        }
      },
      set(date) {
        this.filterObject.StartDate = new Date(date);
        this.filterObject.StartDate = this.filterObject.StartDate.toDateString();
        
      },
    },
    /**
     * Mô tả:  Chuyển định dạng bắt đầu filter
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 22/05/2022
     */
    formatEndDate: {
      get() {
        if(this.filterObject.EndDate != null){
          var date = new Date(this.filterObject.EndDate);
          return date;
        }
      },
      set(date) {
        this.filterObject.EndDate = new Date(date);
        this.filterObject.EndDate = this.filterObject.EndDate.toDateString();
        
      },
    },
  }, 
  watch:{

  },
  created(){
    this.setDefaultFilter()
  },   
  /**
   * Mô tả: Lắng nghe sự kiện
   * Created by: Đinh Văn Khánh - MF1112
   * Created date: 07/04/2022
   */
  mounted() {
    window.addEventListener("keyup", this.handleKeyEvent);
    window.addEventListener("keydown", this.handleKeyEventDown);
  },
  /**
   * Mô tả: Xóa bỏ sự kiện
   * Created by: Đinh Văn Khánh - MF1112
   * Created date: 07/04/2022
   */
  beforeUnmount() {
    window.removeEventListener("keyup", this.handleKeyEvent);
    window.removeEventListener("keydown", this.handleKeyEventDown);
  },
  methods: {

    /**
      * Mô tả:  Chuyển dữ liệu sang dạng number
      * Created by: Đinh Văn Khánh - MF1112
      * Created date: 26/05/2022
      */
      formatNumber(value){
          
          try {
          if (!value) return 0;
          let result = Intl.NumberFormat("vi-VN").format(value);
          return result;
              
          } catch (error) {
              console.log(error);
              return 0
          }
      },
    /**
    * Mô tả: Đóng form kết quả xóa nhiều
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 28/05/2022
    */
    handleCloseFormResult(){
      this.isShowFormResultDeleteMulti = false,
      this.listEntity = []
      this.resultDeleteMulti = {}
    },
    loadColumnHandle(dataTable,columns){
      var totalMoney = dataTable.reduce((prev,current)=>{
        if(!!current) return prev + current.TotalMoney
        else return prev
      },0)

      this.totalMoneyTable = parseFloat(totalMoney).toFixed(1)
      this.columns = columns
      if(!!dataTable.length && !!columns.length) this.isShowFooter = true
      else this.isShowFooter = false
    },
    handleChangeOptionFilter(value){
      console.log(value)
      var startDate = new Date()
      var endDate = new Date()
      switch(value.Id){
        //Năm nay
        case 5:
          startDate.setDate(1)
          startDate.setMonth(0)
          endDate.setDate(1)
          endDate.setMonth(12)
          break
        case 2:
          startDate.setDate(1)
          startDate.setMonth(1)
          endDate.setDate(1)
          endDate.setMonth(12)
          break
        case 1:
          startDate.setDate(1)
          startDate.setMonth(startDate.getMonth())
          endDate.setDate(1)
          endDate.setMonth(endDate.getMonth()+1)
          break
      }
      console.log(startDate)
      this.filterObject.StartDate = startDate.toDateString()
      this.filterObject.EndDate = endDate.toDateString()
    },
    /**
    * Mô tả: Chỉnh mặc định các trường lọc
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 20/05/2022
    */
    setDefaultFilter(){
      //Set ngày bắt đầu 01/01/1970
      var startDate = new Date()
      startDate.setDate(1)
      startDate.setMonth(0)
      startDate.setFullYear(1970)
      var endDate = new Date()
      
      //Set object filter
      this.filterObject = {
        FilterTypePaymentId:7,
        FilterRecordPaymentId:2,
        FilterOptionPaymentId:4,
        StartDate:startDate,
        EndDate:endDate
      }
    },

    /**
    * Mô tả: Xử lý vị trí của nút button
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 20/05/2022
    */
    handlefilterClick(refName){
      
      this.filterBtnLeft = parseInt(this.$refs[refName].$el.getBoundingClientRect().left)
      this.filterBtnTop = parseInt(this.$refs[refName].$el.getBoundingClientRect().top+50)
      this.isShowFilter = !this.isShowFilter
      console.log(this.filterBtnLeft,this.filterBtnTop)
    },
    //Hiển thị toast message
    showPopupInfo(message = "Thành công", type = "success") {
      this.contentPopupInfo = message;
      this.typePopupInfo = type;
      this.isShowPopupInfo = true;
    },
    /*
            Hiểm thị popup thêm nhân viên
        */
    togglePopupPayment(value) {
      this.isShowPopupPayment = value;
    },

    // Hiển thị toast
    showToast(message = "Thành công", type = "success") {
      this.toasts.push({ message, type });
      var me = this;
      setTimeout(function () {
        me.toasts.shift();
      }, 3000);
    },
    /* 
            Xử ly sự kiện khi bấm nút thêm nhân viên
        */
    handleAddClick() {
      this.payment = {};
      this.togglePopupPayment(true);
    },
    /*
            Xử lý sự kiện khi bấm nút sửa nhân viên
        */
    handleEdit(payment) {
      this.payment = payment;
      this.togglePopupPayment(true);
    },

    /*
        Lấy dữ liệu 
    */
    reloadData(currentPage = 1) {
      //Đóng form lọc
      this.isShowFilter = false

      var queryString = this.getQueryString()
      // this.currentPage = 1
      this.$refs.tableVendor.loadAllData(currentPage,queryString);
      
    },
    /**
    * Mô tả: Lấy query string filter
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 31/05/2022
    */
    getQueryString(){
      var queryString =""
      if(this.filterObject.FilterTypePaymentId != 8){
        queryString += `&paymentType=${this.filterObject.FilterTypePaymentId}`
      }
      if(this.filterObject.FilterRecordPaymentId == 3 ){
        queryString += `&isRecord=${true}`
      }
      if(this.filterObject.FilterRecordPaymentId ==1 ){
        queryString += `&isRecord=${false}`
      }
      console.log(this.filterObject.EndDate)
      var startDate = new Date(this.filterObject.StartDate)
      var endDate = new Date(this.filterObject.EndDate)
      queryString += `&startDate=${startDate.toDateString()}`
      queryString += `&endDate=${endDate.toDateString()}`

      return queryString
    },
    /**
        * Mô tả: set trang hiện tại
        @param (trang dữ liệu)
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 20/04/2022
        */
    clickCallback(pageNum) {
      this.currentPage = pageNum;
      this.reloadData(this.currentPage)
    },

    /**
        * Mô tả: set tổng số trang, trang hiện tại, số lượng dữ liệu hiển thị
        @param (tổng số trang,trang hiện tại,số lượng dữ liệu hiển thị)
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 20/04/2022
        */
    loadHandle(Payments, currentPage, totalPage, count) {
      if (Payments.length > 0) {
        this.isNoData = false;
      } else this.isNoData = true;
      this.currentPage = currentPage;
      this.totalPage = totalPage;
      this.totalRecord = count;
      
    },

    //Xử lý khi bấm nút xuất dữ liệu
    handleExport() {
      //Lấy điều kiện lọc dữ liệu
      var queryString = this.getQueryString()
      var apiConnectionString = queryString !=="" ?`${Api.payments}/export?currentPage=${this.currentPage}&pageSize=${this.pageSize}&${queryString}`:
      `${Api.payments}/export?currentPage=${this.currentPage}&pageSize=${this.pageSize}`
      console.log(queryString)
      axios({
        url: apiConnectionString,
        method: "GET",
        responseType: "blob",
      })
        .then((res) => {
          

          //download file excel
          var a = window.document.createElement("a");

          a.href = window.URL.createObjectURL(
            new Blob([res.data], { type: "application/xlsx" })
          );

          a.download = `Danh_sach_phieu_chi.xlsx`;

          document.body.appendChild(a);
          a.click();
          document.body.removeChild(a);
        })
        .catch((res) => {
          console.log(res);
        });
    },

    /**
    * Mô tả:  Mô tả Xử lý scroll document
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 31/04/2022
    */
    handleScroll(e) {
      
      this.topTitle = (e.path[0].scrollTop) * -1;
    },

    /**
    * Mô tả: Xử lý check column table
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 31/04/2022
    */
    handleCheckDataTable(checkboxs) {
      
      this.PaymentIds = checkboxs;
      //Nếu danh sách check > 1 => mở button thực hiện hàng loạt
      if (this.PaymentIds.length > 1) {
        this.isDisableMultipleDeleteBtn = false;
      } else this.isDisableMultipleDeleteBtn = true;
    },

    //Xóa nhiều bản ghi
    handleConfirmBtn(confirm) {
      this.isShowPopupInfo = false;
      if (confirm) {
        let value = Object.values(this.PaymentIds);
        axios({
          url: `${Api.deleteMultiPayment}`,
          method: "DELETE",
          data: value,
        })
          .then((response) => {
            if (response.status == 200) {
              //Lấy kêt quả quá
              this.resultDeleteMulti = response.data
              var deleteMsg = this.resultDeleteMulti.DeleteMsg
              var errorIds = Object.keys(deleteMsg)

              //Thêm nguyên nhân lỗi cho các entity lỗi
              errorIds.forEach((x)=>{
                var entity = this.vendors.find(v => v.VendorId == x)
                entity.Reason = this.resultDeleteMulti.DeleteMsg[x]
                this.listEntity.push(entity)
              })
              this.isShowFormResultDeleteMulti = true
              // this.showToast(this.toastMsg.deleteVendorSuccessMsg, "success");
              this.PaymentIds = [];
              this.reloadData();
              
            }
          })
          .catch((e) => {
            try{
              this.showToast(
                e.response.data.data[Object.keys(e.response.data.data)[0]],
                "error"
              );
            }
            catch(e){
              this.showToast(
                "Có lỗi xảy ra.Liên hệ Misa để được hỗ trợ",
                "error"
              );
            }
          });
      }
    },
    
    /**
    * Mô tả: Xử lý sự kiện bâ,s thực hiện hàng loạt
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 25/04/2022
    */
    handleMultipleDeleteClick() {
      this.isShowOptionItem = !this.isShowOptionItem;
      if (this.PaymentIds.length > 1) {
        this.showPopupInfo(this.popupMsg.confirmMultipleDeleteMsg, this.typePopupName.warningConfirm);
      }
    },

    //Xử lý các key event
    handleKeyEvent(e) {
      // console.log(e)
      if (e.shiftKey && e.keyCode === 46) {
        this.handleMultipleDeleteClick();
      }
      if (e.keyCode === 112 && e.ctrlKey) {
        this.handleAddClick();
      }
    },
    //Xử lý sự kiện key down
    handleKeyEventDown(e) {
      if (e.shiftKey && e.keyCode === 46) {
        e.preventDefault();
      }
      if (e.keyCode === 112 && e.ctrlKey) {
        e.preventDefault();
      }
    },

    /**
    * Mô tả: Xử lý sự kiện khi đóng form chỉnh giao diện bảng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 09/05/2022
    */
    handleCloseSettingViewForm(isShow){
      this.isShowSettingViewForm = false
      if(isShow){
        this.$refs.tableVendor.loadColumnTable(1)
      }
    }
  },
};
</script>

<style scoped>
.router-content {
  padding-top: 48px;
  padding-left: 20px;
  background-color: #eceef1;
}
.content-area-content {
  height: 100vh;
  width: 100%;
  position: relative;
}
.layout-dictionary {
  position: relative;
  display: flex;
  flex-direction: column;
  height: calc(100vh - 48px - 88px);
}
.title-distance {
  padding: calc(var(--title-list-padding-top) + 10px) 39px var(--title-list-padding-top) 0;
  transition: top 0.25s;
  position: absolute;
  top: 0px;
  z-index: 7;
  width: 100%;
  align-items: center;
 
}
.header-di {
  display: flex;
  justify-content: space-between;
}
.title-di {
  font-size: 24px;
  font-weight: 700;
  color: #111;
}
.breadcrumb {
  display: flex;
  align-items: center;
  color: #0075c0;
  cursor: pointer;
}
.breadcrumb-icon {
  background-position: -224px -360px;
}
.btn-container {
  display: flex;
}
.btn-container .btn-box {
  margin-left: 10px;
}

.layout-dictionary-body {
  flex: 1;
  padding: calc(
      var(--title-list-padding-bottom) + var(--title-list-padding-bottom) +
        var(--rounded-button-height) + 140px
    )
    30px 0 0;
  overflow: auto;
  scroll-behavior: smooth;
  position: relative;
  max-width: 100%;
  min-height: 0;
}
.router-view{
    width: 100%;
    height: 100%;
    margin-right: -60px;
    margin-left:-60px
}
.router-content{
    padding-top: 0px;
    padding-left: 0px;
}
.content-area-content{
    height:unset
}
.table-option {
  padding: var(--horizontal-control-padding-y) 16px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: #fff;
  position: sticky;
  left: 0;
}
.table-option-left {
  position: relative;
  align-items: center;
}
.icon-table-option{
  background-position: -256px -143px;
  margin: 0 12px;
}
.table-option-right {
  padding: 5px;
}
.input-search {
  min-width: 240px;
}
.reload {
  background-position: -423px -201px;
}
.reload:hover {
  background-position: -1097px -88px;
}
.export {
  background-position: -704px -200px;
}
.export:hover {
  background-position: -704px -256px;
}
.setting {
  background-position: -88px -200px;
}
.setting:hover {
  background-position: -88px -256px;
}
.btn-icon {
  margin-left: 14px;
}
.router-pagination {
  position: sticky;
  bottom: 0;
  left: 0;
  z-index: 5;
  background-color: #fff;
}
.pagination {
  min-height: 58px;
}
.pagination-no-data {
  height: 200px;
}
.no-data-info span {
  display: block;
  text-align: center;
}
.left-pagination {
  height: 100%;
  padding-left: 24px;
}
.right-pagination {
  height: 100%;

  padding-right: 24px;
}
.page-container {
  display: flex;
  height: 100%;
  align-items: center;
}
.page-item {
  margin-right: 10px;
  margin-left: 20px !important;
  cursor: pointer;
}
.page-container .page-number-btn {
  margin-left: 10px;
  cursor: pointer;
}
.page-container .active {
  border: 1px solid #e0e0e0;
  font-weight: 700;
  height: 20px;
  width: 24px;
  display: flex;
  align-items: center;
  justify-content: center;
}
#toast {
  position: fixed;
  top: 100px;
  right: 50%;
  transform: translateX(50%);
  z-index: 1000;
}

.option-item-container {
  position: absolute;
  left: 0px;
  top: 130px;

  width: 100px;
  background-color: #fff;
  border: 1px solid black;
  z-index: 10000;
}
.m-table {
    width: calc(100% - 20px);
    height: 100%;
    position: relative;
    background-color: #fff;
    margin-right: 30px;
}

.m-table::-webkit-scrollbar {
    height: 10px;
}

.table-tool {
    padding: 16px;
    height: 68px;
    position: sticky;
    left: 0px;
    /*test*/
}

.table-tool .tool-search {
    max-width: 250px;
    margin-right: 10px;
}

.table-tool .m-right {
    display: flex;
    align-items: center;
}

.table-tool .m-right .ultility-button {
    display: flex;
    align-items: center;
}

.table-tool .m-right .ultility-button>div {
    margin: 0 6px;
    width: 24px;
    height: 24px;
    cursor: pointer;
}

.table-tool .m-right .ultility-button .refresh {
    background: url('@/assets/img/Sprites.64af8f61.svg') no-repeat;
    background-position: -423px -201px;
}

.table-tool .m-right .ultility-button .refresh:hover {
    background-position: -1097px -88px;
}

.table-tool .m-right .ultility-button .export {
    background: url('@/assets/img/Sprites.64af8f61.svg') no-repeat;
    background-position: -704px -200px;
}

.table-tool .m-right .ultility-button .export:hover {
    background-position: -704px -256px;
}

.table-tool .m-right .ultility-button .setting {
    background: url('@/assets/img/Sprites.64af8f61.svg') no-repeat;
    background-position: -88px -200px;
}

.table-tool .m-right .ultility-button .setting:hover {
    background-position: -88px -256px;
}


/*end table tool*/

.m-table .table-content {
    width: 100%;
    /* height: calc(100% - 68px); */
    /* overflow: scroll; */
    /* padding-bottom: 56px; */
}

.m-table .table-content::-webkit-scrollbar {
    position: absolute;
    width: 5px;
    height: 10px;
    background-color: #f1f1f1;
}

.m-table .table-content::-webkit-scrollbar-thumb {
    background-color: #b8bcc3;
}

.m-table .table-content::-webkit-scrollbar-thumb:hover {
    background-color: #a5a5a5;
}

.m-table .table-content::-webkit-scrollbar-track {
    background-color: #f1f1f1;
}

table {
    border-collapse: separate;
    border-spacing: 0;
    /* overflow: hidden; */
    width: 100%;
}

.table-content thead>tr>th {
    border-bottom: 1px solid #c7c7c7;
    margin: 0;
    /* min-width: 120px; */
    position: sticky;
    top: 0;
    font-size: 12px;
    text-transform: uppercase;
    font-style: bold;
    padding: 0 10px;
    text-align: left;
    border-right: 1px solid #c7c7c7;
}

.table-content thead>tr {
    margin: 0;
    height: 35px;
    z-index: 2;
    background-color: #eceef1;
}

.table-content tbody>tr td {
    border-right: 1px dotted #c7c7c7;
    padding: 0 10px;
}

.table-content tfoot>tr td {
    padding: 0 10px;
}

.table-content tbody>tr>td {
    border-bottom: 1px solid #c7c7c7;
    box-sizing: border-box;
    min-height: 44px;
    height: 44px;
}

.table-content tfoot>tr>td {
    box-sizing: border-box;
    min-height: 44px;
    height: 44px;
}

.table-content tbody>tr:last-child {
    border-bottom: 1px solid #c7c7c7;
}

.table-content thead th {
    border-right: 1px solid #c7c7c7;
    background-color: #eceef1;
    z-index: 2;
}

.table-content tbody td {
    background-color: #fff;
}

.table-content tfoot td {
    background-color: #eceef1;
    position: sticky;
    bottom: 0px;
}

.table-content tbody tr.selected>td {
    background-color: #e5f3ff;
}

.table-content tbody tr.checked>td {
    background-color: #e5f3ff;
}

.table-content tbody tr:hover>td {
    background-color: #f2f9ff;
}

.table-content thead th.table-checkbox {
    position: sticky;
    left: 16px;
    z-index: 3;
    max-width: 40px;
    min-width: 40px;
    box-sizing: border-box;
}

.table-content thead th.table-function {
    border-right: none;
    position: sticky;
    right: 40px;
    z-index: 3;
    width: 120px;
    text-align: center;
    border-left: 1px solid #c7c7c7;
}

.table-content tbody td.table-function {
    border-right: none;
    position: sticky;
    right: 40px;
    z-index: 2;
    border-left: 1px dotted #c7c7c7;
    /* min-width: 120px; */
    text-align: center;
}

.table-content tfoot td.table-function {
    border-right: none;
    position: sticky;
    right: 40px;
    z-index: 2;
    text-align: center;
    background-color: #eceef1;
}

.table-content tbody td.table-checkbox {
    position: sticky;
    left: 16px;
    z-index: 2;
    max-width: 40px;
    min-width: 40px;
    box-sizing: border-box;
}

.table-content tfoot td.table-checkbox {
    position: sticky;
    left: 16px;
    z-index: 2;
    max-width: 40px;
    min-width: 40px;
    box-sizing: border-box;
    background-color: #eceef1;
}

.table-content .hidden-left {
    position: sticky;
    left: 0px;
    z-index: 4;
    max-width: 16px;
    min-width: 16px;
    border: none;
    padding: 0;
    background-color: #fff;
    pointer-events: none;
}

.table-content .hidden-right {
    pointer-events: none;
    position: sticky;
    right: 0px;
    padding: 0;
    z-index: 4;
    max-width: 20px;
    min-width: 20px;
    border: none;
    background-color: #fff;
}


/* table function*/

.table-function .table-function-button {
    /* height: 36px; */
    display: flex;
    align-items: center;
    justify-content: center;
}

.table-function .table-function-button .button-edit {
    color: #0075c0;
    margin-left: 10px;
    font-weight: 600;
}

.table-function .table-function-button .arrow-down-button {
    /* height: 16px; */
    padding: 0px 5px;
    margin: 0px 9px;
    border: 1px solid transparent;
}

.table-function .table-function-button .arrow-down-button .icon {
    background: url('@/assets/img/Sprites.64af8f61.svg') no-repeat;
    background-position: -896px -359px;
    width: 16px;
    height: 16px;
}

.table-function .table-function-button .arrow-down-button:hover {
    border: 1px solid #0075c0;
}

.table-function .table-function-button .button-edit:hover {
    cursor: pointer;
}

.table-function .table-function-button .arrow-down-button:hover {
    cursor: pointer;
}


/*table footer , paging*/

.table-footer {
    position: sticky;
    bottom: 0px;
    height: 56px;
    width: 100%;
    z-index: 5;
    padding-right: 5px;
    left: 0;
    top: calc(100% - 56px);
}

.table-footer .footer {
    background-color: #fff;
    height: 100%;
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 16px;
}

.table-footer .footer .count {
    font-weight: 600;
}

.table-footer .footer .paging {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.table-footer .footer .paging .select-max-row {
    min-width: 200px;
}

.table-footer .footer .count-row {
    display: flex;
    align-items: center;
}

.table-footer .footer .paging-index {
    width: 20px;
    height: 20px;
    display: flex;
    align-items: center;
    width: 100%;
    margin-left: 5px;
    cursor: pointer;
}

.table-footer .footer .paging-index .index {
    height: 20px;
    margin-left: 5px;
    display: flex;
    align-items: center;
}

.table-footer .footer .paging-index .index .number-index {
    height: 20px;
    margin-left: 5px;
    display: flex;
    align-items: center;
    padding: 0 6.5px;
}

.table-footer .footer .paging-index .index .number-index.selected {
    border: 1px solid #00000021;
}

.table-footer .footer .paging-index .next {
    height: 20px;
    margin-left: 5px;
}

.table-footer .footer .paging-index .prev {
    height: 20px;
    margin-left: 5px;
}
.counter {
  padding: calc(var(--title-list-padding-top) + 65px) 30px var(--title-list-padding-top) 0;
  transition: top 0.25s;
  position: absolute;
  top: 0px;
  z-index: 5;
  width: 100%;
  align-items: center;
}
.counter .row{
  margin-right: 0!important;
}
.view-total{
  min-height: 64px;
  display: flex;
  justify-content: center;
  flex-direction: column;
  padding: 0 0 0 12px;
}
.total-receipt{
  background: #ff7f2c;
  color: #fff;
  padding-left: 20px;
  padding: 12px 30px 6px 12px;
}
.total-payment{
  background: #00a9f2;
  color: #fff;
  padding: 12px 30px 6px 12px;
}
.total-count{
  background: #74cb2f;
  padding: 12px 30px 6px 12px;
}
.total-count .total{
  color: #ff7f2c;
}
.total-count .text{
  color: #fff;
}
.total{
  font-size: 24px;
  margin-bottom: 5px;
}
.text{
  font-size:14px;
}
.tab-table-container{
  display: flex;
  padding: calc(var(--title-list-padding-top) + 153px) 30px var(--title-list-padding-top) 0;
  transition: top 0.25s;
  position: absolute;
  top: 0px;
  z-index: 5;
  width: 100%;
  align-items: center;
  padding-left: 15px;

}
.tab-item + .tab-item{
  margin-left: 30px;
}

.btn-filter{
  margin-left: 10px;
  position: relative;
}
.filter-option-container{
  
  height: auto;
  position:fixed;
  
  z-index: 30;
}
.filter-option-container label{
  font-weight: 700;
  color: #111;
  margin-bottom: 5px;
  display: block;
}
.filter-option{
  border: 1px solid #c7c7c7;
  background-color: #fff;
  padding: 20px;
  width: 456px;
  padding-top: 0px;
}
.filter-option .row{
  margin-top: 20px;
}
.btn-filter-form{
  display: flex;
  justify-content: right;
}

.m-tfoot tr{
  height: 44px;
  position: sticky;
  bottom: 58px;
  left: 0;
  z-index: 5;
}
.m-tfoot .padding-td{
  background-color: #fff !important;
  position: sticky;
  z-index: 4;
  border: none;
  left: 0;
  bottom:58px;
  min-width: unset;
  
}
.m-tfoot .checkbox-column{
  position: sticky;
  bottom:58px;
  left: 16px;
  width: 41px;
}

.m-tfoot td{
  background-color: #e5e8ec;
}
.m-tfoot .option-column-td{
  border-right: none;
  min-width: 120px;
  
}
.m-tfoot .padding-right-td{
  background-color: #fff !important;
  position: sticky;
  z-index: 10;
  border: none;
  right: 0;
  min-width: unset;
}
.m-tfoot .hidden-td{
  min-width: unset;
    position: sticky;
    position: -webkit-sticky;
    right: -30px;
    background-color: #eceef1 !important;
    border: none;
    padding: 0 15px;
}
.m-tfoot span{
  font-weight: 700;
  color: #111;
}
.m-tfoot tr td:nth-child(3){
  position: sticky;
  left: 57px;
}
</style>
