<template>
    <div class="header">
        <div class="header-left">
            <div class="logo-container">
                <button class="m-icon-btn">
                    <img src="../assets/img/toggle.png" alt="" width="24" height="24" >
                </button>
                <div class="logo-box">
                    <img src="../assets/img/cukcuk-logo.png" alt="" height="24">
                </div>
            </div>
        </div>
        <div class="header-right">
            <div class="header__select-box custom-select custom-select__header">
                <Combobox :placeholder="'Đang tải dữ liệu...'" :id="'restaurantId'" :name="'restaurantName'" 
                :default="restaurants" v-model="restaurant.restaurantId" :modelValue="restaurant.restaurantName"/>
                
            </div>
            <div class="header__user">
                <div class="user__icon">
                    <img src="../assets/icon/avatar-default.png" alt="" width="35">
                </div>
                <div class="user__name">
                    <span>Đinh Văn Khánh</span>
                </div>
                <div class="user__option">
                    <button class="m-icon-btn">
                        <img src="../assets/icon/option.png" alt="">
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Combobox from './Combobox.vue'
export default {
    data() {
        return {
            restaurant:{
                restaurantId:0,
                restaurantName:"Nhà hàng Biển Đông"          
            },
            restaurants:[
                { restaurantId:0,restaurantName:"Nhà hàng Biển Đông"},
                { restaurantId:1,restaurantName:"Nhà hàng Biển Tây"},
                { restaurantId:2,restaurantName:"Nhà hàng Biển Bắc"},
            ],
        }
    },
    components:{
        Combobox
    }
}
</script>

<style>
    
.header{
    height: var(--header-height);
    display: flex;
    
}
.header-left{
    width: 220px;
    border-right: 1px solid #E5E5E5;
}
.header-right{
    flex: 1;
    
    padding: 0 16px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-family: MISA GoogleSans;
    font-size: 13px;
    color: #000;
}
.header__user{
    display: flex;
    align-items: center;
}
.header__user div{
    margin-left: 16px;
}
.user__icon,
.user__option button{
    display: flex;
    align-items: center;
}
</style>
