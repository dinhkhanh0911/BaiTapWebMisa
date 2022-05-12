<template>
    <div class="body-left " :class="{'m-body-left': isShow}">
            <div class="menu">
                <ul class="nav">
                    <li class="nav-item">
                        <router-link to="#" class="nav-link">
                            <img src="../assets/icon/dashboard.png" alt="" width="20" height="20" >
                            <span>Tổng quan</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/report" class="nav-link">
                            <img src="../assets/icon/report.png" alt="" width="20" height="20" >
                            <span>Báo cáo</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="#" class="nav-link">
                            <img src="../assets/icon/dashboard.png" alt="" width="20" height="20" >
                            <span>Mua hàng</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/employees" class="nav-link">
                            <img src="../assets/icon/dic-employee.png" alt="" width="20" height="20" >
                            <span>Danh mục nhân viên</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/employees" class="nav-link">
                            <img src="../assets/icon/dic-employee.png" alt="" width="20" height="20" >
                            <span>Danh mục nhân viên</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="#" class="nav-link">
                            <img src="../assets/icon/setting.png" alt="" width="20" height="20" >
                            <span>Thiết lập hệ thống</span>
                        </router-link>
                    </li>
                </ul>
                <div class="set-size">
                    <button id="m-set-size-btn" class="min-size" @click="isShow =! isShow">
                        <img src="../assets/icon/btn-prev-page.svg" alt="" v-show="!isShow">
                        <img src="../assets/icon/btn-next-page.svg" alt="" v-show="isShow">
                    </button>
                </div>
            </div>
        </div>
</template>

<script>
export default {
    data(){
        return{
            
            isShow : false,
            
        }
    },
}
</script>

<style>

</style>