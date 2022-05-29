<template>
  <div class="combobox-container" ref="combobox" :title="titleError" :class="className">
    <div class="value-combobox" ref="valueCombobox" >
      <input type="text" :value="valueCB" :placeholder="placeholder" :disabled="disabled"/>
    </div>
    <div class="action" >
      <button @click="onClickAction()" :disabled="disabled">
        <div class="mi mi-16"></div>
      </button>
    </div>
    <div
      class="combobox-option"
      ref="comboboxAction"
      v-show="isShow"
      :class="{ dropUp: !isDropDown }"
      v-bind:style="{ left: leftAction + 'px','min-width': widthAction + 'px',}"
      @scroll="lazyLoad()"
    >
      <table v-if="defaultTable" ref="baseTable" :class="{ 'table-one-column': isOneColumn }">
        <thead>
          <tr>
            <th>Mã đơn vị</th>
            <th>Tên đơn vị</th>
          </tr>
        </thead>
        <tbody ref="tbody">
          <tr
            v-for="(value, index) in values"
            :key="index"
            @click="handleSelect(value, index)"
            :class="{ active: (value[id] === modelValue) }"
          >
            <td>{{ value[code] }}</td>
            <td>{{ value[name] }}</td>
          </tr>
        </tbody>
      </table>
      <table v-else ref="baseTable" :class="{ 'table-one-column': isOneColumn }">
        <thead>
          <tr>
            <th v-for="(column, index) in columnsDescription" :key="index" :class="column.columnClass" class="resizeable" 
            > {{column.viewColumnName}}</th>
          </tr>
        </thead>
        <tbody ref="tbody">
          <tr
            v-for="(value, index) in values"
            :key="index"
            @click="handleSelect(value, index)"
            :class="{ active: (value[id] === modelValue) }"
          >
            <td v-for="(column, index) in columnsDescription" :key="index" :class="column.columnClass"
            > 
              {{value[column.columnField]}}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
/**
 * Mô tả:  Combobox
 * Created by: Đinh Văn Khánh - MF1112
 * Created date: 14/04/2022
 */
