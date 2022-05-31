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
              :disabled="showMode"
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
          <div class="exit" :title="'Esc'" @click="closePopup()">
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
                      :apiColumn="apiColumn"
                      :tableName="'VendorCombobox'"
                      :id="'VendorId'"
                      :name="'VendorName'"
                      :code="'VendorCode'"
                      :componentDes="'Mã đối tượng'"
                      :dataValue="payment.VendorName"
                      ref="VendorId"
                      v-model="payment.VendorId"
                      @change="handleChangeVendor"
                      :isLazyloading="'true'"
                      :disabled="showMode"
                      :propValue="'VendorCode'"
                      
                    />
                  </div>
                  <div class="right col c-7">
                    <div class="lable"><label for="">Tên đối tượng</label></div>
                    <BaseInput v-model="vendorName" ref="vendorName" :disabled="showMode" />
                  </div>
                </div>
                <div class="row">
                  <div class="left col c-5">
                    <div class="lable"><label for="">Người nhận</label></div>
                    <BaseInput v-model="payment.ReceiverName" :disabled="showMode"/>
                  </div>
                  <div class="right col c-7">
                    <div class="lable"><label for="">Địa chỉ</label></div>
                    <BaseInput v-model="payment.Address"  :disabled="showMode"/>
                  </div>
                </div>
                <div class="row">
                  <div class="right col c-12">
                    <div class="lable"><label for="">Lý do chi</label></div>
                    <BaseInput :placeholder="'Chi tiền cho'" v-model="handleChangeDescriptionPayment" :disabled="showMode"/>
                  </div>
                </div>
                <div class="row">
                  <div class="left col c-5">
                    <div class="lable"><label for="">Nhân viên</label></div>
                    <BaseCombobox
                      :Api="'http://localhost:5158/api/v1/Employees/filter'"
                      :apiColumn="apiColumn"
                      :tableName="'EmployeeCombobox'"
                      :id="'EmployeeId'"
                      :name="'EmployeeName'"
                      :code="'EmployeeCode'"
                      :componentDes="'Nhân viên mua hàng'"
                      ref="EmployeeId"
                      v-model="payment.EmployeeId"
                      :dataValue="payment.EmployeeName"
                      :disabled="showMode"
                      :isLazyloading="true"
                    />
                  </div>
                  <div class="right col c-6">
                    <div class="lable"><label for="">Kèm theo</label></div>
                    <div style="display: flex; align-items: center">
                      <div class="m-col-7">
                        <BaseInput :disabled="showMode" :type="'number'" :placeholder="'Số lượng'" v-model="payment.AttachDocumentAmount" :searchClass="'text-right'"/>
                      </div>
                      <div class="description" style="margin-left: 6px; white-space:nowrap;">
                        chứng từ gốc
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="input-right">
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
                      :disabled="showMode"
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
                      :disabled="showMode"
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
                    <BaseInput v-model="payment.PaymentCode" 
                      ref="PaymentCode" 
                      :componentDes="'Số phiếu chi'" 
                      :disabled="showMode"
                      :rules="['required']"
                    />
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
            <div class="type-money-container d-flex">
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
                    :disabled="showMode"
                    @change="handleChangeTypeMoney"
                  />
                  
                </div>
              </div>
              <div class="type-currency" v-if="payment.CurrencyId == 1">
                <div style="padding: 0px 10px 0px 20px">Tỷ giá</div>
                <div class="m-col-6">
                  <BaseInput :searchClass="'type-money text-right'" :type="'currency'" v-model="handleChangeExchangeRate" :disabled="showMode"/>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="table-area" :class="{'table-disabled':showMode}">
          <div class="table">
            <!-- <MTableEditable :deleteFunc="true"/> -->
            <div class="table-container">
              <div id="table">
                <table>
                    <thead>
                        <tr>
                          <th class="text-center" >
                              <div class="stt text-center">
                                #
                              </div>
                          </th>
                          <th>
                            <div class="column" style="min-width:400px">Diễn giải</div>
                          </th>
                          <th>
                            <div class="column">TK nợ</div>
                          </th>
                          <th>
                            <div class="column">Tk có</div>
                          </th>
                          <th>
                            <div class="column text-right">Số tiền</div>
                          </th>
                          <th>
                            <div class="column">Đối tượng</div>
                          </th>
                          <th>
                            <div class="column">Tên đối tượng</div>
                          </th>
                          <th class="option"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(value,index) in paymentDetails" :key="index">
                          <td class="text-center stt">
                              {{index+1}}
                          </td>
                          <td>
                              <span v-if="showMode">{{value.DescriptionPayment}}</span>
                              <BaseInput v-else v-model="value.DescriptionPayment" @click="filterPaymentDetail(index)" :disabled="showMode"/>
                          </td>
                          <td>
                              <span v-if="showMode">{{accounts.find((x)=> x.Id == value.DebitAccountId)['Code']}}</span>
                              <BaseCombobox 
                                v-else
                                :valueOption="accounts"
                                :id="'Id'"
                                :name="'Value'"
                                :code="'Code'"
                                :propValue="'Code'"
                                v-model="value.DebitAccountId"
                                @click="filterPaymentDetail(index)"
                                :disabled="showMode"
                              />
                          </td>
                          <td>
                              <span v-if="showMode">{{accounts.find((x)=> x.Id == value.CreditAccountId)['Code']}}</span>
                              <BaseCombobox 
                                v-else
                                :valueOption="accounts"
                                :id="'Id'"
                                :name="'Value'"
                                :code="'Code'"
                                :propValue="'Code'"
                                v-model="value.CreditAccountId"
                                @click="filterPaymentDetail(index)"
                                :disabled="showMode"
                              />
                          </td>
                          <td class="column-number">
                              <span v-if="showMode" class="text-right">{{value.CashAmount}}</span>
                              <BaseInput 
                                v-else
                                :isNumber="'true'" 
                                :type="'currency'" 
                                v-model="value.CashAmount" 
                                :searchClass="'text-right'" 
                                @blur="getTotalMoney"
                                @click="filterPaymentDetail(index)"
                                :disabled="showMode"
                              />
                          </td>
                          <td>
                              <span v-if="showMode">{{value.VendorCode}}</span>
                              <BaseCombobox
                                v-else
                                :Api="'http://localhost:5158/api/v1/Vendors/filterAdvanced'"
                                :apiColumn="apiColumn"
                                :tableName="'VendorCombobox'"
                                :id="'VendorId'"
                                :name="'VendorName'"
                                :code="'VendorCode'"
                                :componentDes="'Mã đối tượng'"
                                :dataValue="value.VendorName"
                                :propValue="'VendorCode'"
                                ref="VendorId"
                                v-model="value.VendorId"
                                @click="filterPaymentDetail(index)"
                                @change="handleChangeVendorPaymentDetails($event,index)"
                                :disabled="showMode"
                                :isLazyloading="'true'"
                                
                              />
                              <!-- <BaseCombobox
                                
                                :Api="vendorsApi"
                                
                                :id="'VendorId'"
                                :name="'VendorName'"
                                :code="'VendorCode'"
                                :componentDes="'Mã đối tượng'"
                                ref="VendorId"
                                v-model="value.VendorId"
                                @click="filterPaymentDetail(index)"
                                @change="handleChangeVendorPaymentDetails($event,index)"
                                :disabled="showMode"
                              /> -->
                          </td>
                          <td>
                              <span v-if="showMode">{{value.VendorName}}</span>
                              <BaseInput v-else :disabled="'true'" @click="filterPaymentDetail(index)" v-model="value.VendorName"/>
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
              </div>
              
              <div class="file-attach">
                <div class="button-area">
                  <div class="add-row">
                    <button @click="addRowTableAccount()" :disabled="showMode">Thêm dòng</button>
                  </div>
                  <div class="delete-all">
                    <button :disabled="showMode" @click="deleteRowTableAccount()">Xóa hết dòng</button>
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
        <div class="m-left" >
          <div class="cancel-btn" v-if="!showMode">
            <BaseButton
              :classBtn="'btn-default btn-sq btn-no-op'"
              :content="'Hủy'"
              @click="closePopup()"
              @keyup.tab="handleTabEvent($event)"
            />
          </div>
        </div>
        <div class="m-right" >
          <div class="save-btn" v-if="!showMode">
            <BaseButton
              :classBtn="'btn-default btn-sq btn-no-op'"
              :content="'Cất'"
              @click="handleSave()"
              :title="'Ctrl+S'"
            />
                
          </div>
          <div class="save-and-print-btn" v-if="!showMode">
            <BaseButton
              :classBtn="'btn-default btn-sq btn-no-op btn-primary'"
              :content="'Cất và in'"
              @click="handleSaveAndContinue()"
              :title="'Ctrl+Shift+S'"
            />
          </div>
          <div class="save-and-print-btn" v-if="showMode">
            <BaseButton
              :classBtn="'btn-default btn-sq btn-no-op btn-primary'"
              :content="'Đóng'"
              @click="closePopup()"
              :title="'Esc'"
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
      totalMoney:'0,0',
      //Api
      paymentsApi : Api.payments,
      paymentDetailsApi:Api.paymentDetailsByPaymentId,
      vendorsApi:Api.vendors,
      employeesApi:Api.getEmployeeById,
      apiColumn: Api.getColumnOption,
      //Hash Table
      filterTypePayments:DB.filterTypePayments,
      accounts:DB.accounts,
      typeMoney:DB.typeMoney,

      showMode:false,
      exchangeRate:1,

      validateProp:['PaymentCode']
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
        // this.payment.AccountingDate = new Date(date);
        // this.payment.AccountingDate = this.payment.AccountingDate.toDateString();
       
        try {
          
          if (
            this.payment.AccountingDate === this.payment.PaymentDate
          ) 
          {
            this.payment.PaymentDate = new Date(date).toDateString();
          }
          console.log(this.payment)
          this.payment.AccountingDate = new Date(date);
          this.payment.AccountingDate = this.payment.AccountingDate.toDateString();
          
        } catch (error) {
          console.log(error)
          return new Date().toDateString();
        }
      
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
    handleChangeExchangeRate:{
      get(){
        return this.payment.ExchangeRate
      },
      set(number){
        this.payment.ExchangeRate = number
        this.getTotalMoney()
      }
    },
    handleChangeDescriptionPayment:{
      get(){
        return this.payment.DescriptionPayment
      },
      set(value){
        this.payment.DescriptionPayment = value
        this.paymentDetails.forEach((x)=>{
          x.DescriptionPayment = value
        })
      }
    }
  },
  created(){
    this.payment = this.model
    
    if(this.payment.IsShow != undefined){
      this.showMode = true
      this.totalMoney = this.formatNumber(this.payment.TotalMoney)
    }
    this.vendorName = this.payment.VendorName
    if(this.payment.PaymentCode == undefined){
      this.setDefault()
    }
    
    
    this.loadPaymentDetail()
  },
  mounted() {
    if(this.$refs.PaymentCode != undefined) this.$refs.PaymentCode.focus()
    window.addEventListener("keyup", this.handleKeyEvent);
    window.addEventListener("keydown", this.handleKeyEventDown);
  },
  //Bỏ lắng nghe xự kiện
  beforeUnmount() {
    window.removeEventListener("keyup", this.handleKeyEvent);
    window.removeEventListener("keydown", this.handleKeyEventDown);
  },
  watch:{
    vendorName:function(){
      this.payment.ReceiverName = this.vendorName
      var value = `Chi tiền cho ${this.vendorName}`
      this.payment.DescriptionPayment = value
      this.paymentDetails.forEach((x)=>{
          x.DescriptionPayment = value
        })
    }
  },
  methods: {
    /**
    * Mô tả: Xóa dòng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 30/05/2022
    */
    deleteRowTableAccount(){
      this.showPopupInfo(this.typePopupName.warningConfirm,'Bạn có thực sự muốn xóa tất cả các dòng đã nhập không?','confirmDeletePaymentDetail');
    },
    /**
     * Mô tả: Xử lý sự kiện key up
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleKeyEvent(e) {
      console.log(e)
      if (e.ctrlKey && e.keyCode == 83) {
        this.handleSave();
      }
      if(e.shiftKey && e.ctrlKey && e.keyCode == 83){
        e.preventDefault()
        this.handleSaveAndContinue()
      }
      if(e.keyCode == 27){
        this.closePopup()
      }

    },

    /**
     * Mô tả: Xử lý sự kiện key down
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleKeyEventDown(e) {
      if (e.ctrlKey && e.keyCode == 83) {
        e.preventDefault()
      }
      if(e.shiftKey && e.ctrlKey && e.keyCode == 83){
        e.preventDefault()
        
      }
      if(e.keyCode == 27){
        e.preventDefault()
      }
    },
    /**
    * Mô tả: Xử lý thay đổi loại tiền
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 27/05/2022
    */
    handleChangeTypeMoney(value){
      if(value.Id === 2) {
          this.payment.ExchangeRate = 1
        
      }
      if(value.Id == 1 && (!this.payment.ExchangeRate || this.payment.ExchangeRate == 1)){
        
        this.payment.ExchangeRate = 23188
      }
      this.getTotalMoney()
    },

    /**
    * Mô tả: Chuyển định dạng số
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
    * Mô tả: Lấy tổng tiền payment detail
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 26/05/2022
    */
    getTotalMoney(){
      this.totalMoney = this.paymentDetails.reduce((accumulator,value)=>{
        return accumulator + value.CashAmount
      },0)
      
      if(!!this.totalMoney) {
        var exchangeRate = !!this.payment.ExchangeRate?this.payment.ExchangeRate:1
        this.totalMoney = this.totalMoney*exchangeRate
        this.payment.TotalMoney = this.totalMoney
        
        this.totalMoney = this.formatNumber(this.totalMoney)
      }
      
      else this.totalMoney = '0,0'
    },
    
    /**
     * Mô tả: Xử lý sự kiện confirm popup
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    handleConfirmBtn(isConfirm, key) {
      //Popup thông báo validate
      if (key === "error" || key === "validate") {
        this.isShowPopupInfo = false;
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
      
      
      if(key === 'confirmDeletePaymentDetail' ){
        this.isShowPopupInfo = false;
        if(isConfirm) this.paymentDetails = [{
          CreditAccountId: 1
        }]
      }
    },
    /**
    * Mô tả: Xóa bỏ những phiếu chi chi tiết rỗng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    filterPaymentDetail(index){
      if(index == 0 ){
        var newPaymentDetails = []
        this.paymentDetails = this.paymentDetails.filter((x)=>{
          var ok = !this.validatePaymentDetail(x)
          
          if(ok) {
            newPaymentDetails.push(x)
            return true
          }
          else return false
                
        })
        if(newPaymentDetails.length == 0) this.paymentDetails = [{}]
        else this.paymentDetails = newPaymentDetails
        return newPaymentDetails
      }
            
    },
    /**
    * Mô tả: Validate Payment detail
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    validatePaymentDetail(paymentDetail){
      console.log(paymentDetail)
      var keys = Object.keys(paymentDetail)
      console.log(keys)
      if(keys.length == 0) {
        return true
      }
      // if(paymentDetail.DebitAccountId == undefined) return "Tài khoản nợ không được để trống."
      // if(!paymentDetail.CreditAccountId == undefined) return "Tài khoản có không được để trống."
      return undefined
    },
    /**
    * Mô tả: Xử lý thay đổi đối tượng payment master
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    handleChangeVendor(value){
      this.vendorName = value.VendorName
      if(this.paymentDetails.length >0){
        this.paymentDetails[0].VendorId = value.VendorId
        this.paymentDetails[0].VendorName = value.VendorName
      }
    },
    handleChangeVendorPaymentDetails(value,index){
      this.paymentDetails[index].VendorName = value.VendorName
    },

    /**
    * Mô tả: Chỉnh sửa mặc định các giá trị phiếu chi
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 23/05/2022
    */
    setDefault(){
      //Loại phiếu = chi khác
      this.payment.PaymentType = 7

      //Loại tiền vnd
      this.payment.CurrencyId = 2
      //Tỷ lệ chuyển đổi = 1
      this.payment.ExchangeRate = 1
      //Ngày hạch toán
      this.payment.AccountingDate = new Date().toDateString()
      //Ngày chứng từ
      this.payment.PaymentDate = new Date().toDateString()
      //Diễn giải
      this.payment.DescriptionPayment = 'Chi tiền cho '

      this.paymentDetails = [{
        CreditAccountId: 1
      }]
      //Lấy mã mới
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
          }
        })
        .catch((e) => {
          console.log("e");
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
            this.paymentDetails.forEach((p) =>{
              p.CashAmount = this.formatNumber(p.CashAmount)
            })
          }
        })
        .catch((e) => {
          console.log("e");
          // this.isLoading = false;
          // this.loadHandle([]);
        });
      }
    },
    /**
    * Mô tả: Thêm dòng vào bảng tài khoản ngân hàng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 20/05/2022
    */
    addRowTableAccount(){
      try{
        var length = this.paymentDetails.length
        var obj = !!length ? Object.assign({},this.paymentDetails[length-1]) : {}
        this.paymentDetails.push(obj)
        this.getTotalMoney()

      // console.log(obj)
      }
      catch(e){
        this.paymentDetails = [{}]
      }
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
     * Mô tả: Validate các trường dữ liệu
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 18/04/2022
     */
    validate() {
      var length = this.validateProp.length
      var errorMsg = '';

      //Mã phiếu chi trống
      for(var i = 0 ;i < length;++i){
        var propName = this.validateProp[i]
        if(this.$refs[propName] != undefined){
          var msg = this.$refs[propName].validate()
          if(errorMsg === '' && !!msg ) errorMsg = msg
        }
      }
      // console.log(this.payment.AccountingDate < this.payment.PaymentDate)
      if(this.payment.AccountingDate < this.payment.PaymentDate){
        var msg = 'Ngày chứng từ không được lớn hơn ngày hạch toán.'
        if(errorMsg === '' && !!msg ) errorMsg = msg
      }

      //Chứng từ chi tiết rỗng
      var paymentDetails = this.filterPaymentDetail(0)
      if(paymentDetails != undefined && paymentDetails.length > 0) this.payment.paymentDetails = paymentDetails
      else {
        var msg = this.popupMsg.paymentDetailNotFound
        if(errorMsg === '' && !!msg ) errorMsg = msg
      }

      if(errorMsg !== ''){
        console.log(errorMsg)
        this.showPopupInfo(this.typePopupName.error,errorMsg,'error');
        return false
      }
      
      else return true
    },
    /**
     * Mô tả: Lưu thông tin nhân viên
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 12/04/2022
     */
    save(isContinue) {
       
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
              console.log(e.response.data);
              if (keys.length > 0) {
                var errorMsg = e.response.data.data[keys[0]];
                this.errorRefsName = keys[0];
                console.log(errorMsg)
                this.showPopupInfo(this.typePopupName.warningNotify, errorMsg, "validate");
              } 
              else {
                this.showPopupInfo(
                  this.typePopupName.warningNotify,
                  this.errorMsg.exceptionMessage,
                  "validate"
                );
              }
            }
            catch(e){
              this.showPopupInfo(
                this.typePopupName.warningNotify,
                this.errorMsg.exceptionMessage,
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
.input-right{
  max-width: 250px;
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
  padding: 12px 24px 10px 0px;
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
  margin-bottom: 5px;

}
/* Table */
.table-container{
  width: 100%;
  position: sticky;
  padding:8px 24px 0px 24px;
}
.table{
  width: 100%;
}
#table{
  width: 100%;
  
  overflow-x: auto;
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
table tr th .column{
  min-width: 50px;
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
  
}
table tr .stt{
  min-width: 30px!important;
  width: 30px!important;
}
table tfoot th{
  border:none;
  text-align: right;
}
.column-number{
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
  background-color: #EDEEF0;
  width: 100px;
}
.action{
  background-color: #fafafa;
}
.disabled{
  display: inline-block!important;
  background-color: transparent;
}
</style>

