<template>
  <div class="table-container">
    <table>
        <thead>
            <tr>
                <th v-for="(value,index) in columnsDescription" :key="index">
                    {{value.columnName}}
                </th>
                <th class="option"></th>
            </tr>
        </thead>
        <tbody>
            <tr 
                
                v-for="(valueModel,index) in model" :key="index"
            >
                <td v-for="(value,index) in columnsDescription" :key="index" >
                    <BaseInput v-model="valueModel[value.columnField]" @input="onInput()" />
                </td>
                <td class="option d-flex justify-space-center alignt-center">
                    <div class="option-icon mi mi-16"></div>
                </td>
            </tr>
        </tbody>
    </table>
    
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
import BaseInput from "@/components/base/BaseInput.vue"
export default {
    components: {
        BasePopup,
        BaseInput,
    },
    data() {
        return {
            model:[],
            columnsDescription:[],
            timeOut:0,
        }
    },
    async mounted(){
        await this.loadColumnData()
    },
    props: {
        api:{
            type:String,
            default:""
        },
        tableName:{
        type:String,
        default:""
        },
        colNumber:{
            type:Number,
            default:1
        },
        modelValue:{
            type:String,
            default:''
        },
        timer: {
            type: Number,
            default: 500,
        },
    },
    created(){
        if(this.modelValue != '') this.model = JSON.parse(this.modelValue)
    },
    watch:{
        modelValue:function(newValue,oldValue){
            console.log(oldValue)
        }
    },  
    methods:{
        addRow(){
            var obj = {}
                this.columnsDescription.forEach(x => {
                    obj[x.columnField]=''
                })
            this.model.push(obj)
        },
        loadColumnData(){
            if(this.api != ""){
                axios.get(this.api)
                    .then((response) => {
                        if(response.status == 200){
                            this.columnsDescription = response.data.columns
                            this.handleModelValueNull()
                        }
                    })
            }
        },
        handleModelValueNull(){
            if(this.model.length == 0){
                this.addRow()
            }
        },
        onInput(){
            if (this.timer === 0) {
                this.clearNullObjectAndEmit()
                
                
            } else {
                this.clearNullObjectAndEmit()
            }
        },
        clearNullObjectAndEmit(){
            this.model = this.model.filter((x) => {
                for(var key in x){
                    if(x[key] !== '' ) return true
                }
                if(this.model.length == 1) return true
                return false
            })
            if(this.model.length == 1){
                for(var key in this.model[0]){
                    if(this.model[0][key] !== '' ){
                        console.log(this.model[key])
                        this.$emit("update:modelValue", JSON.stringify(this.model));
                        return
                    }
                }
                this.$emit("update:modelValue","");
            }
            else this.$emit("update:modelValue", JSON.stringify(this.model));

        }
    }
    
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
}
.option{
    min-width: 50px;
}
.option-icon{
    background-position: -464px -312px;
}
</style>
