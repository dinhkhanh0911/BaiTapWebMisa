<template>
  <div class="m-popup">
    <div class="payment-popup">
      <div class="popup-title">
        <div class="left">
          <div class="popup-icon mi mi-24 ic-recent-log"></div>
          <div class="popup-title-text">Phiếu chi {{payment.PaymentCode}}</div>
          <div class="type-payment">
            <BaseCombobox
              :valueOption="filterTypePayments"
              :id="'Id'"
              :name="'Value'"
              :code="'Value'"
              :componentDes="'Type Payment'"
              ref="TypePayment"
              :isOneColumn="'True'"
              v-model="payment.PaymentType"
            />
          </div>
        </div>
        <div class="right">
          <div class="guideline">
            <div class="mi mi-24 ic-guideline"></div>
            <a class="link-guideline">Hướng dẫn</a>
          </div>
          <div class="setting">
            <div class="mi mi-24 ic-setting"></div>
          </div>
          <div class="help">
            <div class="mi mi-24 ic-help-dialog"></div>
          </div>
          <div class="exit">
            <div class="mi mi-24 ic-exit-dialog"></div>
          </div>
        </div>
      </div>
      <!-- content -->
      <div class="popup-content grid">
        <div class="form row">
          <div class="form-input col c-10">
            <div class="row" style="width:100%">
              <div class="input-left col c-8">
                <div class="row">
                  <div class="left col c-5">
                    <div class="lable"><label for="">Đối tượng</label></div>
                    <BaseCombobox
                      :Api="'http://localhost:5158/api/v1/Vendors/filterAdvanced'"
                      :id="'VendorId'"
                      :name="'VendorName'"
                      :code="'VendorCode'"
                      :componentDes="'Nhân viên mua hàng'"
                      ref="VendorId"
                      v-model="payment.VendorId"
                      @change="handleChangeVendor"
                      :isLazyloading="'true'"
                    />
                  </div>
                  <div class="right col c-7">
                    <div class="lable"><label for="">Tên đối tượng</label></div>
                    <BaseInput v-model="vendorName" ref="vendorName" />
                  </div>
                </div>
                <div class="row">
                  <div class="left col c-5">
                    <div class="lable"><label for="">Người nhận</label></div>
                    <BaseInput v-model="payment.ReceiverName" />
                  </div>
                  <div class="right col c-7">
                    <div class="lable"><label for="">Địa chỉ</label></div>
                    <BaseInput v-model="payment.Address"  />
                  </div>
                </div>
                <div class="row">
                  <div class="right col c-12">
                    <div class="lable"><label for="">Lý do chi</label></div>
                    <BaseInput :placeholder="'Chi tiền cho'" v-model="payment.DescriptionPayment" />
                  </div>
                </div>
                <div class="row">
                  <div class="left col c-5">
                    <div class="lable"><label for="">Nhân viên</label></div>
                    <BaseCombobox
                      :Api="employeesApi"
                      :id="'EmployeeId'"
                      :name="'EmployeeName'"
                      :code="'EmployeeCode'"
                      :componentDes="'Nhân viên mua hàng'"
                      ref="EmployeeId"
                      v-model="payment.EmployeeId"
                      :className="'type-money'"
                    />
                  </div>
                  <div class="right col c-6">
                    <div class="lable"><label for="">Kèm theo</label></div>
                    <div style="display: flex; align-items: center">
                      <div class="m-col-7">
                        <BaseInput :placeholder="'Số lượng'" :align="'right'" v-model="payment.AttachDocumentAmount" :searchClass="'text-right'"/>
                      </div>
                      <div class="description" style="margin-left: 6px; white-space:nowrap;">
                        chứng từ gốc
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="input-right col c-4">
                <div class="row">
                  <div class="right col c-12">
                    <div class="lable"><label for="">Ngày hạch toán</label></div>
                    <!-- eslint-disable -->
                    <DatePicker
                      v-model:value="formatAccountingDate"
                      :format="'DD/MM/YYYY'"
                      :placeholder="'DD/MM/YYYY'"
                      :lang="'vi'"
                      :clearable="false"
                      title-format="DD/MM/YYYY"
                    />
                    <!-- eslint-enable -->
                  </div>
                </div>
                <div class="row">
                  <div class="right col c-12">
                    <div class="lable"><label for="">Ngày chứng từ</label></div>
                    <!-- eslint-disable -->
                    <DatePicker
                      v-model:value="formatPaymentDate"
                      :format="'DD/MM/YYYY'"
                      :placeholder="'DD/MM/YYYY'"
                      :lang="'vi'"
                      :clearable="false"
                      title-format="DD/MM/YYYY"
                    />
                    <!-- eslint-enable -->
                  </div>
                </div>
                <div class="row">
                  <div class="right col c-12">
                    <div class="lable">
                      <label for=""
                        >Số phiếu chi</label
                      >
                    </div>
                    <BaseInput v-model="payment.PaymentCode" />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="m-right summary-info col c-2">
            <div class="total-payment">
              <div class="header" style="margin-bottom:10px">Tổng tiền</div>
              <div class="total text-right" >{{totalMoney}}</div>
            </div>
          </div>
        </div>
        <div class="table-area-1">
          <div class="title">
            <a class="link">Hạch toán</a>
            <div class="type-currency">
              <div style="padding: 0px 10px 0px 20px">Loại tiền</div>
              <div class="m-col-6">
                <BaseCombobox 
                  :valueOption="typeMoney"
                  :id="'Id'"
                  :name="'Value'"
                  :code="'Value'"
                  v-model="payment.CurrencyId"
                  :className="'type-money'"
                />
                
              </div>
            </div>
          </div>
        </div>
        <div class="table-area">
          <div class="table">
            <!-- <MTableEditable :deleteFunc="true"/> -->
            <div class="table-container">
              <table>
                  <thead>
                      <tr>
                        <th class="text-right">
                            #
                        </th>
                        <th>
                          Diễn giải
                        </th>
                        <th>
                          TK nợ
                        </th>
                        <th>
                          Tk có
                        </th>
                        <th>
                          Số tiền
                        </th>
                        <th>
                          Đối tượng
                        </th>
                        <th>
                          Tên đối tượng
                        </th>
                        <th class="option"></th>
                      </tr>
                  </thead>
                  <tbody>
                      <tr v-for="(value,index) in paymentDetails" :key="index">
                        <td class="text-right">
                            {{index+1}}
                        </td>
                        <td>
                            <BaseInput v-model="value.PaymentDescriptionPayment" @click="filterPaymentDetail()"/>
                        </td>
                        <td>
                            <BaseCombobox 
                              :valueOption="accounts"
                              :id="'Id'"
                              :name="'Value'"
                              :code="'Code'"
                              v-model="value.DebitAccountId"
                              @click="filterPaymentDetail()"
                            />
                        </td>
                        <td>
                            <BaseCombobox 
                              :valueOption="accounts"
                              :id="'Id'"
                              :name="'Value'"
                              :code="'Code'"
                              v-model="value.CreditAccountId"
                              @click="filterPaymentDetail()"
                            />
                        </td>
                        <td >
                            <BaseInput :isNumber="'true'" v-model="value.CashAmount" :searchClass="'text-right'"/>
                        </td>
                        <td>
                            <BaseCombobox
                              :Api="vendorsApi"
                              :id="'VendorId'"
                              :name="'VendorName'"
                              :code="'VendorCode'"
                              :componentDes="'Nhân viên mua hàng'"
                              ref="VendorId"
                              v-model="value.VendorId"
                              @click="filterPaymentDetail()"
                            />
                        </td>
                        <td>
                            <BaseInput @click="filterPaymentDetail()"/>
                        </td>
                        <td class="option d-flex justify-space-center alignt-center">
                            <div class="option-icon mi mi-16"></div>
                        </td>
                      </tr>
                  </tbody>
                  <tfoot>
                    <tr >
                        <th>
                            
                        </th>
                        <th>
                            
                        </th>
                        <th>
                            
                        </th>
                        <th>
                            
                        </th>
                        <th>
                            {{totalMoney}}
                        </th>
                        <th>
                            
                        </th>
                        <th>
                            
                        </th>
                        <th class="option d-flex justify-space-center alignt-center">
                          
                        </th>
                      </tr>
                  </tfoot>
              </table>
              
              <div class="file-attach">
                <div class="button-area">
                  <div class="add-row">
                    <button @click="addRowTableAccount()">Thêm dòng</button>
                  </div>
                  <div class="delete-all">
                    <button>Xóa hết dòng</button>
                  </div>
                </div>
                <div class="attach">
                  <div class="head">
                    <div class="ic-attach"></div>
                    <div class="text" style="font-weight: 600; padding-right: 8px">
                      Đính kèm
                    </div>
                    <div class="text">Dung lượng tối đa 5Mb</div>
                  </div>
                  <div class="drag-target">
                    <div class="text">Kéo/thả tệp vào đây hoặc bấm vào đây</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- footer -->
      <div class="popup-footer">
        <div class="m-left">
          <div class="cancel-btn">
            <BaseButton
              :classBtn="'btn-default btn-sq btn-no-op'"
              :content="'Hủy'"
              @click="closePopup()"
              @keyup.tab="handleTabEvent($event)"
            />
          </div>
        </div>
        <div class="m-right">
          <div class="save-btn">
            <BaseButton
              :classBtn="'btn-default btn-sq btn-no-op'"
              :content="'Cất'"
              @click="handleSave()"
            />
                
          </div>
          <div class="save-and-print-btn">
            <BaseButton
              :classBtn="'btn-default btn-sq btn-no-op btn-primary'"
              :content="'Cất và thêm'"
              @click="handleSaveAndContinue()"
            />
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
  </div>
