<template>
    <div class="layout-main">
        <TheMenu :toggle="isZoomOutMenu" @zoomOut="handleZoomOut()"/>
        <div class="content-area">
            <div class="content-wrapper">
                <TheHeader @toggle ="handleToggle" :isDisabled="isZoomOut"/>
                <router-view />
            </div>
        </div>
    </div>
</template>

<script>
import TheEmployees from '@/components/layout/TheEmployees.vue'
import TheHeader from '@/components/layout/TheHeader.vue'
import TheMenu from '@/components/layout/TheMenu.vue'
export default {
  name: "App",
  components: {
    TheEmployees,
    TheHeader,
    TheMenu
  },
  data(){
    return{
      isZoomOutMenu:false,
      isZoomOut:true
    }
  },
  methods:{
    handleZoomOut(){
      this.isZoomOutMenu = false
      this.isZoomOut = false
    },
    handleToggle(value){
      this.isZoomOutMenu = true
      this.isZoomOut = true
    }
    
    
  }
};
</script>

<style>
.layout-main{
  display: flex;
  overflow: hidden;
  height: 100vh;
}
.content-area{
  width: calc(100vw - var(--menu-width));
  flex: 1;
}
.content-wrapper{
  position: relative;
}
</style>

