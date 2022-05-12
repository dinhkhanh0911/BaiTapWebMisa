<template>
  <div>
    <HeaderComponent />
    <div class="body-container">
        <Menu />
        <router-view />
        <div id="toast">
          <Toast v-for="(item,index) in toast" :key="index" :message="item.message" :type="item.type"/>
        </div>
        
    </div>
    
    <!-- <EmployeesManagement /> -->
  </div>
</template>

<script>
import HeaderComponent from './components/HeaderComponent.vue'
import Menu from './components/Menu.vue'
import Toast from './components/Toast.vue'
import Popup from './components/Popup.vue'
import {mapMutations,mapGetters} from 'vuex'
export default {
  name: 'App',
  data(){
    return {
      
    }
  },
  components: {
    HeaderComponent,
    Menu,
    Toast,
    Popup
  },
  computed:{
    ...mapGetters(['toast','popup'])
  },
  methods: {
    ...mapMutations(['settoast','setPopup'])
  },
  watch:{
    toast: function(){
      console.log(this.toast)
      setTimeout(() => {
        this.toast.shift()
        this.settoast(this.toast)
      }, 2000);
    }
  },
}
</script>

<style>
#toast{
    position: fixed;
    top:24px;
    right: 24px;
    z-index: 1000;
}
</style>
