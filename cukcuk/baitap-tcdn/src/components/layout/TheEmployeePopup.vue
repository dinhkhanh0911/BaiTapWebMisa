<template>
  <div class="popup-container">
    <div class="popup" ref="popup" id="popup">
      <div class="popup-dialog" role="document">
        <div class="popup-content">
          <div class="popup-header d-flex justify-space-between">
            <div class="popup-title-box">
              <div class="header-popup d-flex">
                <h1 class="popup-title">Thông tin nhân viên</h1>
              </div>
            </div>
            <div class="close-popup">
              <div class="help mi mi-24"></div>
              <div class="close mi mi-24" @click="handleClosePopup()"></div>
            </div>
          </div>
          <div class="popup-body">
            <div class="grid">
              <div class="primary-input">
                <div class="row">
                  <div class="col c-2-5">
                    <div class="form-group">
                      <label for=""
                        >Mã<span class="required-text">*</span></label
                      >
                      <BaseInput
                        v-model="employee.EmployeeCode"
                        ref="EmployeeCode"
                        :componentDes="'Mã'"
                      />
                    </div>
                  </div>
                  <div class="col c-3-5 padding-right-8">
                    <div class="form-group">
                      <label for=""
                        >Tên<span class="required-text">*</span></label
                      >
                      <BaseInput
                        v-model="employee.FullName"
                        ref="FullName"
                        :componentDes="'Tên'"
                        @blur="onBlur($event,'FullName')"
                      />
                    </div>
                  </div>
                  <div class="col c-2-5 padding-left-8">
                    <div class="form-group">
                      <label for="">Ngày sinh</label>
                      <DatePicker
                        v-model:value="formatBrithDay"
                        :format="'DD/MM/YYYY'"
                        :placeholder="'DD/MM/YYYY'"
                        :lang="'vi'"
                        :clearable="false"
                        title-format="DD/MM/YYYY"
                        :disabled-date="afterToday"
                      />
                    </div>
                  </div>
                  <div class="col c-3-5">
                    <div class="form-group">
                      <label for="">Giới tính</label>
                      <div class="checkbox-container d-flex">
                        <BaseRadioButton
                          :classCheckbox="'right-10'"
                          :nameRadio="'Gender'"
                          :labelRadio="'Nam'"
                          v-model="employee.Gender"
                          :valueRadio="1"
                        />
                        <BaseRadioButton
                          :nameRadio="'Gender'"
                          :labelRadio="'Nữ'"
                          v-model="employee.Gender"
                          :valueRadio="0"
                        />
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col c-6 padding-right-8">
                    <div class="form-group">
                      <label for=""
                        >Đơn vị<span class="required-text">*</span></label
                      >
                      <BaseCombobox
                        :Api="departmentApi"
                        v-model="employee.DepartmentId"
                        :id="'DepartmentId'"
                        :name="'DepartmentName'"
                        :code="'DepartmentCode'"
                        :componentDes="'Đơn vị'"
                        ref="DepartmentId"
                      />
                    </div>
                  </div>
                  <div class="col c-3-5 padding-left-8">
                    <div class="form-group">
                      <label for="" :title="'Số chứng minh nhân dân'"
                        >Số CMND</label
                      >
                      <BaseInput v-model="employee.IdentityNumber" />
                    </div>
                  </div>
                  <div class="col c-2-5">
                    <div class="form-group">
                      <label for="">Ngày cấp</label>
                      <DatePicker
                        v-model:value="formatIdentityDate"
                        :format="'DD/MM/YYYY'"
                        :placeholder="'DD/MM/YYYY'"
                        :lang="'vi'"
                        :clearable="false"
                        title-format="DD/MM/YYYY"
                        :disabled-date="afterToday"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col c-6 padding-right-8">
                    <div class="form-group">
                      <label for="">Chức danh</label>
                      <BaseInput v-model="employee.PositionName" />
                    </div>
                  </div>
                  <div class="col c-6 padding-left-8">
                    <div class="form-group">
                      <label for="">Nơi cấp</label>
                      <BaseInput v-model="employee.IdentityPlace" />
                    </div>
                  </div>
                </div>
              </div>
              <div class="sub-input">
                <div class="row">
                  <div class="col c-12">
                    <div class="form-group">
                      <label for="">Địa chỉ</label>
                      <BaseInput v-model="employee.ContactAddress" />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col c-3">
                    <div class="form-group">
                      <label for="">ĐT di dộng</label>
                      <BaseInput v-model="employee.PhoneNumber" />
                    </div>
                  </div>
                  <div class="col c-3">
                    <div class="form-group">
                      <label for="">ĐT cố định</label>
                      <BaseInput v-model="employee.LandlineNumber" />
                    </div>
                  </div>
                  <div class="col c-3">
                    <div class="form-group">
                      <label for="">Email</label>
                      <BaseInput
                        v-model="employee.Email"
                        ref="Email"
                        :componentDes="'Email'"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col c-3">
                    <div class="form-group">
                      <label for="">Tài khoản ngân hàng</label>
                      <BaseInput v-model="employee.BankAccount" />
                    </div>
                  </div>
                  <div class="col c-3">
                    <div class="form-group">
                      <label for="">Tên ngân hàng</label>
                      <BaseInput v-model="employee.BankName" />
                    </div>
                  </div>
                  <div class="col c-3">
                    <div class="form-group">
                      <label for="">Chi nhánh</label>
                      <BaseInput v-model="employee.BankBranch" />
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="border-bottom"></div>
          </div>
          <div class="popup-footer">
            <div class="btn-container">
              <div class="btn-left d-flex">
                <BaseButton
                  :classBtn="'btn-default btn-sq btn-no-op'"
                  :content="'Hủy'"
                  @click="closePopup()"
                  @keyup.tab="handleTabEvent($event)"
                />
              </div>
              <div class="btn-right d-flex">
                <BaseButton
                  :classBtn="'btn-default btn-sq btn-no-op'"
                  :content="'Cất'"
                  @click="handleSave()"
                />
                <BaseButton
                  :classBtn="'btn-default btn-sq btn-no-op btn-primary'"
                  :content="'Cất và thêm'"
                  @click="handleSaveAndContinue()"
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
import axios from "axios";
import Validator from "@/assets/js/validator.js";
import BasePopup from "@/components/base/BasePopup.vue";
import DatePicker from "vue-datepicker-next";
import "vue-datepicker-next/index.css";
import Helpers from "@/assets/js/helpers.js";
import Api from "@/assets/js/api.js";
export default {
  components: {
    BaseInput,
    BaseButton,
    BaseRadioButton,
    BaseCombobox,
    BasePopup,
    DatePicker,
  },
  props: {
    modelPopup: {
      type: Object,
      default: {},
    },
  },
  data() {
    return {
      employee: {},
      employeeValiate: {},
      departmentApi: Api.getDepartment,
      required: ["EmployeeCode", "FullName", "DepartmentId"],
      email: ["Email"],
      contentPopupInfo: "",
      isShowPopupInfo: false,
      typePopupInfo: "error",

      keyCombobox: "",
      errorRefsName: "",
    };
  },
  computed: {
    /**
     * Mô tả:  Chuyển định dạng ngày sinh
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 12/04/2022
     */
    formatBrithDay: {
      get() {
        if(this.employee.DateOfBirth != null){
          var date = new Date(this.employee.DateOfBirth);
          return date;
        }
      },
      set(date) {
        this.employee.DateOfBirth = new Date(date);
        this.employee.DateOfBirth = this.employee.DateOfBirth.toDateString();
        
      },
    },
    formatIdentityDate: {
      get() {
        if(this.employee.IdentityDate != null){
          var date = new Date(this.employee.IdentityDate);
        
          return date;
        }
      },
      set(date) {
        this.employee.IdentityDate = new Date(date);
        this.employee.IdentityDate = this.employee.IdentityDate.toDateString();
      },
    },
  },

  mounted() {
    this.$refs.EmployeeCode.focus();
    //lắng nghe sự kiện bàn phím
    window.addEventListener("keyup", this.handleKeyEvent);
    window.addEventListener("keydown", this.handleKeyEventDown);

    //set draggable popup
    Helpers.draggable("#popup");
    Object.assign(this.employeeValiate, this.employee);
  },
  //Bỏ lắng nghe xự kiện
  beforeUnmount() {
    window.removeEventListener("keyup", this.handleKeyEvent);
    window.removeEventListener("keydown", this.handleKeyEventDown);
  },
  created() {
    this.employee = this.modelPopup;
    //Nếu employee không có mã code thì lấy mã code
    if (this.employee.EmployeeCode == undefined) {
      this.getNewCode();
    }
  },

  methods: {
    /**
     * Mô tả: Xử lý sự kiện tab up
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 27/04/2022
     */
    handleTabEvent(e) {
      this.$refs["EmployeeCode"].focus();
    },
    /**
     * Mô tả: Xử lý sự kiện tab down
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 27/04/2022
     */
    handleTabEventDown(e) {
      e.preventDefault();
    },
    /**
        * Mô tả: So sánh 2 object
        @param object a, object b
        *@return boolean
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 12/04/2022
        */
    compareObject(a, b) {
      for (var item in a) {
        if (a[item] !== b[item]) {
          return true;
        }
      }
      return false;
    },
    /**
     * Mô tả: Xử lý sự kiện đóng popup
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 12/04/2022
     */
    handleClosePopup() {
      this.compareObject(this.employee, this.employeeValiate);
      if (this.compareObject(this.employee, this.employeeValiate)) {
        this.showPopupInfo(
          this.typePopupName.info,
          this.popupMsg.confirmChangeDataMsg,
          "confirm"
        );
      } else {
        this.closePopup();
      }
    },
    //Đóng popup
    closePopup() {
      this.$emit("showPopup", false);
    },

    //Xử lý sự kiện luu thông tin nhân viên và tiếp tục thêm
    handleSaveAndContinue() {
      if (this.validate()) {
        this.save(true);
      }
    },
    //Xử lý sự kiện luu thông tin nhân viên
    handleSave() {
      if (this.validate()) {
        this.save(false);
      }
    },
    /**
        * Mô tả: Hiển thị popup thông báo
        @param dạng popup, Nội dung , key
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 12/04/2022
        */
    showPopupInfo(type, errorMsg, key) {
      
      this.typePopupInfo = type;
      this.contentPopupInfo = errorMsg;
      this.keyCombobox = key;
      
      this.isShowPopupInfo = true;
    },

    /**
     * Mô tả: Lưu thông tin nhân viên
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 12/04/2022
     */
    save(isContinue) {
      // Cập nhật thông tin
      if (this.employee.EmployeeId != undefined) {
        axios
          .put(
            `${Api.updateEmployee}/${this.employee.EmployeeId}`,
            this.employee
          )
          .then((response) => {
            if (response.status === 200) {
              this.$refs["EmployeeCode"].focus();
              this.$emit(
                "showToast",
                this.toastMsg.updateSuccessMsg,
                "success"
              );
              this.$emit("addSuccess", true);
              if (!isContinue) {
                this.closePopup();
              } else {
                this.employee = {};
                this.employee.Gender = 1;
                this.getNewCode();
              }
              // this.employee = response.data
              // console.log(this.employee)
            }
          })
          .catch((e) => {
            var keys = Object.keys(e.response.data.data);
            if (keys.length > 0) {
              this.errorRefsName = keys[0];
              var errorMsg = e.response.data.data[keys[0]];
              
              this.showPopupInfo(this.typePopupName.warningNotify, errorMsg, "validate");
              
            } else {
              this.showPopupInfo(
                this.typePopupName.warningNotify,
                this.errorMsg.exceptionMessage,
                "validate"
              );
            }
          });
      }
      // Thêm nhân viên
      else {
        
        axios
          .post(Api.addEmployee, this.employee)
          .then((response) => {
            if (response.status === 201) {
              this.$refs["EmployeeCode"].focus();
              this.$emit("showToast", this.toastMsg.addSuccessMsg, "success");
              this.$emit("addSuccess", true);
              if (!isContinue) {
                this.closePopup();
              } else {
                this.employee = {};
                this.employee.Gender = 1;
                this.getNewCode();
              }
              // this.employee = response.data
              // console.log(this.employee)
            }
          })
          .catch((e) => {
            var keys = Object.keys(e.response.data.data);
            if (keys.length > 0) {
              var errorMsg = e.response.data.data[keys[0]];
              this.errorRefsName = keys[0];
              this.showPopupInfo(this.typePopupName.warningNotify, errorMsg, "validate");
            } else {
              this.showPopupInfo(
                this.typePopupName.warningNotify,
                this.toastMsg.exceptionMessage,
                "validate"
              );
            }
          });
      }
    },

    /**
     * Mô tả: Validate các trường dữ liệu
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    validate() {
      var submittable = true;
      var isFirstErrorMsg = true;

      //Required
      for (var item of this.required) {
        var errorMsg = Validator.required(this.employee[item]);
        if (errorMsg != undefined) {
          if (!this.$refs[item].error) {
            this.$refs[item].addError("error", errorMsg);
          }
          submittable = false;
          //Hiển thị popup thông báo lỗi đầu tiên
          if (isFirstErrorMsg) {
            isFirstErrorMsg = false;
            this.errorRefsName = item;
            this.showPopupInfo(
              this.typePopupName.error,
              this.$refs[item].title,
              "validate"
            );
          }
        }
      }

      //Định dạng email
      for (var item of this.email) {
        var errorMsg = Validator.email(this.employee[item]);
        if (errorMsg != undefined) {
          if (!this.$refs[item].error) {
            this.$refs[item].addError("error", errorMsg);
          }

          submittable = false;
          //Hiển thị popup thông báo lỗi đầu tiên
          if (isFirstErrorMsg) {
            isFirstErrorMsg = false;
            this.errorRefsName = item;
            this.showPopupInfo(
              this.typePopupName.error,
              this.$refs[item].title,
              "validate"
            );
          }
        }
      }
      return submittable;
    },

    /**
     * Mô tả: Xử lý sự kiện confirm popup
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleConfirmBtn(isConfirm, key) {
      //Popup thông báo validate
      if (key === "validate") {
        this.isShowPopupInfo = false;
        
        this.$refs[`${this.errorRefsName}`].focus();
      }

      //Popup xác nhận
      if (key === "confirm") {
        this.isShowPopupInfo = false;
        if (isConfirm === 0) {
          
          this.$emit("showPopup", false);
        } else if (isConfirm) {
          
          this.handleSaveAndContinue();
        } else {
          console.log(isConfirm);
        }
      }
    },

    /**
     * Mô tả: Lấy mã nhân viên mới
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    getNewCode() {
      // Gọi API lấy dữ liệu

      axios.get(`${Api.getNewEmployeeCode}`).then((response) => {
        if (response.status === 200) {
          this.employee.EmployeeCode = response.data;
          this.employeeValiate.EmployeeCode = response.data;
        }
      });
    },

    /**
     * Mô tả: Xử lý sự kiện key up
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleKeyEvent(e) {
      if (e.ctrlKey && e.keyCode == 83) {
        this.handleSaveAndContinue();
      }
    },

    /**
     * Mô tả: Xử lý sự kiện key down
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleKeyEventDown(e) {
      if (e.ctrlKey && e.keyCode == 83) {
        e.preventDefault();
      }
    },

    /**
     * Mô tả: Validate ngày nhập lớn hơn ngày hiện tại
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    afterToday(date) {
      return date > new Date();
    },
    onBlur($event,ref){
      if(this.employee[ref].trim() === ''){
        this.$refs[ref].addError("error", 'Trường dữ liệu không được để trống');
      }
    }
  },
};
</script>
<style scoped>
@import url("@/assets/css/commom/datepicker.css");
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
  width: 900px;
  background-color: #fff;
  position: absolute;
  top: 50%;

  left: 50%;
  transform: translateX(-50%) translateY(-50%);
  animation: moveUpToDown linear 0.3s;
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
.primary-input {
  margin-bottom: 30px;
}
.sub-input {
  margin-bottom: 40px;
}
.form-group {
  margin-bottom: 10px;
}
.form-group label {
  display: block;
  margin-bottom: 3px;
}
.required-text {
  color: red;
  margin-left: 2px;
}
.custom-checkbox {
  margin-left: 10px;
}

/* footer */
.popup-footer {
  padding: 24px 32px;
}
.btn-container {
  display: flex;
  justify-content: space-between;
}
.movable {
  cursor: grabbing;
}
</style>
