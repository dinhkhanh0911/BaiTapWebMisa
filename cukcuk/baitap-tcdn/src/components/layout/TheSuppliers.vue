<template>
  <div class="router-view">
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
                  :classBtn="'btn-default btn-sq btn-no-op btn-primary'"
                  :content="'Thêm mới nhân viên'"
                  @click="handleAddClick()"
                />
              </div>
            </div>
            <!-- <div class="breadcrumb">
                            <div class="breadcrumb-icon mi mi-16"></div>
                            <span> Tất cả danh mục</span>
                        </div> -->
          </div>
          <div class="layout-dictionary-body" @scroll="handleScroll($event)">
            <div class="table-option">
              <div class="table-option-left">
                <BaseButton
                  :classBtn="'btn-default'"
                  :content="'Thực hiện hàng loạt'"
                  :disabled="isDisableMultipleDeleteBtn"
                  @click="handleMultipleDeleteClick()"
                />
              </div>

              <div class="table-option-right d-flex alignt-center">
                <div class="input-search">
                  <BaseInput
                    :placeholder="'Tìm kiếm theo mã,tên nhân viên'"
                    v-model="searchValue"
                    :timer="1000"
                    :searchClass="'search-container'"
                  />
                </div>
                <div
                  class="reload btn-icon mi mi-24"
                  @click="reloadData()"
                  @mouseover="hover($event, 'Lấy lại dữ liệu')"
                  @mouseleave="isShowTooltip = false"
                ></div>
                <div
                  class="export btn-icon mi mi-24"
                  @mouseover="hover($event, 'Xuất ra excel')"
                  @mouseleave="isShowTooltip = false"
                  @click="handleExport()"
                ></div>
                <div class="setting btn-icon mi mi-24 "
                    @mouseover="hover($event,'Tùy chỉnh giao diện')" 
                    @mouseleave="isShowTooltip = false"
                    @click="isShowSettingViewForm = true"
                ></div>
              </div>
            </div>
            <BaseTable
              ref="tableEmployee"
              @employeeValue="handleEdit"
              :searchValue="searchValue"
              :currentPage="currentPage"
              :pageSize="pageSize"
              :loadHandle="loadHandle"
              @showToast="showToast"
              @loading="isNoData = false"
              @check="handleCheckDataTable"
              :api="apiColumn"
              tableName="Employee"
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
      v-if="true"
      @showPopup="togglePopupEmployee"
      :modelPopup="employee"
      @showToast="showToast"
      @addSuccess="reloadData"
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
      tableName="Employee"
      v-if="isShowSettingViewForm"
      @close="handleCloseSettingViewForm"
    />
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
import axios from "axios";
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
    BaseSettingViewForm
  },
  data() {
    return {
      isNoData: false,
      employeeIds: [],
      isDisableMultipleDeleteBtn: true,
      isShowOptionItem: false,
      //tooltip
      topTooltip: 0,
      leftTooltip: 0,
      isShowTooltip: false,
      valueTooltip: "",
      isShowPopupEmployee: false,
      employee: {},

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
      valueOption: [
        { Id: 10, Value: "10 bản ghi trên 1 trang" },
        { Id: 20, Value: "20 bản ghi trên 1 trang" },
        { Id: 30, Value: "30 bản ghi trên 1 trang" },
        { Id: 50, Value: "50 bản ghi trên 1 trang" },
        { Id: 100, Value: "100 bản ghi trên 1 trang" },
      ],

      apiColumn:`${Api.getColumnOption}`,
      isShowSettingViewForm:false,
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
    //Hiển thị toast message
    showPopupInfo(message = "Thành công", type = "success") {
      this.contentPopupInfo = message;
      this.typePopupInfo = type;
      this.isShowPopupInfo = true;
    },
    /*
            Hiểm thị popup thêm nhân viên
        */
    togglePopupEmployee(value) {
      this.isShowPopupEmployee = value;
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
      this.employee = {};
      this.togglePopupEmployee(true);
    },
    /*
            Xử lý sự kiện khi bấm nút sửa nhân viên
        */
    handleEdit(employee) {
      this.employee = employee;
      this.togglePopupEmployee(true);
    },

    /*
            Lấy dữ liệu 
        */
    reloadData() {
      // this.currentPage = 1
      this.$refs.tableEmployee.loadAllData(1);
      
    },

    /**
        * Mô tả:  Xử lý khi hover vào element
        @param(event,Nội dung tooltip)
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 18/04/2022
        */
    hover(e, value) {
      this.topTooltip = e.clientY + 10;
      this.leftTooltip = e.clientX + 10;
      this.isShowTooltip = true;
      this.valueTooltip = value;
    },

    /**
        * Mô tả: set trang hiện tại
        @param (trang dữ liệu)
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 20/04/2022
        */
    clickCallback(pageNum) {
      this.currentPage = pageNum;
    },

    /**
        * Mô tả: set tổng số trang, trang hiện tại, số lượng dữ liệu hiển thị
        @param (tổng số trang,trang hiện tại,số lượng dữ liệu hiển thị)
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 20/04/2022
        */
    loadHandle(employees, currentPage, totalPage, count) {
      if (employees.length > 0) {
        this.isNoData = false;
      } else this.isNoData = true;
      this.currentPage = currentPage;
      this.totalPage = totalPage;
      this.totalRecord = count;
      
    },

    //Xử lý khi bấm nút xuất dữ liệu
    handleExport() {
      var apiConnectionString = `${Api.exportEmployee}?currentPage=${this.currentPage}&pageSize=${this.pageSize}`;
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

          a.download = `Danh_sach_nhan_vien.xlsx`;

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
    handleCheckDataTable(checkboxs) {
      
      this.employeeIds = checkboxs;
      if (this.employeeIds.length > 0) {
        this.isDisableMultipleDeleteBtn = false;
      } else this.isDisableMultipleDeleteBtn = true;
    },

    //Xác nhận confirm
    handleConfirmBtn(confirm) {
      this.isShowPopupInfo = false;
      if (confirm) {
        let value = Object.values(this.employeeIds);
        axios({
          url: `${Api.deleteMultiEmployee}`,
          method: "DELETE",
          data: value,
        })
          .then((response) => {
            if (response.status == 200) {
              this.reloadData();
              this.showToast(this.toastMsg.deleteEmpSuccessMsg, "success");
              this.employeeIds = [];
              
            }
          })
          .catch((e) => {
            
            this.showToast(
              e.response.data.data[Object.keys(e.response.data.data)[0]],
              "error"
            );
          });
      }
    },
    //Xử lý xóa nhiều nhân viên
    handleMultipleDeleteClick() {
      this.isShowOptionItem = !this.isShowOptionItem;
      if (this.employeeIds.length > 0) {
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
        this.$refs.tableEmployee.loadColumnTable(1)
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
  z-index: 5;
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
</style>
