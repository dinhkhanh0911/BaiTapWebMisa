<template>
  <div class="input-container" :class="[searchClass,{ error: errorInput }]" ref="input" :title="titleError">
    <input
      :placeholder="placeholder"
      
      v-model="value"
      :disabled="disabled"
      @input="onInputHandle($event.target.value)"
      @mousemove="onMouseMoveEvent($event)"
      
      @blur="onBlur($event.target.value)"
    />
    <div class="search-btn mi mi-16"></div>
    <!-- <BaseTooltip :top="topTooltip" :left="leftTooltip" v-if="isShowTooltip" :valueTooltip="valueTooltip"/> -->
  </div>
</template>

<script>
import Validator from '@/assets/js/validator';
/**
 * Mô tả: Components Input
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 07/04/2022
 */
export default {
  props: {
    type: {
      type: String,
      default: "text",
    },
    placeholder: {
      type: String,
      default: "",
    },
    classInput: {
      type: String,
      default: "",
    },
    modelValue: {
      type: [String,Number],
      default: "",
    },
    timer: {
      type: Number,
      default: 0,
    },
    searchClass: {
      type: String,
      default: "",
    },
    componentDes: {
      type: String,
      default: "",
    },
    disabled:{
      type:[Boolean,String],
      default:false
    },
    rules:{
      type:Array,
      default:[]
    }
  },
  data() {
    return {
      value: "",
      errorInput: false,
      timeOut: null,
      titleError: "",
      valueNumber:""
    };
  },
  created(){
    this.value = this.modelValue
  },
  watch:{
    modelValue:function(){
      if(this.type != 'currency' && this.type != 'number') this.value = this.modelValue
    },
  },
  methods: {
    validate(){
      for(var i=0;i<this.rules.length;++i){
        var rule = this.rules[i]
        var ok = Validator[rule](this.value)
        if(!ok){
          this.titleError = `${this.componentDes} ${this.validateMsg[rule]}`
          this.errorInput = true
          return this.titleError
        }
      }
      return false
    },
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
     * Mô tả:  Xử lý dữ liệu khi nhập input
     * @param: dữ liệu input
     * @return: Gửi dữ liệu lên component cha
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 15/04/2022
     */
    onInputHandle(value) {
      this.title = ""
      this.errorInput = false
      switch(this.type){
        case 'currency':
          this.handleCurrency(value)
          break
        case 'number':
          this.handleNumber(value)
          break
        default:
          this.handleText(value)
          break
      }
    },
    //Xử lý dạng tiền tệ
    handleCurrency(value){
       
      //Xóa bỏ định dạng số
      value = value.toString().replaceAll('.','')
      //Kiểm tra ký tự nhập vào đúng định dạng chưa
      var ok = Validator.numeric(value)
      if(ok){
        // value = parseFloat(value)
        //Tách chuỗi chưa dấu ,
        var number = value.split(',')
        console.log('number',number)
        //Chuyển sang dạng số phần nguyên
        var numberInteger = number[0] !== '' ? this.formatNumber(parseInt(number[0])) : 0
        //Không có phần dư
        if(number.length == 1) {
          this.value = `${numberInteger}`
          
        }
        //Chỉ nhập dấu ,
        if(number.length>1 && number[0] ==='') {
          this.value = `${numberInteger},`
        
        }
        //Nhập dấu , không nhập phần dư
        if(number.length>1 && number[1] !=='') {
          this.value = `${numberInteger},${number[1]}`
        
        }
        console.log(this.value)
        this.valueNumber = this.value

        //Chuyển sang biến kiểu float
        var numberFormat = this.value.toString().replaceAll('.','')
        numberFormat = numberFormat.toString().replaceAll(',','.')
        numberFormat = parseFloat(numberFormat)
        this.$emit("update:modelValue", numberFormat)
      }
      else{
        this.value = this.valueNumber
      }
    },
    //Xử lý khi type input là số
    handleNumber(value){
      //Xóa bỏ định dạng số
      value = value.toString().replaceAll('.','')
      //Kiểm tra ký tự nhập vào đúng định dạng chưa
      var ok = Validator.interger(value)
      if(ok){
        try{
          //Chuyển từ string sang dạng số nguyên
          var numberFormat = parseInt(value)
          //Nếu số bằng null thì hiển thị chuỗi rỗng không thi hiển thị số
          this.value = !!numberFormat ? this.formatNumber(numberFormat) :''

          //Gán biến giá trị hiện tại
          this.valueNumber = this.value

          
          this.$emit("update:modelValue", !numberFormat ? null : numberFormat)
        }
        catch(e){
          this.$emit("update:modelValue", null)
        }
      }
      else{
        //Nếu validate lỗi lấy giá trị cũ
        this.value = this.valueNumber
      }
    },
    handleText(value){
      if (this.timer === 0) {
        (this.error = false), this.$emit("update:modelValue", value);
      } else {
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() => {
          (this.error = false), this.$emit("update:modelValue", value);
        }, this.timer);
      }
    },

    /**
     * Mô tả: Focus vào component
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 07/04/2022
     */
    focus() {

      this.$refs["input"].querySelector("input").focus();
    },

    select(){
      this.$refs["input"].querySelector("input").select();
    },

    /**
     * Mô tả:  Lăng nghe khi kiện mouse move
     * Khi sự kiện xảy ra tắt bỏ sự kiên component cha
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 07/04/2022
     */
    onMouseMoveEvent(e) {
      e.stopPropagation();
    },

    onBlur(value){
      this.validate()
      this.$emit('blur',true)
    }
  },
};
</script>
<style scoped>
.text-center input{
    text-align: center!important;
}
.text-left input{
    text-align: left!important;
}
.text-right input{
    text-align: right!important;
}
</style>
<style scoped>
.input-container{
    width: 100%;
}
.input-container input{
    width: 100%;
    height: 32px;
    outline: none;
    border-radius: 2px;
    border: 1px solid #babec5;
    padding: 0 12px;
}
.input-container input:focus{
    border:1px solid #2ca01c;
}
.error input{
    border: 1px solid red;
}
.search-container{
    display: flex;
    align-items: center;
    border-radius: 2px;
    position: relative;
    width: 100%;
    width: unset;
}
.search-container input{
    padding-right: 40px;
}
.search-btn{
    display: none;
}
.search-container .search-btn{
    display: block;
    background-position: -992px -360px;
    position: absolute;
    top: 8px;
    right: 12px;
    cursor: pointer;
}
.no-border input{
    border: none!important;
}

</style>