</template>

<script>
import BaseCombobox from "@/components/base/BaseCombobox.vue";
import BaseButton from "@/components/base/BaseButton.vue";
import BaseInput from "@/components/base/BaseInput.vue";
import DatePicker from "vue-datepicker-next";
import BaseEditableTable from "@/components/base/BaseEditableTable.vue";
import "vue-datepicker-next/index.css";
import Api from '@/assets/js/api';
import axios from "axios";
import DB from '@/assets/js/hashDatabase'
import BasePopup from '@/components/base/BasePopup.vue'
export default {
  name: "ThePaymentDialog",
  components: {
    BaseButton,
    BaseInput,
    DatePicker,
    BaseEditableTable,
    BaseCombobox,
    BasePopup
},
  data() {
    return {
      payment:{},
      paymentDetails:[],
      comboboxData: {
        name: "GenderName",
        id: "Gender",
        data: [
          {
            Gender: "0",
            GenderName: "Nam",
          },
          {
            Gender: "1",
            GenderName: "Nữ",
          },
        ],
      },

      //Popup
      contentPopupInfo: "",
      isShowPopupInfo: false,
      typePopupInfo: "error",

      vendorName:"",
      totalMoney:0,
      //Api
      paymentsApi : Api.payments,
      paymentDetailsApi:Api.paymentDetailsByPaymentId,
      vendorsApi:Api.vendors,
      employeesApi:Api.getEmployeeById,

      //Hash Table
      filterTypePayments:DB.filterTypePayments,
      accounts:DB.accounts,
      typeMoney:DB.typeMoney
    };
  },
  props:{
    model : {
      type: Object,
      default:{}
    },
    
  },
  computed: {
    /**
     * Mô tả:  Chuyển định dạng ngày sinh
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 12/04/2022
     */
    formatAccountingDate: {
      get() {
        if(this.payment.AccountingDate != null){
          var date = new Date(this.payment.AccountingDate);
          return date;
        }
      },
      set(date) {
        this.payment.AccountingDate = new Date(date);
        this.payment.AccountingDate = this.payment.AccountingDate.toDateString();
        
      },
    },
    formatPaymentDate: {
      get() {
        if(this.payment.PaymentDate != null){
          var date = new Date(this.payment.PaymentDate);
        
          return date;
        }
      },
      set(date) {
        this.payment.PaymentDate = new Date(date);
        this.payment.PaymentDate = this.payment.PaymentDate.toDateString();
      },
    },
  },
  created(){
    
    this.payment = this.model
    if(this.payment.PaymentCode == undefined){
      this.setDefault()
    }
    
    this.loadPaymentDetail()
  },
  watch:{
    vendorName:function(){
      this.payment.ReceiverName = this.vendorName
      this.payment.DescriptionPayment = `${this.DescriptionPayment == undefined?"":this.DescriptionPayment} ${this.vendorName}`
    }
  },
  methods: {
    //Xóa nhiều bản ghi
    /**
     * Mô tả: Xử lý sự kiện confirm popup
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleConfirmBtn(isConfirm, key) {
      //Popup thông báo validate
      if (key === "validate") {
        this.isShowPopupInfo = false;
        console.log("ok")
        // this.$refs[`${this.errorRefsName}`].focus();
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
    * Mô tả: Xóa bỏ những phiếu chi chi tiết rỗng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    filterPaymentDetail(){
      var newPaymentDetails = []
      this.paymentDetails = this.paymentDetails.filter((x)=>{
        var ok = !this.validatePaymentDetail(x)
        
        if(ok) {
          newPaymentDetails.push(x)
          return true
        }
        else return false
              
      })
      console.log(newPaymentDetails)
      if(newPaymentDetails.length == 0) this.paymentDetails = [{}]
      else this.paymentDetails = newPaymentDetails
      return newPaymentDetails
            
    },
    /**
    * Mô tả: Validate Payment detail
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    validatePaymentDetail(paymentDetail){
      console.log(paymentDetail)
      if(paymentDetail.DebitAccountId == undefined) return "Tài khoản nợ không được để trống."
      if(!paymentDetail.CreditAccountId == undefined) return "Tài khoản có không được để trống."
      return undefined
    },
    /**
    * Mô tả: Xử lý thay đổi đối tượng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    handleChangeVendor(id,name){
      this.vendorName = name
    },

    /**
    * Mô tả: Chỉnh sửa mặc định các giá trị phiếu chi
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    setDefault(){
      this.payment.PaymentType = 7
      this.payment.CurrencyId = 1
      this.payment.AccountingDate = new Date()
      this.payment.PaymentDate = new Date()
      this.getNewCode()
    },
    /**
    * Mô tả: Lấy mã code của phiếu chi
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    getNewCode(){
      axios
        .get(`${this.paymentsApi}/NewCode`)
        .then((response) => {
          if (response.status === 200) {
            console.log(response.data)
            this.payment.PaymentCode = response.data
            // this.isLoading = false;
            // this.dataTable = response.data.List;
            // this.loadHandle(
            //   response.data.List,
            //   currentPage,
            //   response.data.TotalPage,
            //   response.data.Count
            // );
          }
        })
        .catch((e) => {
          console.log("e");
          // this.isLoading = false;
          // this.loadHandle([]);
        });
    },
    /**
    * Mô tả: Lấy phiếu chi chi tiết
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    loadPaymentDetail(){
      console.log(this.payment)
      if(!!this.payment.PaymentId){
        axios
        .get(`${this.paymentDetailsApi}/${this.payment.PaymentId}`)
        .then((response) => {
          
          if (response.status === 200) {
            console.log(response.data)
            this.paymentDetails = response.data
            
            // this.isLoading = false;
            // this.dataTable = response.data.List;
            // this.loadHandle(
            //   response.data.List,
            //   currentPage,
            //   response.data.TotalPage,
            //   response.data.Count
            // );
          }
        })
        .catch((e) => {
          console.log("e");
          // this.isLoading = false;
          // this.loadHandle([]);
        });
      }
      else{
        this.paymentDetails.push({
        })
      }
    },
    /**
    * Mô tả: Thêm dòng vào bảng tài khoản ngân hàng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 20/05/2022
    */
    addRowTableAccount(){
      var obj = {}
      this.paymentDetails.push(obj)
      // console.log(obj)
    },
    //Xử lý sự kiện luu thông tin nhân viên và tiếp tục thêm
    handleSaveAndContinue() {
      this.save(true);
      // if (this.validate()) {
      //   this.save(true);
      // }
    },
    //Xử lý sự kiện luu thông tin nhân viên
    handleSave() {
      this.save(false);
      // if (this.validate()) {
      //   this.save(false);
      // }
    },
    /**
     * Mô tả: Lưu thông tin nhân viên
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 12/04/2022
     */
    save(isContinue) {
      var paymentDetails = this.filterPaymentDetail()
      if(paymentDetails.length > 0) this.payment.paymentDetails = paymentDetails 
      // Cập nhật thông tin
      if (this.payment.paymentId != undefined) {
        console.log(this.payment)
        axios.put(
            `${Api.payment}/${this.payment.paymentId}`,
            this.payment
          )
          .then((response) => {
            console.log(response)
            if (response.status === 200) {
              // this.$refs["paymentCode"].focus();
              this.$emit(
                "showToast",
                this.toastMsg.updateSuccessMsg,
                "success"
              );
              this.$emit("addSuccess", true);
              // if (!isContinue) {
              //   this.closePopup();
              // } else {
              //   this.payment = {};
              //   this.employee.Gender = 1;
              //   this.getNewCode();
              // }
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
      // Thêm phiếu chi
      else {
        
        axios
          .post(this.paymentsApi, this.payment)
          .then((response) => {
            if (response.status === 201) {
              this.$refs["vendorName"].focus();
              this.$emit("showToast", this.toastMsg.addSuccessMsg, "success");
              this.$emit("addSuccess", true);
              if (!isContinue) {
                this.closePopup();
              } else {
                this.setDefault()
              }
              // this.employee = response.data
              // console.log(this.employee)
            }
          })
          .catch((e) => {
            try{
              var keys = Object.keys(e.response.data.data);
              console.log("errorMsg")
              if (keys.length > 0) {
                var errorMsg = e.response.data.data[keys[0]];
                this.errorRefsName = keys[0];
                console.log(errorMsg)
                this.showPopupInfo(this.typePopupName.warningNotify, errorMsg, "validate");
              } 
              else {
                this.showPopupInfo(
                  this.typePopupName.warningNotify,
                  this.toastMsg.exceptionMessage,
                  "validate"
                );
              }
            }
            catch(e){
              this.showPopupInfo(
                this.typePopupName.warningNotify,
                this.toastMsg.exceptionMessage,
                "validate"
              );
            }
          });
      }
    },
    //Đóng popup
    closePopup() {
      this.$emit("showPopup", false);
    },
    /**
        * Mô tả: Hiển thị popup thông báo
        @param dạng popup, Nội dung , key
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 12/04/2022
        */
    showPopupInfo(type, errorMsg, key) {
      console.log(type)
      this.typePopupInfo = type;
      this.contentPopupInfo = errorMsg;
      this.keyCombobox = key;
      
      this.isShowPopupInfo = true;
    },
    /**
     * Mô tả : disable ngày lớn hơn hiện tại trả về true nếu ngày lớn hơn hiện tại
     * Created by: Nguyễn Đức Toán - MF1095 (06/05/2022)
     */
    afterToday(date) {
      return date > new Date();
    },
  },
};
</script>

<style scoped>




.m-popup {
  position: fixed;
  width: 100vw;
  height: 100vh;
  background-color: #eceef1;
  z-index: 100;
  top: 0;
  left: 0;
}
.m-popup .payment-popup {
  width: 100vw;
  height: 100vh;
}
/*title*/
.m-popup .popup-title {
  height: 72px;
  min-height: 72px;
  width: 100%;
  padding: 16px 16px 20px 16px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.m-popup .popup-title .left {
  display: flex;
  align-items: center;
}
.m-popup .popup-title .right {
  display: flex;
  align-items: center;
}

.m-popup .popup-title .popup-title-text {
  font-weight: 700;
  font-size: 24px;
  top: -7px;
  left: 20px;
  padding: 0 0 0 12px;
  color: #111;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.m-popup .popup-title .type-payment {
  margin-left: 24px;
}
.m-popup .popup-title .setting {
  width: 40px;
  height: 36px;
  padding: 6px 8px;
}
.m-popup .popup-title .setting:hover {
  background-color: #ccc;
}
.m-popup .popup-title .help {
  width: 40px;
  height: 36px;
  padding: 6px 8px;
}
.m-popup .popup-title .help:hover {
  background-color: #ccc;
}
.m-popup .popup-title .exit {
  width: 40px;
  height: 36px;
  padding: 6px 8px;
}
.m-popup .popup-title .exit:hover {
  background-color: #ccc;
}
.m-popup .popup-title .guideline {
  display: flex;
  align-items: center;
  margin: 0px 12px;
}
.m-popup .popup-title .guideline .link-guideline {
  white-space: nowrap;
  padding-left: 5px;
  color: #0075c0;
  font-size: 13px;
}
.m-popup .popup-title .guideline .link-guideline:hover {
  text-decoration: underline;
  cursor: pointer;
}
/*content*/
.m-popup .popup-content {
  width: 100%;
  height: calc(100% - 122px);
  overflow: auto;
}
.m-popup .popup-content .form {
  background-color: transparent;
  width: 100%;
  padding: 8px 24px 20px 24px;
  display: flex;
  position: sticky;
  left: 0px;
  z-index: 10;
}
.m-popup .popup-content .form .total-payment {
  color: #111;
  float: right;
}
.m-popup .popup-content .form .total-payment .header{
  float: right;
}
.m-popup .popup-content .form .total-payment .total{
  font-size: 36px;
  font-weight: 700;
}
.input-right .row,
.input-left .row{
  margin-bottom: 12px;
}
.m-popup .popup-content .form .form-input {
  display: flex;
}
.m-popup .popup-content .form .input-left {
  padding-right: 16px;
  border-right: 1px solid #ccc;
}
.m-popup .popup-content .form .input-right {
  padding-right: 16px;
  padding-left: 16px;
}
.m-popup .popup-content .form .input-left .left {
  padding-right: 12px;
}
.m-popup .popup-content .table-area {
  background-color: #fff;
  width: 100%;
}
.m-popup .popup-content .table-area .hidden-right {
  background-color: #fff !important;
}
.m-popup .popup-content .table-area-1 {
  background-color: #fff;
  width: 100%;
  padding: 8px 24px 0px 24px;
  /* position: sticky; */
  left: 0;
}
.m-popup .popup-content .table-area-1 .title {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding-bottom: 16px;
}
.m-popup .popup-content .table-area-1 .title .type-currency {
  display: flex;
  align-items: center;
  justify-content: end;
}
.m-popup .popup-content .table-area-1 .title .link {
  white-space: nowrap;
  color: #0075c0;
  font-size: 13px;
  text-decoration: underline;
  cursor: pointer;
  font-weight: 700;
  font-family: Notosans-bold;
}
.m-popup .popup-content .table-area-1 .title .link:hover {
  color: #2ca01c;
}
.m-popup .popup-content .file-attach {
  position: sticky;
  left: 0px;
  background-color: #fff;
  padding: 12px 24px 10px 24px;
  bottom: 0px;
}
.m-popup .popup-content .file-attach .button-area {
  display: flex;
  align-items: center;
  
}
.m-popup .popup-content .file-attach .button-area .add-row {
  height: 24px;
  margin-right: 12px;
}
.m-popup .popup-content .file-attach .button-area button {
  background-color: transparent;
  border: 1px solid #ccc;
  height: 100%;
  border-radius: 2px;
  color: #111;
  font-size: 12px;
  font-weight: 600;
  padding: 2px 16px;
  cursor: pointer;
}
.m-popup .popup-content .file-attach .button-area button:hover {
  background-color: #eceef1;
}
.m-popup .popup-content .file-attach .button-area button:active {
  background-color: #bbb;
}
.m-popup .popup-content .file-attach .button-area .delete-all {
  height: 24px;
}
.m-popup .popup-content .file-attach .attach .head {
  display: flex;
  align-items: center;
  padding-bottom: 8px;
   margin-top: 8px;
}
.m-popup .popup-content .file-attach .attach .ic-attach {
  margin-right: 8px;
 
}
.m-popup .popup-content .file-attach .attach .drag-target {
  min-height: 50px;
  border: 1px solid #bbb;
  display: flex;
  align-items: center;
  justify-content: center;
  max-width: 333px;
  border-radius: 2px;
}
/**footer */
.m-popup .popup-footer {
  background-color: #38393d;
  height: 50px;
  width: 100%;
  min-height: 50px;
  padding: 0 16px;
}
.m-popup .popup-footer {
  background-color: #38393d;
  height: 50px;
  width: 100%;
  min-height: 50px;
  padding: 0 16px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.m-popup .popup-footer .save-btn {
  margin: 0px 12px;
}
.m-popup .popup-footer .m-left {
  display: flex;
  align-items: center;
}
.m-popup .popup-footer .m-right {
  display: flex;
  align-items: center;
}

.ic-recent-log{
  background-position: -648px -144px;
}
.ic-help-dialog{
  background-position: -89px -144px
}
.ic-guideline{
  background-position: -984px -144px;
}
.ic-setting{
  background-position: -32px -144px;
}
.ic-exit-dialog{
  background-position: -144px -144px;
}
.lable{
  font-weight: 700;
  color: #111;

}
/* Table */
.table-container{
  position: sticky;
}
table {
  border-spacing: 0;
  width: 100%;
}
table thead{
  position: sticky;
  top: 0;
  z-index: 100;
}
table tr {
  width: 100%;
}
table tr th {
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
}
table tfoot th{
  border:none;
  text-align: right;
}
.option{
    min-width: 50px;
}
.option-icon{
    background-position: -464px -312px;
}
</style>
<style>
.type-money{
  z-index: 101;
}
</style>

