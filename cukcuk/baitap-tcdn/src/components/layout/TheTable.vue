<template>
  <div class="table-container">
    <table id="table " class="tableEmployee">
      <thead>
        <tr class="sticky-top--87">
          <th class="padding-td"></th>
          <th
            class="sticky-left-30 sticky-top--87 d-flex justify-space-center alignt-center checkbox-column"
          >
            <div class="checkbox-container">
              <input type="checkbox" class="checkbox" v-model="selectAll" />

              <span
                class="checkbox-border"
                :class="{ 'checkbox-border-active': selectAll }"
              >
                <span
                  class="mi mi-16"
                  :class="{ 'checkbox-active': selectAll }"
                ></span>
              </span>
            </div>
            <!-- <Checkbox v-model="selectAll"/> -->
          </th>
          <th>Mã nhân viên</th>
          <th class="overflow-content">
            <div>Tên nhân viên</div>
          </th>
          <th>Giới tính</th>
          <th class="text-center">Ngày sinh</th>
          <th :title="'Số chứng minh nhân dân'">Số CMND</th>

          <th>Chức danh</th>
          <th class="td-min-width">Tên đơn vị</th>
          <th class="td-min-width">Số tài khoản</th>
          <th>Tên ngân hàng</th>
          <th class="border-right-none">Chi nhánh tk ngân hàng</th>
          <th
            class="option-column-td sticky-right-30 justify-space-center alignt-center text-center border-right-solid"
          >
            Chức năng
          </th>
          <th class="padding-td padding-right-td"></th>
          <th class="hidden-td"></th>
        </tr>
      </thead>
      <tbody class="m-tbody loading-body" v-if="isLoading">
        <tr v-for="index in 13" :key="index">
          <td class="padding-td"></td>
          <td class="sticky-left-30 bg-while checkbox-column checkbox-column">
            <div class="checkbox-container">
              <input
                type="checkbox"
                class="checkbox"
                v-model="checkboxs"
                :value="employee.EmployeeId"
                @dblclick="onDbClick($event)"
              />

              <span
                class="checkbox-border"
                :class="{
                  'checkbox-border-active': checkboxs.includes(
                    employee.EmployeeId
                  ),
                }"
              >
                <span
                  class="mi mi-16"
                  :class="{
                    'checkbox-active': checkboxs.includes(employee.EmployeeId),
                  }"
                ></span>
              </span>
            </div>
          </td>
          <td><div class="loading-1"></div></td>
          <td><div class="loading-1"></div></td>
          <td><div class="loading-1"></div></td>
          <td class="text-center"><div class="loading-1"></div></td>
          <td><div class="loading-1"></div></td>
          <td><div class="loading-1"></div></td>
          <td class="overflow-content">
            <div class="loading-1"></div>
          </td>

          <td><div class="loading-1"></div></td>
          <td class="overflow-content">
            <div class="loading-1"></div>
          </td>
          <td class="border-right-none"><div class="loading-1"></div></td>
          <td
            class="option-column-td sticky-right-30 bg-while d-flex justify-space-center alignt-center"
          >
            <div class="loading-1"></div>
          </td>
          <td class="padding-td padding-right-td"></td>
          <td class="hidden-td"></td>
        </tr>
      </tbody>
      <tbody v-else class="bg-while">
        <tr
          v-for="(employee, index) in employees"
          :key="index"
          @click="handleClickColumn(index, employee)"
          @dblclick="handleEdit(employee)"
          :class="{ active: checkboxs.includes(employee.EmployeeId) }"
        >
          <td class="padding-td"></td>
          <td class="sticky-left-30 bg-while checkbox-column checkbox-column">
            <div class="checkbox-container">
              <input
                type="checkbox"
                class="checkbox"
                v-model="checkboxs"
                :value="employee.EmployeeId"
                @dblclick="onDbClick($event)"
              />

              <span
                class="checkbox-border"
                :class="{
                  'checkbox-border-active': checkboxs.includes(
                    employee.EmployeeId
                  ),
                }"
              >
                <span
                  class="mi mi-16"
                  :class="{
                    'checkbox-active': checkboxs.includes(employee.EmployeeId),
                  }"
                ></span>
              </span>
            </div>
          </td>
          <td>{{ employee.EmployeeCode }}</td>
          <td>{{ employee.FullName }}</td>
          <td>{{ employee.GenderName }}</td>
          <td class="text-center">{{ formatDate(employee.DateOfBirth) }}</td>
          <td>{{ employee.IdentityNumber }}</td>
          <td>{{ employee.PositionName }}</td>
          <td class="overflow-content">
            <div>{{ employee.DepartmentName }}</div>
          </td>

          <td>{{ employee.BankAccount }}</td>
          <td class="overflow-content">
            <div>{{ employee.BankName }}</div>
          </td>
          <td class="border-right-none">{{ employee.BankBranch }}</td>
          <td
            class="option-column-td sticky-right-30 bg-while d-flex justify-space-center alignt-center"
          >
            <div class="option-column d-flex alignt-center">
              <div class="edit-btn" @click="handleEdit(employee)">Sửa</div>
              <div class="option-btn d-flex justify-space-center">
                <div
                  class="option-column-icon mi mi-16 relative"
                  @click="handleOptionItemClick($event, employee, index)"
                  @dblclick="onDbClick($event)"
                ></div>
              </div>
            </div>
          </td>
          <td class="padding-td padding-right-td"></td>
          <td class="hidden-td"></td>
        </tr>
      </tbody>
    </table>
    <ul
      class="option-item-container"
      v-if="isShowOptionItem"
      v-bind:style="{ top: top + 'px', left: left + 'px' }"
    >
      <li @click="handleDelelte()">Xóa</li>
      <li @click="handleDuplicate()">Nhân bản</li>
    </ul>

    <BasePopup
      v-if="isShowPopupInfo"
      :contentPrev="contentPopupInfo"
      @confirm="handleConfirmBtn"
      :type="typePopupInfo"
    />
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
import Api from "@/assets/js/api.js";
export default {
  components: {
    BasePopup,
  },
  data() {
    return {
      employees: [],
      top: 0,
      left: 0,
      isShowOptionItem: false,
      employee: {},
      isLoading: false,
      //Popup
      contentPopupInfo: "",
      isShowPopupInfo: false,
      typePopupInfo: "error",

      //checkbox
      checkboxs: [],
      checked: false,

      //option
      indexOption: -1,
    };
  },
  props: {
    searchValue: {
      type: String,
      default: "",
    },
    loadHandle: {
      type: Function,
      default: () => {},
    },
    currentPage: {
      type: Number,
      default: 1,
    },
    pageSize: {
      type: Number,
      default: 20,
    },
  },
  created() {
    this.loadAllData();
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
  watch: {
    //Lắng nghe sự kiện phân trang
    currentPage: function () {
      this.loadAllData();
    },
    //Lắng nghe input tìm kiếm có thay đổi sẽ tìm kiếm
    //Khi input rỗng sẽ không tìm kiếm
    searchValue: function () {
      this.loadAllData(1);
    },

    //lắng nghe sự kiện chỉnh sửa số lượng bản ghi một trang
    pageSize: function () {
      this.loadAllData(1);
    },

    // Lắng nghe sự kiện checkbox
    checkboxs: function () {
      this.$emit("check", this.checkboxs);
    },
  },
  methods: {
    /**
     * Mô tả: Xử lý sự kiện click document
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleKeyEventClick(e) {
      this.isShowOptionItem = false;
      this.indexOption = -1;
    },

    /**
     * Mô tả: Hiển thị thông báo
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    showPopupInfo(message = "Thành công", type = "success") {
      this.contentPopupInfo = message;
      this.typePopupInfo = type;
      this.isShowPopupInfo = true;
    },

    //Xử lý sự kiện click popup thông báo
    handleConfirmBtn(isConfirm) {
      if (isConfirm) {
        this.delete();
      }
      this.isShowPopupInfo = false;
    },

    /* Xử lý dữ liệu ngày tháng 
            @param: date time
            @return
                date dạng dd/MM/yyyy
            @return 
        */

    formatDate(dateValue) {
      var date = new Date(dateValue);
      var year = date.getFullYear();
      var month = date.getMonth() + 1;
      var day = date.getDate();
      var monthConvert = month > 9 ? month.toString() : `0${month.toString()}`;
      if (year == 1970 && month == 1 && day == 1) return "";
      return day > 9
        ? `${day}/${monthConvert}/${year}`
        : `0${day}/${monthConvert}/${year}`;
    },

    /**
     * Xử lý sự kiện click sửa
     * @param  nhân viên
     */
    handleEdit(employee) {
      this.$emit("employeeValue", Object.assign({}, employee));
    },

    /**
     * Lấy dữ liệu nhân viên
     */
    loadAllData(currentPage = this.currentPage) {
      console.log(currentPage);
      //Bật hiệu ứng loading
      this.isLoading = true;
      this.$emit("loading", true);
      this.checkboxs = [];
      this.employees = [];
      var apiConnectionString;
      if (this.searchValue.trim() == "") {
        apiConnectionString = `${Api.filterEmployee}?currentPage=${currentPage}&pageSize=${this.pageSize}`;
      } else
        apiConnectionString = `${Api.filterEmployee}?filterText=${this.searchValue}&currentPage=${currentPage}&pageSize=${this.pageSize}`;
      axios
        .get(apiConnectionString)
        .then((response) => {
          if (response.status === 200) {
            this.isLoading = false;
            this.employees = response.data.employees;
            this.loadHandle(
              response.data.employees,
              currentPage,
              response.data.totalPage,
              response.data.count
            );
          }
        })
        .catch((e) => {
          console.log("e");
          this.isLoading = false;
          this.loadHandle([]);
        });
    },

    /**
     * Xử lý khi click vào hàng dữ liệu
     * Khi chọn set this.employee = nhân viên hàng đó
     * @param chỉ mục hàng, nhân viên hàng đó
     */
    handleClickColumn(index, employee) {
      // var trElement = document.querySelectorAll('.tableEmployee tbody tr')
      // for(var tr of trElement){
      //     tr.classList.remove("active");
      // }
      // trElement[index].classList.add("active")
      this.employee = employee;
    },

    /**
     * Xử lý khi click vào nút option
     */
    handleOptionItemClick(e, employee, index) {
      e.stopPropagation();
      this.employee = employee;
      //Mở option
      if (index != this.indexOption) {
        this.isShowOptionItem = true;
        this.top = e.clientY + 20;
        this.left = e.clientX - 81;
        this.indexOption = index;
      }
      //Tắt option
      else {
        this.isShowOptionItem = false;
        this.indexOption = -1;
      }
    },

    //Xử lý xóa nhân viên
    handleDelelte() {
      this.showPopupInfo(
        `${this.popupMsg.confirmDeleteEmpMsg(this.employee.EmployeeCode)}`,
        this.typePopupName.warningConfirm
      );
    },
    /**
     * Xóa nhân viên
     */
    delete() {
      axios
        .delete(`${Api.deleteEmployee}/${this.employee.EmployeeId}`)
        .then((response) => {
          if (response.status === 200) {
            this.loadAllData(1);
            this.$emit(
              "showToast",
              this.toastMsg.deleteEmpSuccessMsg,
              "success"
            );
          }
        })
        .catch(function (e) {
          this.$emit("showToast", this.errorMsg.exceptionMessage, "error");
        });
      this.isShowOptionItem = false;
    },
    //Xử lý nhân bản nhân viên
    handleDuplicate() {
      var employee = {};
      Object.assign(employee, this.employee);
      delete employee.EmployeeId;
      delete employee.EmployeeCode;
      console.log(this.employee);
      this.$emit("employeeValue", employee);
      this.isShowOptionItem = false;
    },

    /**
     * Mô tả:  Lắng nghe sự kiện dbClick
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 27/04/2022
     */
    onDbClick(e) {
      console.log("ok");
      e.stopPropagation();
    },
  },
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
}
.padding-right-td {
  right: 0px;
}
table tr:hover > .padding-td {
  background-color: #fff;
}
.hidden-td {
  min-width: 30px;
  position: sticky;
  position: -webkit-sticky;
  right: -30px;
  background-color: #eceef1 !important;
  border: none;
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
