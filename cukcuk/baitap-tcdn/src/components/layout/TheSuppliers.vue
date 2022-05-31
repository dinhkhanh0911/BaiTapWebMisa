<template>
  <div class="router-view vendor">
    <div class="router-content">
      <div class="content-area-content">
        <div class="layout-dictionary">
          <div class="title-distance" :style="{ top: topTitle + 'px' }">
            <div class="header-di">
              <div class="title-di d-flex alignt-center">
                <span>Danh sách nhà cung cấp</span>
              </div>
              <div class="btn-container">
                <BaseButton
                  :addButton="'addButton'"
                  :classBtn="'btn-default btn-primary'"
                  :content="'Thêm mới nhà cung cấp'"
                  @click="handleAddClick()"
                  :title="'Ctr+F1 '"
                />
              </div>
            </div>
            <!-- <div class="breadcrumb">
                            <div class="breadcrumb-icon mi mi-16"></div>
                            <span> Tất cả danh mục</span>
                        </div> -->
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
                    ref="deleteMulti"
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
                  :title="'Lấy lại giữ liệu'"
                ></div>
                <div
                  class="export btn-icon mi mi-24"
                  :title="'Xuất dữ liệu'"
                  @click="handleExport()"
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
              @showToast="showToast"
              @showPopup="showPopupInfo"
              @loading="isNoData = false"
              @check="handleCheckDataTable"
              :api="apiColumn"
              tableName="Vendor"
              entityId="VendorId"
              entityCode="VendorCode"
              entityName="VendorName"
              :apiFilter="filterVendorApi"
              :apiDelelte="deleteVendorApi"
            />
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
    <TheSuppliersPopup
      v-if="isShowPopupSuppliers"
      @showPopup="togglePopupSuppliers"
      :modelPopup="Vendor"
      @showToast="showToast"
      @addSuccess="reloadData(currentPage)"
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
      tableName="Vendor"
      v-if="isShowSettingViewForm"
      @close="handleCloseSettingViewForm"
    />
    <BaseFormResultDeleteMulti 
      v-if="isShowFormResultDeleteMulti" 
      :listEntity="listEntity"
      :result="resultDeleteMulti"
      :apiColumn="apiColumn"
      :tableName="'VendorResult'"
      :description="'nhà cung cấp'"
      @close="handleCloseFormResult"
    />
    <div class="filter-option-container" v-if="isShowFilter" v-bind:style="{ top: filterBtnTop + 'px', left: filterBtnLeft + 'px' }">
      <div class="filter-option grid">
          <!-- Tổ chức -->
        <div class="primary-input">
          <div class="row">
            <div class="col c-6">
              <div class="form-group">
                <label for=""
                  >Loại</label
                >
                <BaseCombobox
                  :valueOption="filterType"
                  :id="'Id'"
                  :name="'Value'"
                  :code="'Id'"
                  :isOneColumn="'true'"
                  v-model="filterObject.FilterTypeId"
                />
              </div>
            </div>
            <div class="col c-6">
              <div class="form-group">
                <label for=""
                  >Trình trạng công nợ</label
                >
                <BaseCombobox
                  :valueOption="filterDebt"
                  :id="'Id'"
                  :name="'Value'"
                  :code="'Id'"
                  :isOneColumn="'true'"
                  v-model="filterObject.FilterDebtId"
                />
              </div>
            </div>
            <!-- <div class="col c-6">
              <div class="form-group">
                <label for=""
                  >Nhóm nhà cung cấp</label
                >
                <BaseCombobox
                  :Api="apiVendorGroup"
                  :id="'VendorGroupId'"
                  :name="'VendorGroupName'"
                  :code="'VendorGroupCode'"
                  :isOneColumn="'true'"
                  v-model="filterObject.VendorGroupId"
                />
              </div>
            </div> -->
          </div>
          <div class="row">
            
            <div class="col c-6">
              <div class="form-group">
                <label for=""
                  >Trạng thái</label
                >
                <BaseCombobox
                  :valueOption="filterStatus"
                  :id="'Id'"
                  :name="'Value'"
                  :code="'Id'"
                  :isOneColumn="'true'"
                  v-model="filterObject.FilterStatusId"
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
              <div class="btn-filter">
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
import TheSuppliersPopup from "@/components/layout/TheSuppliersPopup.vue";
import BaseTooltip from "@/components/base/BaseTooltip.vue";
import Paginate from "vuejs-paginate-next";
import BaseToast from "@/components/base/BaseToast.vue";
import BasePopup from "@/components/base/BasePopup.vue";
import BaseCombobox from "@/components/base/BaseCombobox.vue";
import BaseSettingViewForm from '@/components/base/BaseSettingViewForm.vue'
import Api from "@/assets/js/api";
import DB from "@/assets/js/hashDatabase"
import axios from "axios";
import BaseFormResultDeleteMulti from "../base/BaseFormResultDeleteMulti.vue";
export default {
  components: {
    BaseButton,
    BaseTable,
    BaseInput,
    TheSuppliersPopup,
    BaseTooltip,
    Paginate,
    BaseToast,
    BasePopup,
    BaseCombobox,
    BaseSettingViewForm,
    BaseFormResultDeleteMulti
},
  data() {
    return {
      filterVendorApi : Api.filterVendor,
      deleteVendorApi : Api.deleteVendor,
      isNoData: false,
      VendorIds: [],
      vendors:[],
      isDisableMultipleDeleteBtn: true,
      isShowOptionItem: false,
      //tooltip
      topTooltip: 0,
      leftTooltip: 0,
      isShowTooltip: false,
      valueTooltip: "",
      isShowPopupSuppliers: false,
      Vendor: {},

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

      apiColumn:`${Api.getColumnOption}`,
      apiVendorGroup:Api.getVendorGroup,
      isShowSettingViewForm:false,

      //Filter
      filterBtnLeft:0,
      filterBtnTop:0,
      isShowFilter:false,
      filterObject:{
        FilterTypeId:3,
        FilterDebtId:2,
        FilterStatusId:2,
      },
      filterType:DB.filterType,
      filterDebt:DB.filterDebt,
      filterStatus:DB.filterStatus,

      isShowFormResultDeleteMulti:false,
      listEntity:[],
      resultDeleteMulti:{}
    };

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
    * Mô tả: Đóng form kết quả xóa nhiều
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 28/05/2022
    */
    handleCloseFormResult(){
      this.isShowFormResultDeleteMulti = false,
      this.listEntity = []
      this.resultDeleteMulti = {}
    },
    /**
    * Mô tả: Chỉnh mặc định các trường lọc
    @param
    *@return
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 20/05/2022
    */
    setDefaultFilter(){
      this.filterObject.FilterTypeId = 3
      this.filterObject.FilterDebtId = 2
      this.filterObject.FilterStatusId = 2
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
      console.log("a")
    },
    /*
            Hiểm thị popup thêm nhân viên
        */
    togglePopupSuppliers(value) {
      this.isShowPopupSuppliers = value;
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
      this.Vendor = {};
      this.togglePopupSuppliers(true);
    },
    /*
            Xử lý sự kiện khi bấm nút sửa nhân viên
        */
    handleEdit(Vendor) {
      this.Vendor = Vendor;
      this.togglePopupSuppliers(true);
    },

    /**
    * Mô tả: Lấy lại dữ liệu
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 31/05/2022
    */
    reloadData(currentPage = 1) {
      this.isShowFilter = false;
      var queryString = this.getQueryString()

      
      // if(!!this.filterObject.VendorGroupId){
      //   queryString += `&vendorGroupId=${this.filterObject.VendorGroupId}`
      // }
      console.log(queryString)
      this.currentPage = 1
      console.log(queryString)
      this.$refs.tableVendor.loadAllData(currentPage,queryString);
      
    },
    /**
    * Mô tả: Lấy query string filter
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 31/05/2022
    */
    getQueryString(){
      var queryString = ""
      //Lọc nâng cao
      //Loại nhà cung cấp
      if(this.filterObject.FilterTypeId == 1){
        queryString += `&vendorType=${this.filterObject.FilterTypeId}`
      }
      if(this.filterObject.FilterTypeId == 2){
        queryString += `&vendorType=${this.filterObject.FilterTypeId}`
      }
      //Tình trạng công nợ
      if(this.filterObject.FilterDebtId ==3 ){
        queryString += `&isOwed=${true}`
      }
      if(this.filterObject.FilterDebtId ==1 ){
        queryString += `&isOwed=${false}`
      }
      //Trạng thái
      if(this.filterObject.FilterStatusId == 3){
        queryString += `&isUsed=${true}`
      }
      if(this.filterObject.FilterStatusId == 1){
        queryString += `&isUsed=${false}`
      }

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
    loadHandle(Vendors, currentPage, totalPage, count) {
      if (Vendors.length > 0) {
        this.isNoData = false;
      } else this.isNoData = true;
      this.currentPage = currentPage;
      this.totalPage = totalPage;
      this.totalRecord = count;
      
    },

    //Xử lý khi bấm nút xuất dữ liệu
    handleExport() {
      var queryString = this.getQueryString()
      var apiConnectionString = queryString !=="" ?`${Api.vendors}/export?currentPage=${this.currentPage}&pageSize=${this.pageSize}&${queryString}`:
      `${Api.vendors}/export?currentPage=${this.currentPage}&pageSize=${this.pageSize}`
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

          a.download = `Danh_sach_nha_cung_cap.xlsx`;

          document.body.appendChild(a);
          a.click();
          document.body.removeChild(a);
        })
        .catch((res) => {
          console.log(res);
        });
    },

    /**
     * Xử lý sự kiện cuộn chuột
     */
    handleScroll(e) {
      this.topTitle = e.path[0].scrollTop * -1;
    },

    //Xử lý chọn nhân viên
    handleCheckDataTable(checkboxs,dataTable) {
      this.vendors = dataTable
      this.VendorIds = checkboxs;
      if (this.VendorIds.length > 1) {
        this.isDisableMultipleDeleteBtn = false;
      } else this.isDisableMultipleDeleteBtn = true;
    },

    //Xác nhận confirm
    handleConfirmBtn(confirm) {
      this.isShowPopupInfo = false;
      if (confirm) {
        let value = Object.values(this.VendorIds);
        console.log(value)
        axios({
          url: `${Api.deleteMultiVendor}`,
          method: "DELETE",
          data: value,
        })
          .then((response) => {
            console.log(response)
            if (response.status == 200) {
              
              this.resultDeleteMulti = response.data
              var deleteMsg = this.resultDeleteMulti.DeleteMsg
              var errorIds = Object.keys(deleteMsg)
              errorIds.forEach((x)=>{
                var entity = this.vendors.find(v => v.VendorId == x)
                entity.Reason = this.resultDeleteMulti.DeleteMsg[x]
                this.listEntity.push(entity)
              })
              this.isShowFormResultDeleteMulti = true
              // this.showToast(this.toastMsg.deleteVendorSuccessMsg, "success");
              this.VendorIds = [];
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
    //Xử lý xóa nhiều bản ghi
    handleMultipleDeleteClick() {
      this.isShowOptionItem = !this.isShowOptionItem;
      if (this.VendorIds.length > 1) {
        this.showPopupInfo(this.popupMsg.confirmMultipleDeleteMsg, this.typePopupName.warningConfirm);
      }
    },

    //Xử lý các key event
    handleKeyEvent(e) {
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
  height: calc(100vh - 48px);
}
.title-distance {
  padding: var(--title-list-padding-top) 39px var(--title-list-padding-top) 0;
  transition: top 0.25s;
  position: absolute;
  top: 0px;
  z-index: 6;
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
      calc(var(--title-list-padding-bottom) + 85px) + var(--title-list-padding-bottom) +
        var(--rounded-button-height) + 3px
    )
    30px 0 0;
  overflow: auto;
  scroll-behavior: smooth;
  position: relative;
  max-width: 100%;
  min-height: 0;
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
  width: 533px;
  padding-top: 0px;
}
.filter-option .row{
  margin-top: 20px;
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

.btn-filter{
  display: flex;
  justify-content: right;
  
}

/**
counter
*/
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
</style>
<style>
.vendor .sticky-top--87{
  top:-175px
}
</style>
