<template>
  <div class="combobox-container" ref="combobox" :title="titleError">
    <div class="value-combobox d-flex alignt-center">
        <div class="select-item" v-for="(value, index) in valueChecked"
            :key="index" 
        >
          <div class="d-flex alignt-center" >
              <div class="item-text" >
                  {{ value[code] }}
              </div>
              <div class="item-icon mi mi-16"
              :title="value[code]">
  
              </div> 

          </div>
            
        </div>
      <input type="text" @keyup="deleteEvent($event)" v-model="valueCB" @keydown="handleDeleteEvent($event)"
      @input="handleInput"/>
    </div>
    <div class="add">
      <button>
        <div class="mi mi-16"></div>
      </button>
    </div>
    <div class="action">
      <button @click="isShow = !isShow">
        <div class="mi mi-16"></div>
      </button>
    </div>
    <div
      class="combobox-option"
      v-show="isShow"
      :class="{ dropUp: !isDropDown }"
    >
      <table :class="{ 'table-one-column': isOneColumn }">
        <thead>
          <tr>
            <th>Mã đơn vị</th>
            <th>Tên đơn vị</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(value, index) in values"
            :key="index"
            @click="handleSelect(value, index)"
            :class="{ active: value[id] == modelValue }"
          >
            <td>{{ value[code] }}</td>
            <td>{{ value[name] }}</td>
            <td class="selected-container">
                <div class="selected-icon mi mi-16" v-if="checkValueChecked(value)"></div>
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
export default {
  data() {
    return {
        isShow: false,
        valueCB: "",
        values: [],
        titleError: "",
        error: false,

        //Giá trị id các giá trị được chọn        
        valueChecked:[],
        objectChecked:[],

        timeOut: null,
        model:[]
    };
  },
  props: {
    Api: {
      type: String,
      default: "",
    },
    modelValue: {
      type:String,
      default:""
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
    timer:{
      type:Number,
      default:1000,
    },
  },
  created() {
    
    //Lấy dữ liệu combobox
    //Nếu Api có, gọi Api lấy giữ liệu
    console.log(this.modelValue)
    if(!!this.modelValue){
      this.valueChecked = JSON.parse(this.modelValue)
    }
    
    if (this.Api != "") {
      axios
        .get(this.Api)
        .then((response) => {
          if (response.status === 200) {
            this.values = response.data;
            //Nếu modelValue khác rỗng và số giá trị mảng option combobox lớn hơn không thì gán
            // giá trị input bằng giá trị value tương ứng
            // if (this.modelValue !== "" && this.values.length > 0 && this.valueCB == null) {
            //   var value = this.values.find(
            //     (item) => item[this.id] == this.modelValue
            //   );
            //   if (value != undefined) {
            //     this.valueCB = value[this.name];
            //   }
            // }
          }
        })
        .catch((e) => {
          console.log(e);
        });
    } else {
      this.values = this.valueOption;
      /**
       * Nếu Api không có, lấy mảng trong prop
       * Nếu modelValue khác rỗng và số giá trị mảng option combobox
       * lớn hơn không thì gán giá trị input bằng giá trị value tương ứng
       */
      if (this.modelValue !== "" && this.values.length > 0) {
        var value = this.values.find(
          (item) => item[this.id] == this.modelValue
        );
        if (value != undefined) {
          this.valueCB = value[this.name];
        }
      }
    }
  }, 
  watch: {
    
    /**
     * Mô tả:  Lăng nghe thay đổi giá trị model
     * Nếu model rỗng thì gán giá trị input bằng rỗng
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    modelValue: function () {
      if (this.modelValue == "") {
        this.valueCB = "";
      }
      
    },
  },
  methods: {
    reloadOption(){
      if (this.Api != "" ) {
        var apiSearch
        if(this.valueCB.trim() ===''){
          apiSearch = this.Api
        }
        else apiSearch = `${this.Api}/search?searchValue=${this.valueCB}`
        axios
          .get(apiSearch)
          .then((response) => {
            if (response.status === 200) {
              this.values = response.data;
              this.isShow = true
              
              //Nếu modelValue khác rỗng và số giá trị mảng option combobox lớn hơn không thì gán
              // giá trị input bằng giá trị value tương ứng
              // if (this.modelValue !== "" && this.values.length > 0 && this.valueCB == null) {
              //   var value = this.values.find(
              //     (item) => item[this.id] == this.modelValue
              //   );
              //   if (value != undefined) {
              //     this.valueCB = value[this.name];
              //   }
              // }
            }
          })
          .catch((e) => {
            console.log(e);
          });
      } else {
        this.values = this.valueOption;
        /**
         * Nếu Api không có, lấy mảng trong prop
         * Nếu modelValue khác rỗng và số giá trị mảng option combobox
         * lớn hơn không thì gán giá trị input bằng giá trị value tương ứng
         */
        if (this.modelValue !== "" && this.values.length > 0) {
          var value = this.values.find(
            (item) => item[this.id] == this.modelValue
          );
          if (value != undefined) {
            this.valueCB = value[this.name];
          }
        }
      }
    },
    handleInput(){
      
      if (this.timer === 0) {
        this.reloadOption()
        console.log("A")
      } else {
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() => {
          this.reloadOption()
          
        }, this.timer);
      }
      
    },
    deleteEvent(e){
        e.preventDefault();
    },
    handleDeleteEvent(e){
      
      if(!this.valueCB){
          if(e.keyCode == 8 && this.valueChecked.length >0){
              this.valueChecked.pop()
          }
        }
        
    },
    checkValueChecked(value){
      var ok = !!this.valueChecked.find(x => x[this.id]==value[this.id])
      if(ok) return true
      else return false
    },
    /**
     * Mô tả: Xử lý khi chọn combobox
     * Created by: Đinh Văn Khánh - MF1112
     * Created date: 14/04/2022
     */
    handleSelect(value, index) {
    //   this.isShow = false;
    //    this.$emit("update:modelValue", id);
    //   this.valueCB = name;
        
        var ok = !!this.valueChecked.find(x => x[this.id]==value[this.id])
        if(!ok){
            this.valueChecked.push(value)
        }
        else{
            this.valueChecked = this.valueChecked.filter(x => x[this.id] != value[this.id])
        }
        var id = this.id
        var code = this.code
        var name = this.name
        var modelValue = this.valueChecked.map((x) => {
          var obj = {}
          obj[id]= x[id]
          obj[code] = x[code]
          obj[name] = x[name]
          return obj
        })
        
        this.$emit("update:modelValue",JSON.stringify(modelValue));
        
        // this.removeClass("error");

        // //Thêm class vào hàng được chọn
        // var trElement = document.querySelectorAll(
        //     ".combobox-option table tbody tr"
        // );
        
        // for (var tr of trElement) {
        //     tr.classList.remove("active");
        // }
        // if (!trElement[index].classList.contains("active")) {
        //     trElement[index].classList.add("active");
        // }
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
  
  position: relative;
  font-size: 13px;
}
.error {
  border: 1px solid red;
}

