<template>
    <div class="navbar " :class="{'zoom-out': !toggle}">
        <!-- <div class="navbar-item navbar-active">
            <a class="navbar-link " @mouseover="hover($event,'Tổng quan')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuOverview">
                    <div class="item-icon mi-24">
                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Tổng quan
                    </span>
                </div>
            </a>
        </div> -->
        <div class="navbar-item navbar-active">
            <router-link to="/CA" class="navbar-link" @mouseover="hover($event,'Tiền mặt')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuCash">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span >
                        Tiền mặt
                    </span>
                </div>
            </router-link>
        </div>
        <!-- <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Tiền gửi')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuBank">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Tiền gửi
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link menuBuy" @mouseover="hover($event,'Mua hàng')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                       Mua hàng
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Bán hàng')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuSell" >
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Bán hàng
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item"> 
            <a class="navbar-link" @mouseover="hover($event,'Quản lý hóa đơn')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuInvoice" >
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Quản lý hóa đơn
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Kho')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuStock">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Kho
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Công cụ dụng cụ chi phí trả trước')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuTools">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Công cụ dụng cụ
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Tài sản cố định')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuAsset">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Tài sản cố định
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Thuế')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuTax">
                    <div class="item-icon mi-24 ">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Thuế
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Giá thành')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuPrice">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Giá thành
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Tổng hợp')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuGeneral">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Tổng hợp
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Ngân sách')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuBudget">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Ngân sách
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Báo cáo')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuReport">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Báo cáo
                    </span>
                </div>
            </a>
        </div>
        <div class="navbar-item">
            <a class="navbar-link" @mouseover="hover($event,'Phân tích tài chính')" @mouseleave="isShowTooltip = false">
                <div class="navbar-icon menuFinance">
                    <div class="item-icon mi-24">

                    </div>
                </div>
                <div class="navbar-content">
                    <span>
                        Phân tích tài chính
                    </span>
                </div>
            </a>
        </div> -->
        <BaseTooltip :top="topTooltip" :left="leftTooltip" v-if="isShowTooltip" :valueTooltip="valueTooltip"/>
    </div>
</template>

<script>
/**
* Mô tả: Navbar
* Created by: Đinh Văn Khánh - MF1112
* Created date: 18/04/2022
*/
import BaseTooltip from '@/components/base/BaseTooltip.vue'
export default {
    components:{
        BaseTooltip
    },
    data() {
        return {
            topTooltip:0,
            leftTooltip:0,
            isShowTooltip:false,
            valueTooltip:''
        }
    },
    props:{
        toggle:{
            type:Boolean,
            default:false
        }
    },
    methods: {

        /**
        * Mô tả:  Xử lý khi hover vào element
        @param(event,Nội dung tooltip)
        * Created by: Đinh Văn Khánh - MF1112
        * Created date: 18/04/2022
        */
        hover(e,value){
            this.topTooltip = e.clientY + 10
            this.leftTooltip = e.clientX +10
            this.isShowTooltip = true
            this.valueTooltip = value
        }
    },
}
</script>

<style scoped>
.navbar{
    height: calc(100% - 48px);
    padding-top: 10px;
}
.navbar-link{
    display: flex;
    padding-left: 8px;
    padding-right: 14px;
    align-items: center;
    border-left: 6px solid #393a3d;
    text-decoration: none;
}
.navbar-link *{
    pointer-events: none;
}
.navbar-link:hover{
    background-color: #5D5D61;
    border-left: 6px solid #5D5D61;
    cursor: pointer;
}
.navbar-icon{
    height: 42px;
    display: flex;
    align-items: center;
    margin-right: 10px;
}
.navbar-content{
    height: 42px;
    color: #fff;
    display: flex;
    align-items: center;
}
.menuOverview .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -30px -1585px;
}
.menuCash .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -73px -1628px;
}
.menuBank .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -117px -1628px;
}
.menuBuy .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -161px -1628px;

}
.menuSell .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-Position: -204px -1628px;
}

.menuInvoice .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -249px -1628px;
}
.menuStock .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -291px -1628px;
    
}
.menuTools .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -336px -1628px;

}
.menuAsset .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -378px -1630px;
}
.menuTax .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -378px -1630px;

}
.menuPrice .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -467px -1628px;

}
.menuGeneral .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -510px -1628px;

}
.menuBudget .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -378px -1659px;

}
.menuReport .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -547px -1628px;

}
.menuFinance .item-icon{
    background-image: url("@/assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    background-position: -204px -1664px;
}

.zoom-out .navbar-content{
    display: none;
}
</style>