import axios from "axios";
import { nextTick } from '@vue/runtime-core';
export default {
  data() {
    return {
      isShow: false,
      valueCB: "",
      values: [],
      titleError: "",
      error: false,

      // Chỉnh sửa bảng dữ liệu
      defaultTable:true,
      columnsDescription:[],
      //position action
      topAction:0,
      leftAction:0,
      widthAction:0,
      postitionBind:'top',

      //lazy loading
      pageSize:10,
      currentPage:1,

      propShow:""
    };
  },
  props: {
    Api: {
      type: String,
      default: "",
    },
    modelValue: {
      type: [String,Number],
      default: "",
    },
    id: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: "",
    },
    code: {
      type: String,
      defaule: "",
    },
    componentDes: {
      type: String,
      default: "",
    },
    isDropDown: {
      type: Boolean,
      default: true,
    },
    valueOption: {
      type: Array,
      default: [],
    },
    isOneColumn: {
      type: Boolean,
      default: false,
    },
    placeholder:{
      type:String,
      default:''
    },
    className:{
      type:String,
      default:""
    },
    isLazyloading:{
      type:Boolean,
      default:false
    },
    dataValue:{
      type:String,
      default:''
    },
    disabled:{
      type:Boolean,
      default:false
    },
    apiColumn:{
      type:String,
      default:""
    },
    tableName:{
      type:String,
      default:""
    },
    propValue:{
      type:String,
      default:""
    }
  },
  created() {
    if(!!this.propValue) this.propShow = this.propValue
    else this.propShow = this.name 
    console.log(this.propShow)
    if(!this.apiColumn) this.loadData()
    else this.loadColumnTable()

    
  },   
  watch: {
    /**
     * Mô tả:  Lăng nghe thay đổi giá trị model
     * Nếu model rỗng thì gán giá trị input bằng rỗng
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    modelValue: function () {
      
      if (this.modelValue === "") {
        this.valueCB = "";
      }
      else {
        this.valueCB = this.values.find(x => x[this.id] === this.modelValue)[this.propShow]
        
      }
    },
    Api:function(){
      this.valueCB = ""
      this.loadData()
    }
  },
  methods: {
    /**
    * Mô tả: Load cột của bảng
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 09/05/2022
    */
    loadColumnTable(){
      this.defaultTable = false
      axios
        .get(`${this.apiColumn}/${this.tableName}`)
        .then((response) => {
          if (response.status === 200) {
            this.columnsDescription = response.data.columns.filter(column => column.isShow === "True")
            
            this.loadData()
          }
        })
        .catch((e) => {
          console.log(e);
        });
    },
    
    /**
    * Mô tả: Xử lý lazy loading
    * Created by: Đinh Văn Khánh - MF1112
    * Created date: 29/05/2022
    */
    lazyLoad() {
      //nếu lazyLoading bằng true thì mới loading.
      if (this.isLazyloading) {
        //độ cao dropdown
        let dropdownHeight = this.$refs.comboboxAction.clientHeight;
        //phần đã bị tràn khỏi top.
        let scrollTop = this.$refs.comboboxAction.scrollTop;
        //toàn bộ độ cao có thể scroll
        let scrollHeight = this.$refs.comboboxAction.scrollHeight;
        //vị trí hiện tại của con lăn
        var total = dropdownHeight + Math.floor(scrollTop);
        //nếu bằng scrollHeight thì là đang ở bottom
        if (total >= (scrollHeight-10)) {
          console.log("đang ở bottom");
          this.currentPage++
          this.loadMoreData()
          total = 0;
          //emit tới cha load tiếp thông tin, truyền vào dữ liệu filter nếu có để lazy load tiếp thông tin đó.
          // this.$emit("eLoadNext", this.selectedText);
        }
      }
    },
    onClickAction(){
      var combobox = this.$refs['combobox'].getBoundingClientRect()
      var action = this.$refs['comboboxAction']

      //ví trí bên trái của input
      this.leftAction = parseInt(combobox.left )
      // Vị trí bên trên của input
      this.topAction = parseInt(combobox.top+36)
      this.widthAction = parseInt(combobox.width)
      this.isShow =! this.isShow
      // action.style.top = this.topAction +'px'
      const me = this
      nextTick(function(){
        var documentWidth = document.body.clientWidth
        var widthCombobox = action.getBoundingClientRect().width
        var temp = widthCombobox + me.leftAction - documentWidth
        if(temp > 0){
          me.leftAction = me.leftAction - temp - 24
        }
        if(!me.isDropDown) {
          action.style.bottom = screen.height - me.topAction - 66+'px'
        }
        else action.style.top = me.topAction +'px'
      })
    },
    loadMoreData(){
        axios
          .get(`${this.Api}?currentPage=${this.currentPage}&pageSize=20`)
          .then((response) => {
            if (response.status === 200) {
              console.log(response)
              
              this.values = [...this.values,...response.data.List]
              console.log(response.data.List)
              console.log(this.values);
              //Nếu modelValue khác rỗng và số giá trị mảng option combobox lớn hơn không thì gán
              // giá trị input bằng giá trị value tương ứng
              if (this.modelValue !== "" && this.values.length > 0) {
                var value = this.values.find(
                  (item) => item[this.id] == this.modelValue
                );
                if (value != undefined) {
                  this.valueCB = value[this.name];
                }
                
              }
            }
          })
          .catch((e) => {
            console.log(e);
          });
    },
    loadData(){

      //Trường hợp combobox không cho option lazy loading
      if(!this.isLazyloading){
        //Lấy dữ liệu combobox
        //Nếu Api có, gọi Api lấy giữ liệu
        if (this.Api != "") {
          axios
            .get(this.Api)
            .then((response) => {
              if (response.status === 200) {
                this.values = response.data;
                //Nếu modelValue khác rỗng và số giá trị mảng option combobox lớn hơn không thì gán
                // giá trị input bằng giá trị value tương ứng
                if (!!this.modelValue && this.values.length > 0) {
                  var value = this.values.find(
                    (item) => item[this.id] == this.modelValue
                  );
                  //Nếu dữ liệu lấy có chứa model
                  if (value != undefined) {
                    this.valueCB = value[this.propShow];
                  }
                  //Nếu dữ liệu mặc định tồn tại
                  else if(!!this.dataValue.trim()){
                    console.log(this.dataValue)
                    this.valueCB = this.dataValue
                  }
                }
              }
            })
            .catch((e) => {
              console.log(e);
            });
        } 
        else {
          this.values = this.valueOption;
          /**
           * Nếu Api không có, lấy mảng trong prop
           * Nếu modelValue khác rỗng và số giá trị mảng option combobox
           * lớn hơn không thì gán giá trị input bằng giá trị value tương ứng
           */
          if (!!this.modelValue && this.values.length > 0) {
            var value = this.values.find(
              (item) => item[this.id] == this.modelValue
            );
            //Nếu dữ liệu lấy có chứa model
            if (value != undefined) {
              this.valueCB = value[this.propShow];
            }
          }
          //Nếu dữ liệu mặc định tồn tại
          // if(!this.valueCB.trim() && !!this.dataValue.trim()) this.valueCB = this.dataValue
          
        }
      }

      //Trường hợp có lazy loading
      else{
        axios
          .get(`${this.Api}?currentPage=1&pageSize=20`)
          .then((response) => {
            if (response.status === 200) {
              console.log(response)
              this.values = response.data.List;
              //Nếu modelValue khác rỗng và số giá trị mảng option combobox lớn hơn không thì gán
              // giá trị input bằng giá trị value tương ứng
              if (this.modelValue !== "" && this.values.length > 0) {
                var value = this.values.find(
                  (item) => item[this.id] == this.modelValue
                );
                //Nếu dữ liệu lấy có chứa model
                if (value != undefined) {
                  this.valueCB = value[this.propShow];
                }
                //Nếu dữ liệu mặc định tồn tại
                else if(!!this.dataValue.trim()){
                  console.log(this.dataValue)
                  this.valueCB = this.dataValue
                }
              }
            }
          })
          .catch((e) => {
            console.log(e);
          });
      }
    },
    /**
     * Mô tả: Xử lý khi chọn combobox
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    handleSelect(value, index) {
      this.isShow = false;
      this.$emit("update:modelValue", value[this.id]);
      this.$emit("change",value)
      this.valueCB = value[this.propShow];
      this.removeClass("error");

      //Thêm class vào hàng được chọn
      var trElement = this.$refs['tbody'].querySelectorAll(
        "tr"
      );
      for (var tr of trElement) {
        tr.classList.remove("active");
      }
      if (!trElement[index].classList.contains("active")) {
        trElement[index].classList.add("active");
      }
    },

    /**
     * Mô tả: Thêm class lỗi vào combobox
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    addError(className, errorMsg) {
      this.error = true;
      this.$refs["combobox"].classList.add(className);
      if (errorMsg != undefined) {
        this.titleError = `${this.componentDes} ${errorMsg}`;
      }
    },

    /**
     * Mô tả: Xáo class lỗi khỏi combobox
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    removeClass(className) {
      this.$refs["combobox"].classList.remove(className);
      this.error = false;
      this.titleError = "";
    },
  },
};
</script>

<style scoped>
.combobox-container {
  border: 1px solid #afafaf;
  border-radius: 2px;
  display: flex;
  height: 32px;
  position: relative;
  font-size: 13px;
}
.error {
  border: 1px solid red;
}

.value-combobox {
  flex: 1;
  height: 100%;
}
.value-combobox input {
  height: 100%;
  width: 100%;
  border: none;
  outline: none;
  border-radius: 2px;
  padding: 0 12px;
}
.action {
  height: 100%;
  width: 30px;
  background-color: transparent;
  border-radius: 2px;
  border: none;
  position: relative;
}
.action button {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  border: none;
  outline: none;
  border-radius: 2px;
  padding: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #fff;
  
}
.action button:hover {
  background-color: #e0e0e0;
  border-color: #e0e0e0;
}
.action button div {
  background-position: -560px -359px;

  margin: 0;
}

.combobox-option {
  position: fixed;
  background-color: #fff;
  border: 1px solid #afafaf;
  border-radius: 2px;
  z-index: 100;
  max-height: 160px;
  background-color: #fff;
  overflow-y: auto;
}
.dropUp {
  top: unset;
  bottom: 130%;
}
.combobox-option table {
  width: 100%;
  border-spacing: 0;
}
.combobox-option table td,
.combobox-option table th {
  border: none;
  text-align: left;
  min-width: 30px;
  height: 32px;
  font-weight: 400;
}
.combobox-option table thead {
  background-color: #f4f5f8;
  color: #111;
  font-family: MISA notosans bold;
  cursor: default;
}
.table-one-column thead {
  display: none;
}
.table-one-column tbody tr td:first-child {
  display: none;
}
td,
th {
  text-align: left;
  padding: 0 14px 0 10px;
  white-space: nowrap;
}
.combobox-option table tbody tr {
  cursor: pointer;
}
.combobox-option table tbody tr:hover td {
  background-color: #ebedf0;
  color: #35bf22;
}
.active {
  color: #fff !important;
  background-color: #2ca01c !important;
}
.active:hover > td {
  color: #fff !important;
  background-color: #2ca01c !important;
}
.parent {
  font-weight: 700;
  font-family: MISA notosans bold;
}
</style>
<style>
button:disabled{
  background-color: transparent!important;;
}
</style>