.value-combobox {
  flex: 1;
  height: 100%;
  padding: 3px 12px;
  flex-wrap: wrap;
  min-height: 30px;
  
}
.select-item{
    background-color: #e0e0e0;
    min-width: 20px;
    padding: 3px 7px;
    position: relative;
}
.select-item{
    margin-right: 4px;
    margin-bottom: 4px;
}
.select-item .item-text{
    margin-right: 5px;
}
.select-item .item-icon{
    background-position: -80px -312px;
}

.value-combobox input {
    width: 100%;
    border: none;
    outline: none;
    border-radius: 2px;
}
.add,
.action {
  
  width: 30px;
  border-radius: 2px;
  border: none;
  position: relative;
}
.add button,
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
.add button:hover,
.action button:hover {
  background-color: #e0e0e0;
  border-color: #e0e0e0;
}
.action button div {
  background-position: -560px -359px;

  margin: 0;
}
.add button div{
    background-position: -32px -312px;
}
.combobox-option {
  position: absolute;
  left: -1px;
  right: -1px;
  top: calc(100% + 5px);
  background-color: #fff;
  border: 1px solid #afafaf;
  border-radius: 2px;
  z-index: 100;
  max-height: 160px;
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
  position: sticky;
  top: 0;
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
}
.combobox-option table tbody tr {
  cursor: pointer;
}
.combobox-option table tbody tr:hover td {
  background-color: #ebedf0;
  color: #35bf22;
}
.combobox-option table tbody tr .selected-container .selected-icon{
    background-position: -896px -312px;
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
                   