<template>
    <div class="m-combobox">
        <div class="combobox">
            <input
                class="m-input-combobox combobox-input"
                type="text"
                :placeholder= "placeholder"
                @focus="displayBoxItem()"
                :value="convertModel"
                
            />
            <span class="combobox-icon">
                <i class="fa fa-angle-down combobox-down" v-show="!isShowIcon" aria-hidden="true"></i>
                <i class="fa fa-angle-up combobox-up" v-show="isShowIcon" aria-hidden="true"></i>
            </span>
            <div ref="item-box" class="item-box" >
                <div class="item" v-for="(item,index) in options" :key="index" :class="{'item-selected' : isSelected(item[id])}"    @click="changeValue(item[id])">{{item[name]}}</div>
            </div>
        </div>
        
    </div>
</template>

<script>
import axios from 'axios'
export default {
    props:{
        placeholder:{
            type:String,
            default: ''
        },
        default :{
            type:Array,
            default: []
        },
        modelValue :{
            type:String,
            default: undefined
        },
        id:{
            type:String,
            default: ''
        },
        name:{
            type:String,
            default:''
        },
        api:{
            type:String,
            default:''
        }
        
    },
    created() {
            
        if(this.api != ''){
            //Sau khi khởi tạo
            // Lấy dữ liệu combobox
            axios.get(this.api)
                .then((response)=>{
                    if(response.status === 200){
                        this.options = response.data
                        console.log(this.options)
                        if(this.modelValue == undefined && this.options.length > 0){
                            this.$emit('update:modelValue',this.options[0][this.id])
                        }
                    }
                })
        }
        else{
            this.options = this.default
            if(this.modelValue == undefined && this.options.length > 0){
                this.$emit('update:modelValue',this.options[0][this.id])
            }
        }
        

    },
    mounted() {
        
    },
    data() {
        return {
           isShowIcon : true,
           options:[],
        }
    },
    computed:{
        convertModel(){
            if(this.options.length >0){
                if(this.modelValue == undefined) return this.options[0][this.name]
                return this.options.find(item => item[this.id] === this.modelValue)[this.name]
            }
            return ''
        }
    },
    methods: {
        displayBoxItem(){
            this.$refs['item-box'].style.display = 'block'
            this.isShowIcon = false
        },
        changeValue(value){
            this.$emit('update:modelValue',value)
            this.$refs['item-box'].style.display = 'none'
            this.isShowIcon = true
        },
        isSelected(id){
            return this.modelValue == id
        }
    },
}
</script>
<style>

</style>

