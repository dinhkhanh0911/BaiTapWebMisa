<template>
  <div class="popup-container">
    <div class="popup" ref="popup" id="popup">
      <div class="popup-dialog" role="document">
        <div class="popup-content">
          <div class="popup-header d-flex justify-space-between">
            <div class="popup-title-box">
              <div class="header-popup d-flex">
                <h1 class="popup-title">Thông tin nhà cung cấp</h1>
              </div>
            </div>
            <div class="close-popup">
              <div class="help mi mi-24"></div>
              <div class="close mi mi-24" @click="handleClosePopup()"></div>
            </div>
          </div>
          <div class="popup-body">
            <div class="grid">
              <div class="primary-input" >
                <div class="row">
                  <div class="col c-2-5">
                    <div class="form-group">
                      <label for=""
                        >Mã nhà cung cấp<span class="required-text">*</span></label
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
                        >Mã số thuế<span class="required-text">*</span></label
                      >
                      <BaseInput
                        v-model="employee.FullName"
                        ref="FullName"
                        :componentDes="'Tên'"
                        @blur="onBlur($event,'FullName')"
                      />
                    </div>
                  </div>
                  <div class="col c-6 padding-left-8">
                    <div class="form-group">
                      <label for=""
                        >Nhóm nhà cung cấp</label
                      >
                      <BaseComboboxMulti
                        :Api="vendorGroupApi"
                        v-model="employee.DepartmentId"
                        :id="'VendorGroupId'"
                        :name="'VendorGroupName'"
                        :code="'VendorGroupCode'"
                        :componentDes="'Đơn vị'"
                        ref="VendorGroupId"
                        :timer="1000"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col c-2-5">
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
                  <div class="col c-6 padding-left-8">
                    <div class="form-group">
                      <label for=""
                        >Nhóm nhà cung cấp</label
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
                </div>
                <div class="row">
                  <div class="col c-6 padding-right-8">
                    <div class="form-group">
                      <label for="">Địa chỉ</label>
                      <textarea v-model="employee.PositionName" style="width:100%"
                        placeholder="VD: số 82 Duy Tân Dịch Vọng Hậu Cầu Giấy Hà Nội"
                      />
                    </div>
                  </div>
                  
                </div>
              </div>
              <div class="primary-input" style="display:none">
                <div class="row">
                  <div class="col c-2-5">
                    <div class="form-group">
                      <label for=""
                        >Mã nhà cung cấp<span class="required-text">*</span></label
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
                        >Mã số thuế<span class="required-text">*</span></label
                      >
                      <BaseInput
                        v-model="employee.FullName"
                        ref="FullName"
                        :componentDes="'Tên'"
                        @blur="onBlur($event,'FullName')"
                      />
                    </div>
                  </div>
                  <div class="col c-6 padding-left-8">
                    <div class="form-group">
                      <label for=""
                        >Nhóm nhà cung cấp</label
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
                </div>
                <div class="row">
                  <div class="col c-2-5">
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
                  <div class="col c-6 padding-left-8">
                    <div class="form-group">
                      <label for=""
                        >Nhóm nhà cung cấp</label
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
                </div>
                <div class="row">
                  <div class="col c-6 padding-right-8">
                    <div class="form-group">
                      <label for="">Địa chỉ</label>
                      <textarea v-model="employee.PositionName" style="width:100%" />
                    </div>
                  </div>
                  
                </div>
              </div>
              <div class="sub-input">
                <div class="tab-input d-flex">
                    <div class="tab tab-active"
                        ref="tab-1" 
                        @click="handleTabClick('tab-1')">
                        <span>Liên hệ</span>
                    </div>
                    <div class="tab"
                        @click="handleTabClick('tab-2')"
                        ref="tab-2" >
                        <span>Điều khoản thanh toán</span>
                    </div>
                    <div class="tab"
                        @click="handleTabClick('tab-3')"
                        ref="tab-3" >
                        <span>Tài khoản ngân hàng</span>
                    </div>
                    <div class="tab" @click="handleTabClick('tab-4')"
                        ref="tab-4" >
                        <span>Địa chỉ khác</span>
                    </div>
                    <div class="tab" @click="handleTabClick('tab-5')"
                        ref="tab-5" >
                        <span>Ghi chú</span>
                    </div>
                </div>
                <div class="sub-input-container" 
                  v-if="tabActive == 'tab-1'">
                  <div class="row">
                    <div class="col c-6">
                      <div class="form-group">
                        <label for="">Thông tin liên hệ</label>
                        <BaseInput v-model="employee.ContactAddress" :placeholder="'Email'"/>
                      </div>
                    </div>
                    <div class="col c-3">
                      <div class="form-group">
                        <label for="">Thông tin CMND/Thẻ căn cước</label>
                        <BaseInput v-model="employee.ContactAddress" :placeholder="'Số CMND/Thẻ căn cước'"/>
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col c-3">
                      <div class="form-group">
                        
                        <BaseInput v-model="employee.PhoneNumber" 
                        :placeholder="'Điện thoại di động'"/>
                      </div>
                    </div>
                    <div class="col c-3">
                      
                    </div>
                    <div class="col c-3">
                      <DatePicker
                        v-model:value="formatBrithDay"
                        :format="'DD/MM/YYYY'"
                        :placeholder="'Ngày cấp'"
                        :lang="'vi'"
                        :clearable="false"
                        title-format="DD/MM/YYYY"
                        :disabled-date="afterToday"
                      />
                    </div>
                  </div>
                  <div class="row">
                    <div class="col c-3">
                      <div class="form-group">
                        
                        <BaseInput v-model="employee.PhoneNumber"
                        :placeholder="'Điện thoại cố định'"/>
                      </div>
                    </div>
                    <div class="col c-3">
                      
                    </div>
                    <div class="col c-6">
                      <BaseInput v-model="employee.PhoneNumber" 
                        :placeholder="'Nơi cấp'"/>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col c-6">
                      <div class="form-group">
                        <label for="">Đại diện theo PL</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
                    </div>
                  
                  </div>
                </div>
                <div class="sub-input-container" 
                  v-if="tabActive == 'tab-2'">
                  <div class="row">
                    <div class="col c-3">
                      <div class="form-group">
                        <label for="">Điều khoản thanh toán</label>
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
                    <div class="col c-3">
                      <div class="form-group">
                        <label for="">Số ngày được nợ</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
                    </div>
                    <div class="col c-3">
                      <div class="form-group">
                        <label for="">Số nợ phải trả</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col c-3">
                      <div class="form-group">
                        <label for="">Tài khoản công nợ phải thu</label>
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
                    <div class="col c-3">
                      <div class="form-group">
                        <label for="">Tài khoản công nợ phải trả</label>
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
                  
                  </div>
                </div>
                <div class="sub-input-container" 
                  v-if="tabActive == 'tab-3'">
                  <div class="row">
                    <div class="col c-12">
                      <div class="form-group">
                        <label for="">Địa chỉ 3</label>
                        <BaseEditableTable v-model="employee.ContactAddress" 
                          :api="tableBankAccountApi"
                        />
                      </div>
                    </div>
                  </div>
                  
                </div>
                <div class="sub-input-container" 
                  v-if="tabActive == 'tab-4'">
                  <div class="row">
                    <div class="col c-6">
                      <div class="form-group">
                        <label for="">Địa chỉ 4</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
                    </div>
                    <div class="col c-3">
                      <div class="form-group">
                        <label for="">Thông tin CMND/Thẻ căn cước</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col c-3">
                      <div class="form-group">
                        
                        <BaseInput v-model="employee.PhoneNumber" />
                      </div>
                    </div>
                    <div class="col c-3">
                      <div class="form-group">
                        
                        
                      </div>
                    </div>
                    <div class="col c-3">
                      <div class="form-group">
                        <BaseInput
                          v-model="employee.Email"
                          ref="Email"
                          :componentDes="'Email'"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col c-6">
                      <div class="form-group">
                        <label for="">Đại diện theo PL</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
                    </div>
                  
                  </div>
                </div>
                <div class="sub-input-container" 
                  v-if="tabActive == 'tab-5'">
                  <div class="row">
                    <div class="col c-6">
                      <div class="form-group">
                        <label for="">Địa chỉ 5</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
                    </div>
                    <div class="col c-3">
                      <div class="form-group">
                        <label for="">Thông tin CMND/Thẻ căn cước</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col c-3">
                      <div class="form-group">
                        
                        <BaseInput v-model="employee.PhoneNumber" />
                      </div>
                    </div>
                    <div class="col c-3">
                      <div class="form-group">
                        
                        
                      </div>
                    </div>
                    <div class="col c-3">
                      <div class="form-group">
                        <BaseInput
                          v-model="employee.Email"
                          ref="Email"
                          :componentDes="'Email'"
                          :placeholder="'Email'"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col c-6">
                      <div class="form-group">
                        <label for="">Đại diện theo PL</label>
                        <BaseInput v-model="employee.ContactAddress" />
                      </div>
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
import BaseComboboxMulti from "@/components/base/BaseComboboxMulti.vue";
import BaseEditableTable from "@/components/base/BaseEditableTable.vue";
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
    BaseComboboxMulti,
    BaseEditableTable,
    BaseEditableTable
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
      vendorGroupApi:Api.getVendorGroup,
      required: ["EmployeeCode", "FullName", "DepartmentId"],
      email: ["Email"],
      contentPopupInfo: "",
      isShowPopupInfo: false,
      typePopupInfo: "error",

      keyCombobox: "",
      errorRefsName: "",

      /* Tab */
      tabActive:"tab-3",

      tableBankAccountApi:`${Api.getColumnOption}/BankAccount`

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
        this.employee.IdentityDate = this.employee.DateOfBirth.toDateString();
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
    * Mô tả: Xử lý sự kiện chuyển tab trong form thêm
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 10/05/2022
    */
    handleTabClick(refName){
        this.tabActive = refName
        var tabs = document.querySelectorAll('.tab')
        tabs.forEach(tab => {
          tab.classList.remove('tab-active')
        })
        if(!this.$refs[refName].classList.contains('tab-active')){
          this.$refs[refName].classList.add('tab-active')
        }
    },
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
.popup-body .row{
  margin: 0px;
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
  font-size: 12px;
  font-weight: 700;
  color: #111;
}
.form-group textarea{
  resize: none;
  border-radius: 3px;
  border: 1px solid #babec5;
  font-family: inherit;
  display: block;
  padding: 9px;
  color: #000;
  font-size: 13px;
  width: 100%;
  height: 100%;
  outline: none;
  min-height: 56px;
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
.tab-input{
    align-items: flex-end;
}
.tab{
    border: 1px solid #c7c7c7;
    padding: 0px 10px;
    height: 26px;
    display: flex;
    align-items: center;
    cursor: pointer;
}
.tab+.tab{
    margin-left: 5px;
}
.tab-active{
    background: #cce4f9;
    height: 32px;
    font-weight: 700;
}
.tab:hover{
  color: #2ca01c;
}
.sub-input-container{
  border: 1px solid #c7c7c7;
  padding: 5px 10px;
  min-height: 186px;
}
</style>
